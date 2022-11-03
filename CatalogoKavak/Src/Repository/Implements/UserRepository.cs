using CatalogoKavak.Src.Context;
using CatalogoKavak.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Repository.Implements
{
    public class UserRepository : IUser
    {
        #region Attribute 
        private readonly CatalogoKavakContext _context;
        
        #endregion

        #region Controllers
        public UserRepository(CatalogoKavakContext context)
        {
            _context = context;
        }
        #endregion

        #region Methodos
        public async Task NewUserAsync(User user)
        {
            await _context.User.AddAsync(new User
            {
                Nome = user.Nome,
                Email = user.Email,
                Senha = user.Senha,
                Foto = user.Foto,
                Telefone = user.Telefone,
                Endereco = user.Endereco,
                CPF = user.CPF,
                Tipo = user.Tipo
            });
            await _context.SaveChangesAsync();
        }

        public async Task<User> TakeUserByEmailAsync(string email)
        {

            return await _context.User.FirstOrDefaultAsync(e => e.Email == email);

        }

        public async Task UpdateUserAsync(User user)
        {
            var aux = await _context.User.FirstOrDefaultAsync(u => u.Id == user.Id);
            aux.Nome = user.Nome;
            aux.Email = user.Email;
            aux.Senha = user.Senha;
            aux.Foto = user.Foto;
            aux.Telefone = user.Telefone;
            aux.Endereco = user.Endereco;
            aux.CPF = user.CPF;

            _context.User.Update(aux);
            await _context.SaveChangesAsync();
        }

        public async Task<User> TakeUserByCPFAsync(string cpf)
        {
            if (!ExisteCPF(cpf)) throw new Exception("CPF usuário não encontrado!");

            return await _context.User.FirstOrDefaultAsync(c => c.CPF == cpf);

            // função auxiliar
            bool ExisteCPF(string cpf)
            {
                var auxiliar = _context.User.FirstOrDefault(c => c.CPF == cpf);
                return auxiliar != null;
            }
        }
        public async Task<List<User>> TakeAllUserAsync()
        {
            return await _context.User.ToListAsync();
        }
        public async Task<User> TakeUserByIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id de usuário não encontrado!");

            return await _context.User.FirstOrDefaultAsync(i => i.Id == id);

            // função auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _context.User.FirstOrDefault(i => i.Id == id);
                return auxiliar != null;
            }
        }
        public async Task DeleteUserAsync(int id)
        {
            _context.User.Remove(await TakeUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
