using System;
using System.Linq;
using System.Linq.Expressions;

using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public class StyleRepository : RepositoryBase, IStyleRepository
    {
        public StyleRepository(IBrewHowContext context)
            : base(context)
        {
        }

        public IQueryable<StyleEntity> GetStyles()
        {
            return this.StyleEntities;
        }

        public StyleEntity GetStyle(int styleId)
        {
            return this.StyleEntities.FirstOrDefault(s => s.StyleId == styleId);
        }

        public StyleEntity GetStyleBySlug(string slug)
        {
            return this
                .StyleEntities
                .FirstOrDefault(
                    s => s.Slug == slug);
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

        private static readonly Expression<Func<Style, StyleEntity>> AsStyleEntity =
            s => new StyleEntity
            {
                StyleId = s.StyleId,
                Name = s.Name,
                Category = (CategoryEntity) s.Category,
                Slug = s.Slug
            };
    }
}