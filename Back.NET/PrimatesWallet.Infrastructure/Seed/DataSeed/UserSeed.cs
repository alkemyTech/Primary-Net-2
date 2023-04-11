using BCrypt.Net;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Seed.DataSeed
{
    public class UserSeed : SeedData<User>
    {
        protected override IEnumerable<User> GetData()
        {
            return new List<User>()
            {
                new User()
                {
                    First_Name = "Samuel",
                    Last_Name = "Tribulo",
                    Email = "samueltribulo@hotmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password",10),
                    Points = 0,
                    Rol_Id = 1,
                },
                new User()
                {
                    First_Name = "Lionel",
                    Last_Name = "Messi",
                    Email = "liomessichampion@hotmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("liomessi",10),
                    Points = 0,
                    Rol_Id = 2,
                },                
                new User()
                {
                    First_Name = "Matias",
                    Last_Name = "Cespedes",
                    Email = "maticespedes@hotmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("maticespedes",10),
                    Points = 0,
                    Rol_Id = 1,
                },
                new User()
                {
                    First_Name = "Matias",
                    Last_Name = "Boiero",
                    Email ="matiboiero@hotmail.com",
                    Password =  BCrypt.Net.BCrypt.HashPassword("matiboiero",10),
                    Points = 0,
                    Rol_Id = 2,
                },
                new User()
                {
                    First_Name = "Luciano",
                    Last_Name = "Escobar",
                    Email = "luchoescobar@hotmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("luchoescobar",10),
                    Points = 0,
                    Rol_Id = 1,
                }

            };
        }
    }
}
