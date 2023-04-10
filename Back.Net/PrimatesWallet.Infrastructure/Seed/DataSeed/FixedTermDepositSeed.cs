using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Infrastructure.Seed.DataSeed
{
    public class FixedTermDepositSeed : SeedData<FixedTermDeposit>
    {
        protected override IEnumerable<FixedTermDeposit> GetData()
        {
            //Se instancian direfentes plazos fijos apuntados a diferentes cuentas y usuarios
            return new List<FixedTermDeposit>()
            {
                new FixedTermDeposit()
                {
                   AccountId=1,
                   Amount=50000,
                   Creation_Date=new DateTime(2023,1, 10),
                   Closing_Date=new DateTime(2023,2, 11),
                },
                new FixedTermDeposit()
                {
                   AccountId=2,
                   Amount=85800,
                   Creation_Date=new DateTime(2023,1, 17),
                   Closing_Date=new DateTime(2023,2, 18),
                },
                new FixedTermDeposit()
                {
                   AccountId=3,
                   Amount=23965,
                   Creation_Date=new DateTime(2023,2, 10),
                   Closing_Date=new DateTime(2023,3, 11),
                },
                 new FixedTermDeposit()
                {
                   AccountId=4,
                   Amount=105000,
                   Creation_Date=new DateTime(2023,1, 20),
                   Closing_Date=new DateTime(2023,2, 20),
                },
                  new FixedTermDeposit()
                {
                   AccountId=5,
                   Amount=200000,
                   Creation_Date=new DateTime(2023,2, 25),
                   Closing_Date=new DateTime(2028,3, 24),
                },
                    new FixedTermDeposit()
                {
                   AccountId=5,
                   Amount=200000,
                   Creation_Date=new DateTime(2023,2, 25),
                   Closing_Date=new DateTime(2024,3, 24),
                },
                      new FixedTermDeposit()
                {
                   AccountId=5,
                   Amount=200000,
                   Creation_Date=new DateTime(2023,2, 25),
                   Closing_Date=new DateTime(2023,5, 24),
                }
            };
        }
    }
}
