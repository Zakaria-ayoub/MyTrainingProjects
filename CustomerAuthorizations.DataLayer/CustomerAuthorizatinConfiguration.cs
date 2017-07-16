using CustomerAuthorizations.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAuthorizations.DataLayer
{
    public class CustomerAuthorizatinConfiguration : EntityTypeConfiguration<CustomerAuthorization>
    {
        public CustomerAuthorizatinConfiguration()
        {
            
            Property(ca => ca.AuthorizationYear).HasMaxLength(5).IsRequired();
            Property(ca => ca.AuthorizationNo).HasMaxLength(7).IsRequired();
            Property(ca => ca.AuthorizationLetter).HasMaxLength(2).IsRequired();

            Ignore(ca => ca.ObjectState);
            Property(ca => ca.RowVersion).IsRowVersion();

        }
    }
}
