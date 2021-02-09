using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Table("userhash")]
    public class Usuario
    {
        [Key]
        [Column("idusuario")]
        public int IdUsuario { get; set; }
        [Column("nombre")]
        public String Nombre { get; set; }
        [Column("usuario")]
        public String UserName { get; set; }
        [Column("pass")]
        public byte[] Password { get; set; }
        [Column("salt")]
        public String Salt { get; set; }
    }
}
