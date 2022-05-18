using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.DTO
{
    public class BrowseProducts
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string SearchPhrase { get; set; } = "";
        public string SortBy { get; set; } = "";
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
    }
}