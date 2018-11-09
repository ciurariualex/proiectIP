using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Pager<T> : PagedList<T>
        where T : class
    {
        public Pager(IEnumerable<T> items, int page, int pageSize)
            : base(items, page, pageSize)
        {
            int currentPage = 1;

            if (page > 0)
                currentPage = page;

            PageNumber = currentPage;
            PageSize = 10;
        }
    }
}
