using AutoMapper;
using ECommerce.Models.DTO;
using ECommerce.Entities;
using ECommerce.Exceptions;
using ECommerce.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(
            ECommerceDbContext dbContext,
            IMapper mapper)
            => (_dbContext, _mapper) =
            (dbContext, mapper);

        public async Task<Guid> AddAsync(AddProduct addProduct)
        {
            var product = new Product(
                addProduct.Name,
                addProduct.Price,
                addProduct.Description,
                addProduct.Quantity);

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeletetAsync(Guid productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new NotFoundException();

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductDTO> GetByIdAsync(Guid productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId) ??
                throw new NotFoundException();

            var prodcutDTO = _mapper.Map<ProductDTO>(product);
            return prodcutDTO;
        }

        public async Task<PagedResult<ProductDTO>> GetPagedAsync(BrowseProducts query)
        {
            var products = _dbContext.Products
                .Where(p => p.Name.Contains(query.SearchPhrase));

            if (query.SortDirection == SortDirection.ASC)
                products = products.OrderBy(p => p.Name);
            else
                products = products.OrderByDescending(p => p.Name);

            var pagedProducts = await products
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var pagedProductsDTO = _mapper.Map<List<ProductDTO>>(pagedProducts);

            return new PagedResult<ProductDTO>(
                pagedProductsDTO, query.PageNumber, query.PageSize, products.Count());
        }

        public async Task UpdateManyProductsQuantity(Dictionary<Guid, int> productsQuantities)
        {
            var products = await _dbContext.Products
                .Where(p => productsQuantities.Keys.Contains(p.Id)).ToListAsync();
            foreach (var product in products)
            {
                var value = productsQuantities[product.Id];
                product.SetQuantity(product.Quantity - value);
                _dbContext.Update(product);
            }

            //await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateQuantity(UpdateQuantity updateQuantity)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == updateQuantity.ProductId);
        }
    }
}