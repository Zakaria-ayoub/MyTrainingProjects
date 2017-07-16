using CustomerAuthorizations.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema ;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerAuthorizations.Web.ViewModels
{
    public class CustomerViewModel : IObjectWithState
    {
        public CustomerViewModel()
        {
            CustomerAuthorizations = new List<CustomerAuthorizationViewModel>();
        }
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="الموقع: رجاء إدخال اسم العميل")]
        [Index(IsUnique =true )]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "الموقع: رجاء إدخال عنوان العميل")]
        public string Address { get; set; }
        [Required(ErrorMessage = "الموقع: رجاء إدخال رقم الموبايل")]
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        [EmailAddress(ErrorMessage ="الموقع: رحاء كتابة البريد الالكتروني بصيغة صحيحة")]
        public string Email { get; set; }
        public string Notes { get; set; }

        public List<CustomerAuthorizationViewModel> CustomerAuthorizations { get; set; }
        public string MessageToClient { get; set; }
        public ObjectState ObjectState { get; set; }

        public string RowVersion { get; set; }

    }
}