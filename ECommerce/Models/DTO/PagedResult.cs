using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.DTO
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> items, int pageNumber, int pageSize, int totalItems)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int ItemFrom { get => PageSize * (PageNumber - 1) + 1; }
        public int ItemTo { get => PageSize * PageNumber; }
        public int TotalPages { get => (int)Math.Ceiling(TotalItems / (double)PageSize); }

        public List<T> Items { get; set; }
    }
}