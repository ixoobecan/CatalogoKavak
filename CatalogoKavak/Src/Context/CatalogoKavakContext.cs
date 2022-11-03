using Microsoft.EntityFrameworkCore;
using CatalogoKavak.Src.Models;

namespace CatalogoKavak.Src.Context
{
    public class CatalogoKavakContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public CatalogoKavakContext(DbContextOptions<CatalogoKavakContext> opt) : base(opt) { }

    }
}
