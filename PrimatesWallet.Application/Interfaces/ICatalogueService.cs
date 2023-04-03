using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Interfaces
{
    public interface ICatalogueService
    {
        Task<Catalogue> CreateProduct(CatalogueProductDto productdto, int userId);
        Task<List<Catalogue>> GetAllProducts();
        Task<Catalogue> GetProductById(int id);
    }
}
