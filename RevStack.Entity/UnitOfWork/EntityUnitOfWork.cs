using System;
using System.Threading.Tasks;
using System.Data.Entity;
using RevStack.Pattern;

namespace RevStack.Entity
{
    public class EntityUnitOfWork<TEntity, TKey> : IUnitOfWork<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private DbContext _context;
        public EntityUnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public Task CommitAsync()
        {
            Commit();
            return Task.FromResult(true);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_context !=null)
                    {
                        _context.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
