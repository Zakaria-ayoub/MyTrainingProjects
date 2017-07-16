using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAuthorizations.Model
{
    public class Customer : IObjectWithState
    {
        public Customer()
        {
            Authorizations = new List<CustomerAuthorization>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public ObjectState ObjectState { get; set; }

        public virtual List<CustomerAuthorization> Authorizations { get; set; }
        public byte[] RowVersion { get; set; }


    }
}
