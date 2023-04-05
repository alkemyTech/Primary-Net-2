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

        /// <summary>
        /// Creates a new product in the catalog.
        /// </summary>
        /// <remarks>
        /// Creates a new product in the catalog using the provided product data, only an admin user can create a product.
        /// </remarks>
        /// <param name="productdto">The product data to create.</param>
        /// <param name="userId">The user ID that is creating the product.</param>
        /// <returns>The created product data.</returns>
        Task<CatalogueProductDto> CreateProduct(CatalogueProductDto productdto, int userId);

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
        Task<BasePaginateResponse<IEnumerable<Catalogue>>> CataloguePagination(int page, string url, int pageSize);

        /// <summary>
        /// Retrieves all products from the catalogue and orders them by points.
        /// </summary>
        /// <returns>A list of all catalogue products ordered by points.</returns>
        Task<List<Catalogue>> GetAllProducts();

        /// <summary>
        /// Get a specific product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product matching the specified ID.</returns>
        Task<Catalogue> GetProductById(int id);

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="product">An instance of Catalogue containing the updated properties of the product.</param>
        /// <returns>True if the update operation was completed successfully, otherwise false.</returns>
        /// <exception cref="AppException">Thrown when the provided ID does not match the ID of the product sent or the product with the specified ID cannot be found.</exception>
        Task<bool> UpdateProduct(int id, CatalogueDTO productDTO);
        Task<string> DeleteCatalogue(int catalogueId, int currentUser);
    }
}
