using System;
using System.Linq;
using System.Linq.Expressions;

using BrewHow.Domain.Entities;
using BrewHow.Models;

namespace BrewHow.Domain.Repositories 
{
    public class StyleRepository : RepositoryBase
    {
        private BrewHowContext Context = new BrewHowContext();

        private static readonly Expression<Func<Style, StyleEntity>> AsStyleEntity =
            s => new StyleEntity
            {
                StyleId = s.StyleId,
                Name = s.Name,
                Category = (CategoryEntity) s.Category
            };

        public IQueryable<StyleEntity> GetStyles()
        {
            return this.StyleEntities;
        }

        public StyleEntity GetStyle(int styleId)
        {
            return this.StyleEntities.FirstOrDefault(s => s.StyleId == styleId);
        }

        private IQueryable<StyleEntity> StyleEntities
        {
            get
            {
                return this
                    .Context
                    .Styles
                    .OrderBy(s => s.Name)
                    .Select(AsStyleEntity);
            }
        }
    }
}