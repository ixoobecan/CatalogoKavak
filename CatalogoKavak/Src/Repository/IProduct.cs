using CatalogoKavak.Src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Repository
{
    public interface IProduct
    {
        Task<List<Product>> TakeAllProductAsync();
        Task<Product> TakeProductByIdAsync(int id);
        Task NewProductAsync(Product Product);
        Task UpdateProductAsync(Product Product);
        Task DeleteProductAsync(int id);
    }
}
