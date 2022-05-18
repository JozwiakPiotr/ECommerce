using ECommerce.Models.DTO;
using ECommerce.Models.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace ECommerce.Models.Validators
{
    public class BrowseProductsValidator : AbstractValidator<BrowseProducts>
    {
        public BrowseProductsValidator()
        {
            var allowedPageSizes = new[] { 5, 10, 15 };
            var allowedColumns = new[] { "name", "description" };

            RuleFor(b => b.PageNumber)
                .GreaterThan(0);

            RuleFor(b => b.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                        context.AddFailure($"PageSize must be [{string.Join(',', allowedPageSizes)}]");
                });

            RuleFor(b => b.SortBy)
                .Custom((value, context) =>
                {
                    if (!allowedColumns.Contains(value.ToLower()) && !string.IsNullOrWhiteSpace(value))
                        context.AddFailure($"SortBy is optional or must be [{string.Join(',', allowedColumns)}]");
                });
        }
    }
}