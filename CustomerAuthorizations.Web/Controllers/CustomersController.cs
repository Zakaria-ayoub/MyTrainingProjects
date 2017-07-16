using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerAuthorizations.DataLayer;
using CustomerAuthorizations.Model;
using CustomerAuthorizations.Web.ViewModels;
using System.Web.Script.Serialization;

using System.Windows.Forms;


namespace CustomerAuthorizations.Web.Controllers
{
    public class CustomersController : Controller
    {
        private AuthorizationsContext _authorizationsContext ;

        public CustomersController()
        {
            _authorizationsContext = new AuthorizationsContext();
        }

        
        public ActionResult Index()
        {
            return View(_authorizationsContext.Customers.ToList());
        }

        public JsonResult InitializePageData()
        {
            
            var contactForm = _authorizationsContext.AuthorizationTypes ;
            var json = new JavaScriptSerializer().Serialize(contactForm);
            return this.Json(contactForm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _authorizationsContext.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel customerViewModel = ViewModels.Helpers.CreateCustomerViewModelFromCustomerModel(customer);
            return View(customerViewModel);
        }
        [HandleModelStateException]
        public JsonResult Save(CustomerViewModel customerViewModel)
        {
            string messageToClient;
            if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState);
            }
            Customer customer = ViewModels.Helpers.CreateCustomerModelFromCustomerViewModel(customerViewModel);

            if (customer.ObjectState== ObjectState.Deleted )
            {
                
                foreach (var item in customer.Authorizations)
                {
                    if (item.Id <=0 )
                    {
                        customer.Authorizations.Remove(item);
                    } else
                    {
                        item.ObjectState = ObjectState.Deleted;
                    }
                    
                }
            };

            _authorizationsContext.Customers.Attach(customer);
            _authorizationsContext.ApplyStateChanges();

            messageToClient = "";
            try
            {
                _authorizationsContext.SaveChanges();
            }

            catch (DBConcurrencyException)
            {
                messageToClient = "تم تحديث بيانات العميل عن طريق مستخدم آخر وسيقوم الموقع بعرض آخر بيانات للعميل الآن";
            }
            catch (  Exception ex)
            {
                throw new ModelStateException(ex);

            }

            if (messageToClient.Trim().Length == 0)
            {
                messageToClient = ViewModels.Helpers.GetMessageToClient(customer.ObjectState, customer.CustomerName);
            } else
            {
                customerViewModel.CustomerId = customer.CustomerId;
                _authorizationsContext.Dispose();
                _authorizationsContext = new AuthorizationsContext();
                customer = _authorizationsContext.Customers.Find(customerViewModel.CustomerId);
            };



            if (customerViewModel.ObjectState == ObjectState.Deleted)
                return Json(new { newLocation = "/Customers/Index/" });

            customerViewModel = ViewModels.Helpers.CreateCustomerViewModelFromCustomerModel(customer);
            customerViewModel.MessageToClient = messageToClient;

            return Json(new { customerViewModel });

        }

        

        public ActionResult Create()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.CustomerAuthorizations.Add(new CustomerAuthorizationViewModel { Id = -1, AuthorizationTypeId = 1, AuthorizationLetter = "", AuthorizationNo = "", AuthorizationOffice = "", AuthorizationYear = "", ObjectState = ObjectState.Added });
            customerViewModel.ObjectState = ObjectState.Added;
            return View(customerViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _authorizationsContext.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel customerViewModel = ViewModels.Helpers.CreateCustomerViewModelFromCustomerModel(customer);
            return View(customerViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _authorizationsContext.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel customerViewModel = ViewModels.Helpers.CreateCustomerViewModelFromCustomerModel(customer);
            customerViewModel.ObjectState = ObjectState.Deleted;
            return View(customerViewModel);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authorizationsContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
