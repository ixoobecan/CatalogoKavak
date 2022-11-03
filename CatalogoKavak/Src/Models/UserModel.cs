using CatalogoKavak.Src.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatalogoKavak.Src.Models
{
    [Table("tb_users")]
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }    
        public string Endereco { get; set; }
        public string CPF { get; set; }

        [Required]
        public Usertype Tipo { get; set; }

    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}





