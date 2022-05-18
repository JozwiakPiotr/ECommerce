using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
        public string? ImageUrl { get; private set; }

        public Product(string name, double price, string description, int Quantity)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetPrice(price);
            SetDescription(description);
            SetQuantity(Quantity);
        }

        protected Product()
        {
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can not be empty or white space");
            Name = name;
        }

        public void SetPrice(double price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greaten than zero");
            Price = price;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description can not be empty or white space");
            Description = description;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity must be greater than or equal 0");
            Quantity = quantity;
        }
    }
}