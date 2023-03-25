using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("concept", TypeName = "VARCHAR")]
        public string Concept { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("type", TypeName = "VARCHAR")]
        public TransactionType Type { get; set; }

        // FK not null
        [Required]
        public int account_id { get; set; }

        //FK not null
        [Required]
        public int user_id { get; set; }

        //FK
        public int? to_account_id { get; set; }

        //Propiedad de navegación hacía la cuenta asociada a esta transacción
        [ForeignKey("account_id")]
        public Account Account { get; set; }

        //Propiedad de navegación hacia el usuario asociado a esta transacción
        [ForeignKey("user_id")]
        public User User { get; set; }

        //Propiedad de navegación hacia la cuenta receptora (en caso de type = payment).
        [ForeignKey("to_account_id")]
        public Account ToAccount { get; set; }


    }
}
