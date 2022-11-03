using CatalogoKavak.Src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Repository
{
    public interface IUser
    {
        Task<User> TakeUserByEmailAsync(string Email);
        Task NewUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> TakeUserByCPFAsync(string cpf);
        Task<User> TakeUserByIdAsync(int id);
        Task<List<User>> TakeAllUserAsync();
        Task DeleteUserAsync(int id);


    }
}
