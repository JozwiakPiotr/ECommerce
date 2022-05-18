using AutoMapper;
using ECommerce.Entities;
using ECommerce.Exceptions;
using ECommerce.Infrastructure.EF;
using ECommerce.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class AccountService : IAccountService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public AccountService(
            ECommerceDbContext dbCotnext,
            IJwtService jwtService,
            IPasswordHasher<User> passwordHasher,
            IMapper mapper
            )
            => (_dbContext, _jwtService, _passwordHasher, _mapper) =
            (dbCotnext, jwtService, passwordHasher, mapper);

        public async Task<UserDTO> GetById(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id) ??
                throw new NotFoundException();
            var dto = _mapper.Map<UserDTO>(user);
            return dto;
        }

        public async Task<string> Login(LoginUser userLogin)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userLogin.Email) ??
                throw new BadRequestException("Invalid username or password");

            var confirmPassword =
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLogin.Password);

            if (confirmPassword is PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            var token = _jwtService.CreateToken(user);
            return token;
        }

        public async Task Register(RegisterUser userRegister)
        {
            var newUser = new User(
                userRegister.Email,
                userRegister.FirstName,
                userRegister.LastName,
                userRegister.Phone);

            var passwordHash = _passwordHasher.HashPassword(newUser, userRegister.Password);

            newUser.SetPasswordHash(passwordHash);
            newUser.SetRole("User");

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}