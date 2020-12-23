using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewHow.Models
{
    public class PagedResult<T> : List<T>, IPagedResult
    {
        private const int PageSize = 10;

        public PagedResult(IQueryable<T> query, int page)
        {
            this.Page = page;
            this.TotalPages = (int) Math.Ceiling(query.Count() / (double)PageSize);

            this.AddRange(query.Skip(page * PageSize).Take(PageSize));
        }

        public int Page { get; private set; }
        public int TotalPages { get; private set; }
    }
}