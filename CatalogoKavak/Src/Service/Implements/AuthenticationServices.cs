using CatalogoKavak.Src.Models;
using CatalogoKavak.Src.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Service.Implements
{
    public class AuthenticationServices : IAutentication
    {
        #region Attribute
        private IUser _repositorio;
        public IConfiguration Configuracao { get; }
        #endregion

        #region Controllers
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repositorio = repository;
            Configuracao = configuration;
        }
        #endregion

        #region Methodos
        /// <summary> 
        /// <para>Resumo: Método responsável por criptografar senha</para> 
        /// <param name="senha">Senha a ser criptografada</param> 
        /// </summary> 
        public string EncodePassword(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(bytes);
        }
        /// <summary> 
        /// <para>Resumo: Método assíncrono responsavel por criar usuario sem duplicar no banco</para> 
        /// <param name="usuario"> Construtor para cadastrar usuario</param> 
        /// </summary> 
        public async Task CreateUserWithoutDuplicateAsync(User user)
        {
            var aux = await _repositorio.TakeUserByEmailAsync(user.Email);

            if (aux != null) throw new Exception("Este email já está sendo utilizado!");

            user.Senha = EncodePassword(user.Senha);

            await _repositorio.NewUserAsync(user);
        }

        ///  /// <summary> 
        /// <para>Resumo: Método responsável por gerar token JWT</para> 
        /// <param name="usuario"> Construtor de usuario que tenha parametros de e-mail e senha</param> 
        /// </summary> 
        public string CreateToken(User user)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracao["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Tipo.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }
        #endregion
    }
}

