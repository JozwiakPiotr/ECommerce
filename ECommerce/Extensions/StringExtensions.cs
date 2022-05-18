using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this String str)
            => String.IsNullOrWhiteSpace(str);
    }
}