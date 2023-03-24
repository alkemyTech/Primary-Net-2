using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Models
{
    public class Prueba
    {
        [Key]
        public int Id { get; set; }
        [Column("name", TypeName = "VARCHAR(50)")]
        public string Nombre { get; set; }
    }
}
