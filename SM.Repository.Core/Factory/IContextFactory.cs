using SM.Entity.EFContext;

namespace SM.Repository.Core
{
    public interface IContextFactory
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        IDatabaseContext DbContext { get; }
    }
}
