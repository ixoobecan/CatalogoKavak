using CatalogoKavak.Src.Models;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Service
{
    public interface IAutentication
    {
        string EncodePassword(string senha);
        Task CreateUserWithoutDuplicateAsync(User user);
        string CreateToken(User user);
    }
}

