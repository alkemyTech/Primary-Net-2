using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Helpers;
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
        Task<BasePaginateResponse<IEnumerable<Catalogue>>> CataloguePagination(int page, string url, int pageSize);
        Task<Catalogue> CreateProduct(CatalogueProductDTO productdto, int userId);
        Task<List<Catalogue>> GetAllProducts();
        Task<Catalogue> GetProductById(int id);
        Task<bool> UpdateProduct(int id, CatalogueDTO productDTO);
    }
}
