using Microsoft.EntityFrameworkCore;

namespace SM.Entity.EFContext
{
    public class SMContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// initializes a new instance of DbContext class.
        /// </summary>
        /// <param name="options"></param>
        public SMContext(DbContextOptions<SMContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
