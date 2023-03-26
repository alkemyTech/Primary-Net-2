using Microsoft.EntityFrameworkCore;


namespace PrimatesWallet.Infrastructure.Seed
{
    //Deben aplicarla todas las clases que necesiten popular la base de datos,
    //nos va a permitir aplicar polimorfismo
    public abstract class SeedData<T> where T : class
    {
        public void Seed(DbContext context)
        {
            //Para que solamente se popule la base de datos si esta vacia
            if (!context.Set<T>().Any())
            {
                //Agregamos en rango para hacer una unica operacion I/O
                context.Set<T>().AddRange();

                //guardamos en la base de datos
                context.SaveChanges();
            }
        }

        //Metodo que implementan clases hijas con la data propia de sus clases
        protected abstract IEnumerable<T> GetData();
    }
}
