using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AdressId { get; private set; }
        public DateTime OrderedDate { get; private set; }
        public List<Product> Products { get; private set; }

        public double TotalPrice
        { get => Products.Sum(p => p.Price); private set { } }

        public Order(Guid userId, List<Product> products, Guid adressId)
        {
            Id = Guid.NewGuid();
            OrderedDate = DateTime.UtcNow;
            SetProducts(products);
            SetUserId(userId);
            SetAdressId(adressId);
        }

        protected Order()
        {
        }

        public void SetProducts(List<Product> products)
        {
            if (products.Count <= 0)
                throw new ArgumentException("Products can not be empty collection");
            Products = products;
        }

        public void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId can not be empty");
            UserId = userId;
        }

        public void SetAdressId(Guid adressId)
        {
            if (adressId == Guid.Empty)
                throw new ArgumentException("AdressId can not be empty");
            AdressId = adressId;
        }
    }
}