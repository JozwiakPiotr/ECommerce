using ECommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public interface IOrderService
    {
        Task<Guid> AddAsync(AddOrder addOrder);

        Task<List<OrderDTO>> GetAsync();
    }
}