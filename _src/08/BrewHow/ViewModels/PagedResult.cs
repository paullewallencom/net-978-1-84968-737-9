using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BrewHow.ViewModels
{
    public class PagedResult<TFrom, TTo> : List<TTo>, ITypedPagedResult<TTo>
    {
        private const int PageSize = 10;

        public PagedResult(IQueryable<TFrom> query, int page, Func<TFrom, TTo> map)
        {
            this.Page = page;
            this.TotalPages = (int) Math.Ceiling(query.Count() / (double)PageSize);

            this.AddRange(query.Skip(page * PageSize).Take(PageSize).AsEnumerable().Select(map));
        }

        public int Page { get; private set; }
        public int TotalPages { get; private set; }
    }
}