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

        [Column("first_name", TypeName = "VARCHAR")]
        public string First_Name { get; set; }

        [Column("last_name", TypeName = "VARCHAR")]
        public string Last_Name { get; set; }

        [Required]
        [Column("email", TypeName = "VARCHAR")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("password", TypeName = "VARCHAR")]
        public string Password { get; set; }

        [Column("points", TypeName = "INT")]
        public int Points { get; set; }

        [Required]
        public int Rol_Id { get; set; }

        [ForeignKey("rol_id")]
        public Role Role { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Account Account { get; set; }

    }

}
