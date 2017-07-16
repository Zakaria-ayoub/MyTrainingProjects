using CustomerAuthorizations.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAuthorizations.DataLayer
{
    public class CustomerConfigurations :EntityTypeConfiguration<Customer>

    {
        public CustomerConfigurations()
        {
            Property(c => c.CustomerName).HasMaxLength(50).IsRequired();
            Property(c => c.Tel1).IsOptional();
            Property(c => c.Tel2).IsOptional();
            Property(c => c.Address).IsOptional();
            Property(c => c.Email).IsOptional();
            Property(c => c.Notes).IsOptional();
            Ignore(c => c.ObjectState);
            Property(c => c.RowVersion).IsRowVersion();


        }
    }
}
