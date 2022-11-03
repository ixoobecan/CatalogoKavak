using CatalogoKavak.Src.Context;
using CatalogoKavak.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Repository.Implements
{
    public class ProductRepository : IProduct
    {
        #region Attribute 
        private readonly CatalogoKavakContext _context;
        #endregion

        #region Controllers
        public ProductRepository(CatalogoKavakContext context)
        {
            _context = context;
        }
        #endregion

        #region Methodos
        public async Task DeleteProductAsync(int id)
        {
            _context.Product.Remove(await TakeProductByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task NewProductAsync(Product product)
        {
            {
                await _context.Product.AddAsync(new Product
                {
                    Nome = product.Nome,
                    Descricao = product.Descricao,
                    Marca = product.Marca,
                    Modelo = product.Modelo,
                    Preco = product.Preco,
                    Foto = product.Foto,
                   
                });
                await _context.SaveChangesAsync();
            }
        }
         
        public async Task<Product> TakeProductByIdAsync(int id)
        {
            {
                if (!ExisteId(id)) throw new Exception("Id do médico não encontrado!");

                return await _context.Product.FirstOrDefaultAsync(i => i.Id == id);

                // função auxiliar
                bool ExisteId(int id)
                {
                    var auxiliar = _context.Product.FirstOrDefault(i => i.Id == id);
                    return auxiliar != null;
                }
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            {
                var aux = await _context.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
                aux.Nome = product.Nome;
                aux.Descricao = product.Descricao;
                aux.Marca = product.Marca;
                aux.Modelo = product.Modelo;
                aux.Foto = product.Foto;
                aux.Preco = product.Preco;
                

                _context.Product.Update(aux);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> TakeAllProductAsync()
        {
            return await _context.Product.ToListAsync();
        }
        #endregion
    }

}