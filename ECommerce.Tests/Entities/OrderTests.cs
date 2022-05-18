using ECommerce.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Tests.Entities
{
    public class OrderTests
    {
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void SetUserId_ForInvalidValues_ThrowArgumentException(string id)
        {
            var productMock = new Mock<Product>();
            var order = new Order(Guid.NewGuid(), new List<Product> { productMock.Object }, Guid.NewGuid());

            Action action = () => order.SetUserId(Guid.Parse(id));

            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineData("78e49228-b8e0-48ce-99a8-2d585ea70734")]
        public void SetUserId_ForValidValues_UpdatesProperty(string id)
        {
            var productMock = new Mock<Product>();
            var order = new Order(Guid.NewGuid(), new List<Product> { productMock.Object }, Guid.NewGuid());

            order.SetUserId(Guid.Parse(id));

            order.UserId.Should().Be(id);
        }
    }
}