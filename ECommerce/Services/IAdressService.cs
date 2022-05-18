using ECommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public interface IAdressService
    {
        Task<Guid> AddAsync(AddAdress addAdress);

        Task<List<AdressDTO>> GetAsync(Guid userId);

        Task DeleteAsync(Guid adressId);
    }
}