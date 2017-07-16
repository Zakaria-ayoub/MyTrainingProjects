var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};
var AuthorizationTypeData;

$.ajax({
    url: '/Customers/InitializePageData',
    async: false,
    dataType: 'json',
    success: function (json) {
        AuthorizationTypeData = json;
        
    }
});


var customerAuthorizationsMapping = {
    'CustomerAuthorizations': {
        key: function (data) {
            return ko.utils.unwrapObservable(data.Id);
        },
        create: function (options) {
            return new CustomerAuthorizationViewModel(options.data);
        }
    }
};




CustomerAuthorizationViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, customerAuthorizationsMapping,  self);
    self.flagAuthorizationAsEdited = function () {

        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }
        return true;
    };
    self.DeleteAuthorization = function () {

        self.ObjectState(ObjectState.Deleted);
    };

    self.DeletedAuthorization = ko.computed(function () {
        if (self.ObjectState() == ObjectState.Deleted) {
            return true;
        }
        return false;
    });
    self.ActiveAuthorization = ko.computed(function () {
        if (self.ObjectState() == ObjectState.Deleted) {
            return false;
        }
        return true;
    });

};

CustomerViewModel = function (data) {
    var self = this;
    selectItems = ko.observableArray(AuthorizationTypeData);
    ko.mapping.fromJS(data, customerAuthorizationsMapping,self );
    self.customerId = data.CustomerId;
    self.save = function () {
        alert(ko.mapping.toJSON(self));
        $.ajax({
            url: "/Customers/Save/",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: ko.mapping.toJSON(self),
            success: function (data) {
                if (data.customerViewModel != null) {
                    ko.mapping.fromJS(data.customerViewModel, {}, self);
                }
                if (data.newLocation != null)
                    window.location.href = data.newLocation;
            },
            error: function (XMLHttpRequest , textStatus , errorThrown) {
                if (XMLHttpRequest.status == 400) {
                    $('#MessageToClient').text(XMLHttpRequest.responseText);
                }
                else {
                    $('#MessageToClient').text("حدثت مشكلة اثناء الحفظ")
                }
            }

        });
    },
    
    self.flagCustomerAsEdited = function () {
        
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }
        return true;
    },
    self.addCustomerAuthorization = function () {
        var customerAuthorization = new CustomerAuthorizationViewModel({
            AuthoizationId: 0,
            AuthorizationTypeId: 0,
            AuthorizationNo : '' , 
            AuthorizationLetter: '',
            AuthorizationYear: '',
            AuthorizationOffice :'',
            CustomerId: self.customerId,
            ObjectState: ObjectState.Added
        });
        self.CustomerAuthorizations.push(customerAuthorization);
    }
};

$(document).ready(function () {

    $("form").validate({
        submitHandler: function (form) {
            customerViewModel.save();
        },
        invalidHandler: function(event, validator) {
            // 'this' refers to the form
            var errors = validator.numberOfInvalids();
            alert(errors);
            if (errors) {
                var message = errors == 1
                  ? 'You missed 1 field. It has been highlighted'
                  : 'You missed ' + errors + ' fields. They have been highlighted';
                $("div.error span").html(message);
                $("div.error").show();
            } else {
                $("div.error").hide();
            }
            
        },
        rules: {
            CustomerName: {
                required: true
            },
            Address: {
                required: true
            },
            Tel1: {
                required: true
            },
            Tel2: {
                required: true
            },
            AuthorizationNo: {
                required: true
            },
            AuthorizationLetter: {
                required: true
            },
            AuthorizationYear: {
                required: true
            },
            AuthorizationOffice: {
                required: true
            },
            EmailAddress: {
                email: true
            }

        },
        messages: {
            CustomerName: {
                required: "رجاء كتابة اسم العميل"
            },
            Address: {
                required: "رجاء كتابة العنوان"
            },
            Tel1: {
                required: "رجاء ادخال رقم المحمول"
            },
            Tel2: {
                required: "رجاء ادخال رقم التليفون"
            },
            AuthorizationNo: {
                required: "ادخل رقم التوكيل"
            },
            AuthorizationLetter: {
                required: "ادخل الحرف"
            },
            AuthorizationYear: {
                required: "ادخل السنة القضائية"
            },
            AuthorizationOffice: {
                required: "ادخل مكتب التوثيق"
            },
            EmailAddress: {
                email: "تأكد من صيغة البريد الالكتروني"
            }


        },
        tooltip_options: {
            CustomerName: {
                placement: 'left'
            },
            Tel1: {
                placement: 'left'
            },
            Tel2: {
                placement: 'left'
            },
            Address: {
                placement: 'left'
            },
            EmailAddress: {
                placement: 'left'
            }
        }
    });

    //$("#Authorizationform").on('submit', function () {
    //    customerViewModel.save();
    //    return false;
    //});

});
