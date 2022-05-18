using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.DTO
{
    public class AddOrder
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AdressId { get; set; }

        [Required]
        public List<ProductDTO> Products { get; set; }
    }
}