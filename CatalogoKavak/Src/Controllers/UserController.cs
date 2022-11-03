using CatalogoKavak.Src.Models;
using CatalogoKavak.Src.Repository;
using CatalogoKavak.Src.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Controllers
{
    [ApiController]
    [Route("api/User")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attribute
        private readonly IUser _repository;
        private readonly IAutentication _services;

        #endregion

        #region Controllers
        public UserController(IUser repository, IAutentication services)
        {
            _repository = repository;
            _services = services;
        }
        #endregion
        #region Methodos

        /// <summary> 
        /// Pegar todos os usuários
        /// </summary>
        /// <para> Resumo: Método assincrono para pegar todos os usuarios</para>
        /// <returns>ActionResult</returns> 
        /// <response code="200">Retorna todos os usuarios</response> 
        /// <response code="403">Usuario não autorizado</response>
        [HttpGet("allUsers")]
        [Authorize(Roles = "REGULAR, ADMIN, DEV")]
        public async Task<ActionResult> TakeAllUserAsync()
        {
            var lista = await _repository.TakeAllUserAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar usuário pelo Id
        /// </summary>
        /// <param name="idUser">Id do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Id não existente</response>
        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "REGULAR,ADMIN,DEV")]
        public async Task<ActionResult> TakeUserByIdAsync([FromRoute] int idUser)
        {
            try
            {
                return Ok(await _repository.TakeUserByIdAsync(idUser));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pegar usuário pelo Email
        /// </summary>
        /// <param name="emailUser">Email do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "ADMIN,DEV")]
        public async Task<ActionResult> TakeUserByEmailAsync([FromRoute] string emailUser)
        {
            try
            {
                return Ok(await _repository.TakeUserByEmailAsync(emailUser));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pegar usuário pelo CPF
        /// </summary>
        /// <param name="cpfUser">CPF do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Usuário encontrado</response> 
        /// <response code="404">Email não existente</response>
        [HttpGet("cpf/{cpfUser}")]
        [Authorize(Roles = "ADMIN,DEV")]
        public async Task<ActionResult> TakeUserByCPFAsync([FromRoute] string cpfUser)
        {
            try
            {
                return Ok(await _repository.TakeUserByCPFAsync(cpfUser));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criar novo Usuario 
        /// </summary> 
        /// <param name="user">Contrutor para criar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/User/newUser
        ///     { 
        ///         "nome": "Nome do usuário",
        ///         "email": "usuario@email.com",
        ///         "senha": "senha123",
        ///         "foto": "Url_Foto",
        ///         "telefone": "1122334455",
        ///         "endereco": "Rua 1, n 123",
        ///         "cpf": "11122233344",
        ///         "tipo": "REGULAR"
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="201">Retorna usuario criado</response> 
        /// <response code="422">Email ja cadastrado</response>
        [HttpPost("newUser")]
        [AllowAnonymous]
        public async Task<ActionResult> NewUserAsync([FromBody] User user)
        {
            try
            {
                await _services.CreateUserWithoutDuplicateAsync(user);
                return Created($"api/User/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary> 
        /// <param name="user">Construtor para atualizar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     PUT /api/User/UpdateUser 
        ///     { 
        ///         "id": 0,
        ///         "nome": "Nome do usuário",
        ///         "email": "usuario@email.com",
        ///         "senha": "senha123",
        ///         "foto": "Url_Foto",
        ///         "telefone": "1122334455",
        ///         "endereco": "Rua 1, n 123",
        ///         "cpf": "11122233344",
        ///         "tipo": "REGULAR"
        ///     }
        ///     
        /// </remarks> 
        /// <response code="200">Usuario atualizado</response> 
        /// <response code="400">Erro na requisição</response>
        [HttpPut("UpdateUser")]
        [Authorize(Roles = "REGULAR,ADMIN,DEV")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] User user)
        {
            try
            {
                await _repository.UpdateUserAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar usuário
        /// <para>Função assíncrona para deletar usuário pelo Id</para>
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuário deletado</response>
        /// <response code="404">Id do usuário não existe</response>
        [HttpDelete("deleteUser/{idUser}")]
        [Authorize(Roles = "DEV")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            try
            {
                await _repository.DeleteUserAsync(idUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }


        /// <summary> 
        /// Pegar Autorização 
        /// </summary> 
        /// <param name="user">Construtor para logar usuario</param> 
        /// <returns>ActionResult</returns> 
        /// <remarks> 
        /// Exemplo de requisição: 
        /// 
        ///     POST /api/User/logar 
        ///     { 
        ///         "email": "usuario@email.com",
        ///         "senha": "134652" 
        ///     } 
        ///     
        /// </remarks> 
        /// <response code="200">Usuario logado</response> 
        /// <response code="401">E-mail ou senha invalido</response>
        /// 
        [HttpPost("logar")]
        [AllowAnonymous]
        public async Task<ActionResult> LogarAsync([FromBody] User user)
        {
            var auxiliar = await _repository.TakeUserByEmailAsync(user.Email);

            if (auxiliar == null) return Unauthorized(new
            {
                Mensagem = "Email inválido!"
            });

            if (auxiliar.Senha != _services.EncodePassword(user.Senha))
                return Unauthorized(new { Mensagem = "Senha inválida!" });

            var token = "Bearer " + _services.CreateToken(auxiliar);
            return Ok(new { User = auxiliar, Token = token });
        }
        #endregion 
    }
}

