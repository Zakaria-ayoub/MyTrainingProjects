namespace CustomerAuthorizations.DataLayer.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomerAuthorizations.DataLayer.AuthorizationsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(CustomerAuthorizations.DataLayer.AuthorizationsContext context)
        {
            context.AuthorizationTypes.AddOrUpdate(
                c => c.AuthorizationTypeName,
                new AuthorizationType { AuthorizationTypeId=1, AuthorizationTypeName="����� ���" },
                new AuthorizationType { AuthorizationTypeId = 2, AuthorizationTypeName = "����� ���" },
                new AuthorizationType { AuthorizationTypeId = 3, AuthorizationTypeName = "����� ��� �����" }
                );
            context.Customers.AddOrUpdate(
                so => so.CustomerName,
                new Customer { CustomerName = "����� ���� ���", Address = "5 � ������ �����"
                , Authorizations = {
                  new CustomerAuthorization { AuthorizationNo="1" , AuthorizationLetter= "�" , AuthorizationTypeId=1 , AuthorizationOffice="������" , AuthorizationYear = "2016"},
                  new CustomerAuthorization { AuthorizationNo="123" , AuthorizationLetter= "�" , AuthorizationTypeId=2 , AuthorizationOffice="�������" , AuthorizationYear = "2014"}
                    }
                },
                new Customer { CustomerName = "���� ������", Address = "5 � ������ �����"
                ,
                    Authorizations = {
                  new CustomerAuthorization { AuthorizationNo="10" , AuthorizationLetter= "�" , AuthorizationTypeId=3 , AuthorizationOffice="������" , AuthorizationYear = "2003"},
                  new CustomerAuthorization { AuthorizationNo="777" , AuthorizationLetter= "�" , AuthorizationTypeId=1 , AuthorizationOffice="����" , AuthorizationYear = "2005"}
                    }
                }
                );


        }
    }
}
