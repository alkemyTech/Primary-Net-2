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
using PrimatesWallet.Application.DTOS.Pagination;
using AutoMapper.Configuration.Conventions;

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

        /// <summary>
        /// This method looks for all the products in the database, then takes the products corresponding to the page number that is passed to it by parameter.
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">number of products per page</param>
        /// <param name="url">request url</param>
        /// <exception cref="AppException">pagination size cannot be less than 1</exception>
        /// <exception cref="AppException">page number cannot be less than 1</exception>
        /// <exception cref="AppException">this exception tests if there are products in DB</exception>
        /// <exception cref="AppException">this exception evaluates if the page number passed by parameter is greater than the number of pages</exception>
        /// <returns>An object with a message, the number of actual page, the url of the previous page, the results, the url of the next page and the status code.</returns>
        public async Task<BasePaginateResponse<IEnumerable<Catalogue>>> CataloguePagination(int page, string url, int pageSize)
        {
            if (pageSize < 1) throw new AppException("pagination size cannot be less than 1", HttpStatusCode.BadRequest);
            if (page < 1) throw new AppException("page number cannot be less than 1", HttpStatusCode.BadRequest);

            int skip = (page - 1) * pageSize;

            var allProducts = await _unitOfWork.Catalogues.GetAll();

            if (allProducts is null) throw new AppException("There are not products", HttpStatusCode.NotFound);

            var numberOfPages = (int)Math.Ceiling( (double)allProducts.ToList().Count / pageSize );

            if (page > numberOfPages) throw new AppException($"There are only {numberOfPages} pages for products listed by {pageSize}", HttpStatusCode.BadRequest);

            var resultProducts = allProducts.Skip(skip).Take(pageSize).ToList();

            return new BasePaginateResponse<IEnumerable<Catalogue>>() {
                Message = ReplyMessage.MESSAGE_QUERY,
                Page = page,
                PreviousPage = (page > 1) ? $"{url}?page={page - 1}" : null,
                Result = resultProducts,
                NextPage = ( page < numberOfPages ) ? $"{url}?page={page + 1}" : null,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
