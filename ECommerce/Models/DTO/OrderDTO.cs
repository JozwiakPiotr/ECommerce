using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderedDate { get; set; }
        public double TotalPrice { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}