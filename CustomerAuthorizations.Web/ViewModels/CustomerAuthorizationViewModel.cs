using CustomerAuthorizations.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerAuthorizations.Web.ViewModels
{
    public class CustomerAuthorizationViewModel : IObjectWithState
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "الموقع: رجاء إدخال رقم التوكيل")]
        public string AuthorizationNo { get; set; }
        [Required(ErrorMessage = "الموقع: رجاء إدخال الحرف")]
        public string AuthorizationLetter { get; set; }

        [Required(ErrorMessage = "الموقع: رجاء إدخال السنة")]
        public string AuthorizationYear { get; set; }
        [Required(ErrorMessage = "الموقع: رجاء كتابة مكتب التوثيق")]
        public string AuthorizationOffice { get; set; }

        public int AuthorizationTypeId { get; set; }
        public int CustomerId { get; set; }

        public ObjectState ObjectState { get; set; }

        public string RowVersion { get; set; }





    }
}