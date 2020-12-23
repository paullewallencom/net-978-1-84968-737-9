using System;

using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public abstract class RepositoryBase
    {
        public RepositoryBase(IBrewHowContext context)
        {
            this.Context = context;
        }

        public IBrewHowContext Context
        {
            get;
            private set;
        }
    }
}