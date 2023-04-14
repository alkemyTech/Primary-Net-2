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
using AutoMapper.Configuration.Conventions;
using AutoMapper;

namespace PrimatesWallet.Application.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CatalogueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Catalogue>> GetAllProducts()
        {
            var allProducts = await _unitOfWork.Catalogues.GetAll();
            if (allProducts == null) throw new AppException("Cant find Catalogue", HttpStatusCode.NotFound);
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

        public async Task<CatalogueProductDto> CreateProduct(CatalogueProductDto productdto, int userId)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            var isAdmin = _unitOfWork.Users.IsAdmin(user);

            if (!isAdmin) throw new AppException("Invalid credentials", HttpStatusCode.Unauthorized);
            if (productdto.Image == null || productdto.ProductDescription == null)
            { throw new AppException("Missing required fields", HttpStatusCode.BadRequest); }
            var product = new Catalogue() { Image = productdto.Image, Points = productdto.Points, ProductDescription = productdto.ProductDescription };
            var response = new CatalogueProductDto() { Image = productdto.Image, Points = productdto.Points, ProductDescription = productdto.ProductDescription };
            await _unitOfWork.Catalogues.Add(product);
            _unitOfWork.Save();
            return response;
        }

        public async Task<BasePaginateResponse<IEnumerable<Catalogue>>> CataloguePagination(int page, string url, int pageSize)
        {
            if (pageSize < 1) throw new AppException("pagination size cannot be less than 1", HttpStatusCode.BadRequest);
            if (page < 1) throw new AppException("page number cannot be less than 1", HttpStatusCode.BadRequest);

            int skip = (page - 1) * pageSize;

            var allProducts = await _unitOfWork.Catalogues.GetAll();

            if (allProducts is null) throw new AppException("There are not products", HttpStatusCode.NotFound);

            var numberOfPages = (int)Math.Ceiling((double)allProducts.ToList().Count / pageSize);

            if (page > numberOfPages) throw new AppException($"There are only {numberOfPages} pages for products listed by {pageSize}", HttpStatusCode.BadRequest);

            var resultProducts = allProducts.Skip(skip).Take(pageSize).ToList();

            return new BasePaginateResponse<IEnumerable<Catalogue>>()
            {
                Message = ReplyMessage.MESSAGE_QUERY,
                Page = page,
                PreviousPage = (page > 1) ? $"{url}?page={page - 1}" : null,
                Result = resultProducts,
                NextPage = (page < numberOfPages) ? $"{url}?page={page + 1}" : null,
                StatusCode = (int)HttpStatusCode.OK
            };
        }


        public async Task<bool> UpdateProduct(int id, CatalogueDTO productDTO)
        {
            if (id != productDTO.Id) throw new AppException("The ID provided in the request does not match the ID of the product sent.", HttpStatusCode.BadRequest);

            var dbProduct = await _unitOfWork.Catalogues.GetById(id);

            if (dbProduct is null) throw new AppException("The product with the specified ID could not be found.", HttpStatusCode.NotFound);

            _mapper.Map(productDTO, dbProduct);

            _unitOfWork.Catalogues.Update(dbProduct);

            var result = _unitOfWork.Save();

            if (result > 0) return true;

            return false;
        }
        public async Task<string> DeleteCatalogue(int catalogueId, int currentUser)
        {
            var user = await _unitOfWork.Users.GetById(currentUser);
            var isAdmin = _unitOfWork.Users.IsAdmin(user);
            if (!isAdmin) throw new AppException("Invalid Credentials", HttpStatusCode.Forbidden);

            var catalogue = await _unitOfWork.Catalogues.GetById(catalogueId);
            if (catalogue == null) throw new AppException($"Catalogue {catalogueId} not found", HttpStatusCode.NotFound);

            _unitOfWork.Catalogues.RealDelete(catalogue);
            _unitOfWork.Save();

            return $"Catalogue {catalogueId} deleted.";

        }
    }
}
