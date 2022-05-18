using ECommerce.Models.DTO;
using ECommerce.Models.Validators;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ECommerce.IntegratedTests.Validators
{
    public class BrowseProductValidatorTests
    {
        public static IEnumerable<object[]> ValidArticles()
        {
            var articles = new List<BrowseProducts> {
                 new BrowseProducts { PageNumber = 1, PageSize = 5, SortBy = "Name" },
                 new BrowseProducts { PageNumber = 10, PageSize = 10, SortBy = "Description" },
                 new BrowseProducts { PageNumber = 100, PageSize = 15 },
                 new BrowseProducts { PageNumber = 1, PageSize = 5 },
                 new BrowseProducts { }
            };

            return articles.Select(q => new object[] { q });
        }

        public static IEnumerable<object[]> InvalidArticles()
        {
            var articles = new List<BrowseProducts>()
            {
                 new BrowseProducts { PageNumber = 1, PageSize = 0, SortBy = "Name" },
                 new BrowseProducts { PageNumber = 10, PageSize = 10, SortBy = "Title" },
                 new BrowseProducts { PageNumber = 100, PageSize = 13 },
                 new BrowseProducts { PageNumber = 0, PageSize = 5 },
                 new BrowseProducts { PageNumber = 0, PageSize = 0 }
            };

            return articles.Select(q => new object[] { q });
        }

        [Theory]
        [MemberData(nameof(ValidArticles))]
        public void Validate_ForCorrectModel_ReturnsSuccess(BrowseProducts browse)
        {
            var validator = new BrowseProductsValidator();

            var result = validator.TestValidate(browse);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(InvalidArticles))]
        public void Validate_ForIncorrectModel_ReturnsSuccess(BrowseProducts browse)
        {
            var validator = new BrowseProductsValidator();

            var result = validator.TestValidate(browse);

            result.ShouldHaveAnyValidationError();
        }
    }
}