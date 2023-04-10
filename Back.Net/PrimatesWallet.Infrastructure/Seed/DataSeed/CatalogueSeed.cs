using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Seed.DataSeed
{
    public class CatalogueSeed : SeedData<Catalogue>
    {
        protected override IEnumerable<Catalogue> GetData()
        {
            return new List<Catalogue>() 
            {
                new Catalogue()
                {
                    ProductDescription = "Pelota de fútbol",
                    Image = "https://images.pexels.com/photos/39362/the-ball-stadion-football-the-pitch-39362.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 500
                },
                new Catalogue()
                {
                    ProductDescription = "Pelota de básquet",
                    Image = "https://images.pexels.com/photos/1089087/pexels-photo-1089087.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 600
                },
                new Catalogue()
                {
                    ProductDescription = "Reloj de pulsera",
                    Image = "https://images.pexels.com/photos/2113994/pexels-photo-2113994.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 470
                },
                new Catalogue()
                {
                    ProductDescription = "Reloj de pulsera digital",
                    Image = "https://images.pexels.com/photos/4471957/pexels-photo-4471957.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 550
                },                
                new Catalogue()
                {
                    ProductDescription = "Joystick PS5",
                    Image = "https://images.pexels.com/photos/13189290/pexels-photo-13189290.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 1500
                },                
                new Catalogue()
                {
                    ProductDescription = "Nintendo Switch Black",
                    Image = "https://images.pexels.com/photos/4523030/pexels-photo-4523030.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 6500
                },
                new Catalogue()
                {
                    ProductDescription = "Auriculares inalámbricos",
                    Image = "https://images.pexels.com/photos/1649771/pexels-photo-1649771.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 1200
                },                
                new Catalogue()
                {
                    ProductDescription = "TV Samsung 32",
                    Image = "https://samsungar.vtexassets.com/arquivos/ids/168470/UN32T4300AGCZB.jpg?v=637527893225330000",
                    Points = 3000
                },
                new Catalogue()
                {
                    ProductDescription = "Reloj de pulsera",
                    Image = "https://images.pexels.com/photos/2113994/pexels-photo-2113994.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 888
                },
                new Catalogue()
                {
                    ProductDescription = "Pelota de fútbol",
                    Image = "https://images.pexels.com/photos/39362/the-ball-stadion-football-the-pitch-39362.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 665
                },
                new Catalogue()
                {
                    ProductDescription = "Pelota de básquet",
                    Image = "https://images.pexels.com/photos/1089087/pexels-photo-1089087.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Points = 900
                },
            };
        }
    }
}
