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
			try
			{
                var allProducts = await _unitOfWork.Catalogues.GetAll();

                return allProducts
                        .OrderBy(p => p.Points)
                        .ToList();
			}
			catch (Exception ex)
			{
                throw new Exception(ex.Message);
			}
        }

        public async Task<Catalogue> GetProductById(int id)
        {
            try
            {
                var product = await _unitOfWork.Catalogues.GetById(id);

                return product;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Catalogue> CreateProduct(CatalogueProductDTO productdto, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var isAdmin = await _unitOfWork.UserRepository.IsAdmin(user); 
            if ( !isAdmin ) throw new AppException("Invalid credentials", HttpStatusCode.Unauthorized);
            if (productdto.Image == null || productdto.Points == null || productdto.ProductDescription == null)
            { throw new AppException("Missing required fields", HttpStatusCode.BadRequest); }
            var product = new Catalogue() { Image = productdto.Image, Points=productdto.Points, ProductDescription=productdto.ProductDescription  };
            await _unitOfWork.Catalogues.Add(product);
            return product;
        }
    }
}
