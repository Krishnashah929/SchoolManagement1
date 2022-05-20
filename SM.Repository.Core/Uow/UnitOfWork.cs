using SM.Entity.EFContext;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Repository.Core
{
    /// <summary>
    /// Unit of work repository for crud operations
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// The database context.
        /// </summary>
        private IDatabaseContext databaseContext;

        /// <summary>
        /// The repository.
        /// </summary>
        private Dictionary<Type, object> repos;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public UnitOfWork(IContextFactory contextFactory)
        {
            this.databaseContext = contextFactory.DbContext;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// The Repository.
        /// </returns>
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this.repos == null)
            {
                this.repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!this.repos.ContainsKey(type))
            {
                this.repos[type] = new Repository<TEntity>(this.databaseContext);
            }

            return (IRepository<TEntity>)this.repos[type];
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>
        /// The number of objects in an Added, Modified, or Deleted state.
        /// </returns>
        public int Commit()
        {
            return this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns>
        /// The number of objects in an Added, Modified, or Deleted state asynchronously.
        /// </returns>
        public async Task<int> CommitAsync()
        {
            return await this.databaseContext.SaveChangesAsync(CancellationToken.None);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="bDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
                if (this.databaseContext != null)
                {
                    this.databaseContext.Dispose();
                    this.databaseContext = null;
                }
            }
        }
    }
}
