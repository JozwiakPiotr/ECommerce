using ECommerce.Infrastructure.EF;
using ECommerce.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Validators
{
    public class UserRegisterValidator : AbstractValidator<RegisterUser>
    {
        public UserRegisterValidator(ECommerceDbContext dbContext)
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password);

            RuleFor(u => u.FirstName)
                .NotEmpty();

            RuleFor(u => u.FirstName)
                .NotEmpty();

            RuleFor(u => u.Phone)
                .NotEmpty();

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.Email)
                .Custom((value, validationContext) =>
                {
                    if (dbContext.Users.Any(u => u.Email == value))
                    {
                        validationContext.AddFailure("Email", "The Email is taken");
                    }
                });
        }
    }
}