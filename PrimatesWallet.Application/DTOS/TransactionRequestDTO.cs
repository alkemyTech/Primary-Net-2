﻿using PrimatesWallet.Application.Middleware;
using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class TransactionRequestDto
    {
        [Required]
        [Range(100, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than 100.")]
        public decimal Amount { get; set; }

        [Required]
        //[RegularExpression(@"^[a-zA-Z0-9]+$")]
        //[StringLength(50, MinimumLength = 10, ErrorMessage = "Concept length cannot exceed 50 characters.")]
        public string Concept { get; set; }

        [Required]
        //[EnumDataType(typeof(TransactionType), ErrorMessage = "Invalid transaction type.")]
        public string Type { get; set; }

        [Required]
        public int Account_Id { get; set; }

        [Required]
        public int To_Account_Id { get; set; }
    }
}
