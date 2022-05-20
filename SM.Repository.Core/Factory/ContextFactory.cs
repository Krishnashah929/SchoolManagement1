using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SM.Entity.EFContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Repository.Core
{
    public class ContextFactory : IContextFactory
    {
        /// <summary>
        /// The connection options.
        /// </summary>
        private readonly IServiceCollection _services;

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextFactory"/> class.
        /// </summary>
        /// <param name="connectionOptions">The connection options.</param>
        public ContextFactory(IServiceCollection services,IConfiguration configuration)
        {
            Configuration = configuration;
            this._services = services;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public IDatabaseContext DbContext => new SMContext(this.GetDataContext().Options);

        /// <summary>
        /// Gets the data context.
        /// </summary>
        /// <returns>DB Context Options Builder</returns>
        private DbContextOptionsBuilder<SMContext> GetDataContext()
        {
            
            var sqlConnectionBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SchoolManagementContext"));
            var contextOptionsBuilder = new DbContextOptionsBuilder<SMContext>();
            contextOptionsBuilder.UseSqlServer(sqlConnectionBuilder.ConnectionString);
            return contextOptionsBuilder;
        }

        /// <summary>
        /// Validates the default connection.
        /// </summary>
        /// <exception cref="ArgumentNullException">Default Connection</exception>
        //private void ValidateDefaultConnection()
        //{
        //    if (string.IsNullOrEmpty(this.connectionOptions.Value.DefaultConnection))
        //    {
        //        throw new ArgumentNullException(nameof(this.connectionOptions.Value.DefaultConnection));
        //    }
        //}
    }
}
