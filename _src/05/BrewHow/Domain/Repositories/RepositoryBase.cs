using System;

using BrewHow.Models;

namespace BrewHow.Domain.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        public RepositoryBase()
        {
            this.Context = new BrewHowContext();
        }

        public BrewHowContext Context
        {
            get;
            private set;
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}