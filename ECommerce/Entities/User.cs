using ECommerce.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class User
    {
        private readonly Regex _emailRegex = new Regex(
            @"^(([a-z0-9]\.?-?_?[a-z0-9])|[a-z0-9])+@[a-z]+\.[a-z]{2,3}$",
            RegexOptions.IgnoreCase);

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Role { get; private set; }
        public List<Adress> Adresses { get; private set; }
        public List<Order> Orders { get; private set; }

        public User(string email, string firstName, string lastName, string phone)
        {
            Id = Guid.NewGuid();
            SetEmail(email);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetPhone(phone);
        }

        protected User()
        {
        }

        public void SetEmail(string email)
        {
            if (email.IsEmpty())
                throw new ArgumentException("Email can not be null or white space");

            if (!_emailRegex.IsMatch(email))
                throw new ArgumentException($"Email invalid value: \"{email}\"");

            Email = email;
        }

        public void SetFirstName(string firstName)
        {
            if (firstName.IsEmpty())
                throw new ArgumentException("FirstName can not be null or white space");

            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (lastName.IsEmpty())
                throw new ArgumentException("LastName can not be null or white space");

            LastName = lastName;
        }

        public void SetPasswordHash(string hash)
        {
            if (hash.IsEmpty())
                throw new ArgumentException("PasswordHash can not be null or white space");
            PasswordHash = hash;
        }

        public void SetPhone(string phone)
        {
            if (phone.IsEmpty())
                throw new ArgumentException("Phone can not be null or white space");
            Phone = phone;
        }

        public void SetRole(string role)
        {
            if (Phone.IsEmpty())
                throw new ArgumentException("Role can not be null or white space");
            Role = role;
        }
    }
}