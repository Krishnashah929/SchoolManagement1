using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Entity.EFContext
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// set for the specified entity
        /// </returns>
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// number of state entries interacted with database
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="bConfirmAllTransactions">if set to <c>true</c> [confirm all transactions].</param> //04.01.2022, Parth
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// number of state entries interacted with database
        /// </returns>
        Task<int> SaveChangesAsync(bool bConfirmAllTransactions, CancellationToken cancellationToken); //04.01.2022, Parth

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>
        /// number of state entries interacted with database
        /// </returns>
        int SaveChanges();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="bConfirmAllTransactions">if set to <c>true</c> [confirm all transactions].</param> //04.01.2022, Parth
        /// <returns>
        /// number of state entries interacted with database
        /// </returns>
        int SaveChanges(bool bConfirmAllTransactions); //04.01.2022, Parth

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();
    }
}
