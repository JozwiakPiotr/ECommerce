using AutoMapper;
using ECommerce.Authorization;
using ECommerce.Entities;
using ECommerce.Exceptions;
using ECommerce.Infrastructure.EF;
using ECommerce.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public OrderService(ECommerceDbContext dbContext, IMapper mapper,
            IProductService productService, IAuthorizationService authorizationService,
            IUserContextService userContextService)

            => (_dbContext, _mapper, _productService, _authorizationService, _userContextService) =
            (dbContext, mapper, productService, authorizationService, userContextService);

        public async Task<Guid> AddAsync(AddOrder addOrder)
        {
            var products = addOrder.Products.Select(p
                => new Product(p.Name, p.Price, p.Description, p.Quantity)).ToList();
            var order = new Order(addOrder.UserId, products, addOrder.AdressId);

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(_userContextService.User, order, new ResourceOwnerRequirement());

            var x = _userContextService.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            bool y = x == order.UserId.ToString();

            if (!authorizationResult.Succeeded)
                throw new ForbidenException();

            var productsQuantities = addOrder.Products.ToDictionary(p => p.Id, p => p.Quantity);
            await _productService.UpdateManyProductsQuantity(productsQuantities);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task<List<OrderDTO>> GetAsync()
        {
            var orders = await _dbContext.Orders.ToListAsync();

            var ordersDTO = _mapper.Map<List<OrderDTO>>(orders);
            return ordersDTO;
        }
    }
}