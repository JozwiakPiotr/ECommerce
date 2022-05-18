using ECommerce.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace ECommerce.Tests.Entities
{
    public class UserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("@test.com")]
        [InlineData("test@.com")]
        [InlineData("test@test")]
        [InlineData(null)]
        public void SetEmail_ForInvalidValue_ShouldThrowArgumentException(string email)
        {
            var user = new User("test@test.com", "test", "test", "test");

            Action action = () => user.SetEmail(email);

            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineData("test.test@test.com")]
        [InlineData("test-test@test.com")]
        [InlineData("test123@test.com")]
        [InlineData("t@t.pl")]
        [InlineData("test@test.com")]
        public void SetEmail_ForValidValue_UpdatesProperty(string email)
        {
            var user = new User("test@test.com", "test", "test", "test");

            user.SetEmail(email);

            user.Email.Should().Be(email);
        }
    }
}