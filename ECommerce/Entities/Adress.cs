using ECommerce.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class Adress
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public List<Order> Orders { get; private set; }

        public Adress(Guid userId, string city, string street, string houseNumber,
            string postalCode, string country)
        {
            Id = Guid.NewGuid();
            SetUserId(userId);
            SetCity(city);
            SetStreet(street);
            SetHouseNumber(houseNumber);
            SetPostalCode(postalCode);
            SetCountry(country);
        }

        protected Adress()
        {
        }

        public void SetUserId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("UserId can not be empty");
            UserId = id;
        }

        public void SetCity(string city)
        {
            if (city.IsEmpty())
                throw new ArgumentException("City can not be null or white space");
            City = city;
        }

        public void SetStreet(string street)
        {
            if (street.IsEmpty())
                throw new ArgumentException("Street can not be null or white space");
            Street = street;
        }

        public void SetHouseNumber(string houseNumber)
        {
            if (houseNumber.IsEmpty())
                throw new ArgumentException("HouseNumber can not be null or white space");
            HouseNumber = houseNumber;
        }

        public void SetPostalCode(string postalCode)
        {
            if (postalCode.IsEmpty())
                throw new ArgumentException("PostalCode can not be null or white space");
            PostalCode = postalCode;
        }

        public void SetCountry(string country)
        {
            if (country.IsEmpty())
                throw new ArgumentException("Country can not be null or white space");
            Country = country;
        }
    }
}