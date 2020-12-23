using System;
using System.Linq;

using BrewHow.Domain.Entities;

namespace BrewHow.Domain.Repositories
{
    public interface IStyleRepository
    {
        StyleEntity GetStyle(int styleId);
        IQueryable<StyleEntity> GetStyles();
        StyleEntity GetStyleBySlug(string slug);
    }
}
