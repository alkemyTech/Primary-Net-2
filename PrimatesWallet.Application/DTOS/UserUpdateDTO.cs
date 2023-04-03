﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.DTOS
{
    public class UserUpdateDto
    {
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
        public int Points { get; set; }
        [Required]
        public int Rol_Id { get; set; }
    }
}
