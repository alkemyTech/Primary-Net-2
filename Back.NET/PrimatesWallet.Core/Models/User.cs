using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrimatesWallet.Core.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("first_name", TypeName = "VARCHAR(50)")]
        public string First_Name { get; set; }

        [Required]
        [Column("last_name", TypeName = "VARCHAR(50)")]
        public string Last_Name { get; set; }

        [Required]
        [Column("email", TypeName = "VARCHAR(100)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("password", TypeName = "VARCHAR(max)")]
        public string Password { get; set; }


        [Column("points", TypeName = "INT")]
        public int Points { get; set; } = 0;

        [Required]
        public int Rol_Id { get; set; } = 2;

        [ForeignKey("Rol_Id")]
        public Role Role { get; set; }

        public Account Account { get; set; }

        [Column("idDeleted", TypeName = "BIT")]
        public bool IsDeleted { get; set; }
    }

}
