using Microsoft.EntityFrameworkCore;


namespace PrimatesWallet.Infrastructure.Seed
{
    //Deben aplicarla todas las clases que necesiten popular la base de datos
    public abstract class SeedData<T> : ISeedData where T : class
    {
        public void Seed(DbContext context)
        {
            //Para que solamente se popule la base de datos si esta vacia
            if (!context.Set<T>().Any())
            {
                //Agregamos en rango para hacer una unica operacion I/O
                context.Set<T>().AddRange(GetData());

                //guardamos en la base de datos
                context.SaveChanges();
            }
        }

        //Metodo que implementan clases hijas con la data propia de sus clases
        protected abstract IEnumerable<T> GetData();
    }
}
