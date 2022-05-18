using AutoMapper;
using ECommerce.Entities;
using ECommerce.Exceptions;
using ECommerce.Infrastructure.EF;
using ECommerce.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class AdressService : IAdressService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AdressService(ECommerceDbContext dbContext, IMapper mapper)
            => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<Guid> AddAsync(AddAdress addAdress)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == addAdress.UserId);

            var adress = new Adress(
                addAdress.UserId,
                addAdress.City,
                addAdress.Street,
                addAdress.HouseNumber,
                addAdress.PostalCode,
                addAdress.Country);

            _dbContext.Adresses.Add(adress);
            await _dbContext.SaveChangesAsync();

            return adress.Id;
        }

        public async Task DeleteAsync(Guid adressId)
        {
            var adress = _dbContext.Adresses.FirstOrDefault(a => a.Id == adressId) ??
                throw new NotFoundException();

            _dbContext.Adresses.Remove(adress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<AdressDTO>> GetAsync(Guid userId)
        {
            var adresses = await _dbContext.Adresses.Where(a => a.UserId == userId).ToListAsync() ??
                throw new NotFoundException();

            var adressesDTO = _mapper.Map<List<AdressDTO>>(adresses);
            return adressesDTO;
        }
    }
}