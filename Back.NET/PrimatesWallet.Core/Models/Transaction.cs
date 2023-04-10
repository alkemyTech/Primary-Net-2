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

    public class Transaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column("concept", TypeName = "VARCHAR(50)")]
        public string Concept { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("type", TypeName = "VARCHAR(15)")]
        public TransactionType Type { get; set; }

        // FK not null
        [Required]
        [Column("account_id")]
        public int Account_Id { get; set; }

        //Propiedad de navegación hacía la cuenta asociada a esta transacción
        [ForeignKey("Account_Id")]
        public Account Account { get; set; }

        //FK
        [Column("to_account_id")]
        public int? To_Account_Id { get; set; }

        //Propiedad de navegación hacia la cuenta receptora (en caso de type = payment).
        
        [ForeignKey("To_Account_Id")]
        public Account ToAccount { get; set; }

        [Column("idDeleted", TypeName = "BIT")]
        public bool IsDeleted { get; set; }
    }
}