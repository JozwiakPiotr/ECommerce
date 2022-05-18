using ECommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public interface IAccountService
    {
        Task Register(RegisterUser registerDto);

        Task<string> Login(LoginUser loginDto);

        Task<UserDTO> GetById(Guid id);
    }
}