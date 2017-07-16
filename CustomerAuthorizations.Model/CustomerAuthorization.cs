using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAuthorizations.Model
{
    public class CustomerAuthorization : IObjectWithState
    {
        public int Id { get; set; }
        public string AuthorizationNo { get; set; }
        public string AuthorizationLetter { get; set; }
        public string AuthorizationYear { get; set; }
        public string AuthorizationOffice { get; set; }

        public int AuthorizationTypeId { get; set; }
        public AuthorizationType AuthorizationType { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ObjectState ObjectState { get; set; }

        public byte[] RowVersion { get; set; }



    }
}
