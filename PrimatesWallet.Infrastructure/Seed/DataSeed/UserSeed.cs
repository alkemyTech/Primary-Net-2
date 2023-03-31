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
                    Password = "Password",
                    Points = 0,
                    Rol_Id = 1,
                },
                new User()
                {
                    First_Name = "Lionel",
                    Last_Name = "Messi",
                    Email = "liomessichampion@hotmail.com",
                    Password = "liomessi",
                    Points = 0,
                    Rol_Id = 2,
                },                
                new User()
                {
                    First_Name = "Matias",
                    Last_Name = "Cespedes",
                    Email = "maticespedes@hotmail.com",
                    Password = "maticespedes",
                    Points = 0,
                    Rol_Id = 1,
                },
                new User()
                {
                    First_Name = "Matias",
                    Last_Name = "Boiero",
                    Email = "matiboiero@hotmail.com",
                    Password = "matiboiero",
                    Points = 0,
                    Rol_Id = 2,
                },
                new User()
                {
                    First_Name = "Luciano",
                    Last_Name = "Escobar",
                    Email = "luchoescobar@hotmail.com",
                    Password = "luchoescobar",
                    Points = 0,
                    Rol_Id = 1,
                }

            };
        }
    }
}
