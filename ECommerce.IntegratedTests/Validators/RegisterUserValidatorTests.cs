using ECommerce.Entities;
using ECommerce.Infrastructure.EF;
using ECommerce.Models.DTO;
using ECommerce.Models.Validators;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace ECommerce.IntegratedTests.Validators
{
    public class RegisterUserValidatorTests
    {
        private readonly ECommerceDbContext _dbContext;

        public RegisterUserValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<ECommerceDbContext>();
            builder.UseInMemoryDatabase("test");

            _dbContext = new ECommerceDbContext(builder.Options);
        }

        [Theory]
        [MemberData(nameof(ValidModels))]
        public void Validate_ForCorrectModel_ReturnsSuccess(RegisterUser registerDto)
        {
            //arrange
            var validator = new UserRegisterValidator(_dbContext);

            //act
            var result = validator.TestValidate(registerDto);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(InvalidModels))]
        public void Validate_ForIncorrectModel_ReturnsFailure(RegisterUser registerDto)
        {
            var validator = new UserRegisterValidator(_dbContext);

            var result = validator.TestValidate(registerDto);

            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Validate_ForExistingEmailInDbContext_ReturnsFailure()
        {
            //arrange
            var registerDto = new RegisterUser()
            {
                Email = "900@test.com",
                FirstName = "test",
                LastName = "test",
                Password = "test123",
                ConfirmPassword = "test123",
                Phone = "phone"
            };
            var validator = new UserRegisterValidator(_dbContext);
            Seed();

            //act
            var result = validator.TestValidate(registerDto);

            //assert
            result.ShouldHaveAnyValidationError();
        }

        public static IEnumerable<object[]> ValidModels()
        {
            yield return new object[] {
                new RegisterUser()
                {
                    Email = "test@test.com",
                    FirstName = "test",
                    LastName ="test",
                    Password = "test123",
                    ConfirmPassword = "test123",
                    Phone = "phone"
                }
            };
        }

        public static IEnumerable<object[]> InvalidModels()
        {
            yield return new object[] {
                new RegisterUser()
                {
                    Email = "test@test.com",
                    FirstName = "test",
                    LastName = "test",
                    Password = "test123",
                    ConfirmPassword = "test124",
                    Phone = "phone"
                }
            };
            yield return new object[] {
                new RegisterUser()
                {
                    LastName = "test",
                    FirstName = "test",
                    Password = "test123",
                    ConfirmPassword = "test123",
                    Phone = "phone"
                }
            };
        }

        public void Seed()
        {
            var user = new User("900@test.com", "test", "test", "test");
            user.SetPasswordHash("hash");
            user.SetRole("user");
            var users = new User[]
            {
                user
            };

            _dbContext.Users.AddRange(users);

            _dbContext.SaveChanges();
        }
    }
}