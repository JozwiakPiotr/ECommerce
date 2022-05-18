using ECommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public interface IProductService
    {
        Task<Guid> AddAsync(AddProduct addProduct);

        Task DeletetAsync(Guid productId);

        Task<ProductDTO> GetByIdAsync(Guid productId);

        Task<PagedResult<ProductDTO>> GetPagedAsync(BrowseProducts query);

        Task UpdateManyProductsQuantity(Dictionary<Guid, int> productsQuantities);

        Task UpdateQuantity(UpdateQuantity updateQuantity);
    }
}