using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Core
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalItems = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
