using CustomerAuthorizations.DataLayer;
using CustomerAuthorizations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerAuthorizations.Web.ViewModels
{
    public static class Helpers
    {
        public static CustomerViewModel CreateCustomerViewModelFromCustomerModel(Customer customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();

            customerViewModel.CustomerId = customer.CustomerId;
            customerViewModel.CustomerName = customer.CustomerName;
            customerViewModel.Address = customer.Address;
            customerViewModel.Email = customer.Email;
            customerViewModel.Notes = customer.Notes;
            customerViewModel.Tel1 = customer.Tel1;
            customerViewModel.Tel2 = customer.Tel2;
            customerViewModel.RowVersion = Convert.ToBase64String(customer.RowVersion);
            
            customerViewModel.ObjectState = customer.ObjectState;

            foreach (CustomerAuthorization  item in customer.Authorizations   )
            {
                CustomerAuthorizationViewModel customerAuthorizationViewModel = new CustomerAuthorizationViewModel();
                customerAuthorizationViewModel.Id = item.Id;
                customerAuthorizationViewModel.AuthorizationNo = item.AuthorizationNo;
                customerAuthorizationViewModel.AuthorizationLetter = item.AuthorizationLetter;
                customerAuthorizationViewModel.AuthorizationYear = item.AuthorizationYear;
                customerAuthorizationViewModel.AuthorizationOffice = item.AuthorizationOffice;
                customerAuthorizationViewModel.AuthorizationTypeId = item.AuthorizationTypeId;
                customerAuthorizationViewModel.CustomerId = item.CustomerId;
                customerViewModel.ObjectState = ObjectState.Unchanged;

                customerAuthorizationViewModel.RowVersion = Convert.ToBase64String(item.RowVersion);
                customerViewModel.CustomerAuthorizations.Add(customerAuthorizationViewModel);
            }
            return customerViewModel;
        }

        public static AuthorizationType GetAuthorizationType(int authorizationTypeId)
        {
            AuthorizationsContext _authorizationsContext= new AuthorizationsContext();

            AuthorizationType authorization = _authorizationsContext.AuthorizationTypes.Find(authorizationTypeId);
            return authorization;

        }

        public static Customer CreateCustomerModelFromCustomerViewModel(CustomerViewModel customerViewModel)
        {
            Customer customer= new Customer();

            customer.CustomerId = customerViewModel.CustomerId;
            customer.CustomerName = customerViewModel.CustomerName;
            customer.Address = customerViewModel.Address;
            customer.Email = customerViewModel.Email;
            customer.Notes = customerViewModel.Notes;
            customer.Tel1 = customerViewModel.Tel1;
            customer.Tel2 = customerViewModel.Tel2;
            
            customer.ObjectState = customerViewModel.ObjectState;
            

            if (customerViewModel.RowVersion == null)
            {
                customer.RowVersion = Convert.FromBase64String(string.Empty);
            }
            else
            {
                customer.RowVersion = Convert.FromBase64String(customerViewModel.RowVersion);
            }

            int tmpAuthorizationId = -1;
            foreach (CustomerAuthorizationViewModel item in customerViewModel.CustomerAuthorizations)
            {
                CustomerAuthorization customerAuthorization = new CustomerAuthorization();
                if (item.ObjectState == ObjectState.Added)
                {
                    customerAuthorization.Id = tmpAuthorizationId;
                    tmpAuthorizationId --;
                } else
                {
                    customerAuthorization.Id = item.Id;
                }
                
                customerAuthorization.AuthorizationNo = item.AuthorizationNo;
                customerAuthorization.AuthorizationLetter = item.AuthorizationLetter;
                customerAuthorization.AuthorizationYear = item.AuthorizationYear;
                customerAuthorization.AuthorizationOffice = item.AuthorizationOffice;
                customerAuthorization.AuthorizationTypeId = item.AuthorizationTypeId;
                customerAuthorization.CustomerId = item.CustomerId;
                customerAuthorization.ObjectState = item.ObjectState;
                if (item.RowVersion == null) { 
                    customerAuthorization.RowVersion = Convert.FromBase64String(string.Empty);
                } else
                {
                    customerAuthorization.RowVersion = Convert.FromBase64String(item.RowVersion);
                }
                if (!(item.ObjectState == ObjectState.Deleted && item.Id <=0))
                    customer.Authorizations.Add(customerAuthorization);
            }

            return customer;
        }
        public static string GetMessageToClient(ObjectState objectState, string customerName)
        {
            string messageToClient = "";

            switch (objectState)
            {
                case ObjectState.Added:
                    messageToClient = string.Format("تم إضافة بيانات العميل:  {0} ", customerName);
                    break;
                case ObjectState.Modified:
                    messageToClient = string.Format(" تم تحديث بيانات العميل {0}", customerName);
                    break;
            }
            return messageToClient;

        }

    }
}