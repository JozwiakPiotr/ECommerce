using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.DTO
{
    public class UpdateQuantity
    {
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }
    }
}