using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Models
{
    [Table("tb_products")]
    public class Product
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Foto { get; set; }
        public float Preco { get; set; }
        public string Descricao { get; set; }
        
    }
}


