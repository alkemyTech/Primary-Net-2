using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PrimatesWallet.Application.DTOS;

namespace PrimatesWallet.Application.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogueService(IUnitOfWork unitOfWork) 
		{
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Catalogue>> GetAllProducts()
        {
            var allProducts = await _unitOfWork.Catalogues.GetAll();
            if ( allProducts == null) throw new AppException("Cant find Catalogue", HttpStatusCode.NotFound);
            return allProducts
                   .OrderBy(p => p.Points)
                   .ToList();
        }

        public async Task<Catalogue> GetProductById(int id)
        {
            var product = await _unitOfWork.Catalogues.GetById(id);
            if (product == null) throw new AppException("Cant find Product", HttpStatusCode.NotFound);
            return product;
        }

        public async Task<Catalogue> CreateProduct(CatalogueProductDto productdto, int userId)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            var isAdmin = _unitOfWork.Users.IsAdmin(user); 
            
            if ( !isAdmin ) throw new AppException("Invalid credentials", HttpStatusCode.Unauthorized);
            if (productdto.Image == null || productdto.ProductDescription == null)
            { throw new AppException("Missing required fields", HttpStatusCode.BadRequest); }
            var product = new Catalogue() { Image = productdto.Image, Points=productdto.Points, ProductDescription=productdto.ProductDescription  };
            await _unitOfWork.Catalogues.Add(product);
            return product;
        }
    }
}
