using System.Data.Entity;
using CustomerAuthorizations.Model;


namespace CustomerAuthorizations.DataLayer
{
    public class AuthorizationsContext : DbContext
    {
        public AuthorizationsContext() : base("name=LocalConnection")
        {
            // LocalConnection , DefaultConnection
        }
        public IDbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAuthorization> CustomerAuthorizations { get; set; }
        public DbSet<AuthorizationType> AuthorizationTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfigurations());
            modelBuilder.Configurations.Add(new CustomerAuthorizatinConfiguration());

        }

    }
}
