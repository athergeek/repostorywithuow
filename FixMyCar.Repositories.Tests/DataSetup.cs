using FixMyCar.Entities;
using FixMyCar.Entities.Models;
using FixMyCar.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMyCar.Repositories.Tests
{
    public class DataSetup
    {
        private Customer cust = null;
        private Customer cust2 = null;
        public int newCustId { get; set; }



        public Customer getCustomer1() {
            return new Customer() { FirstName = "Sam", LastName = "Houston", EmailAddress = "sam.houston@gmail.com", ContactNumber = "713-924-4444" };
            

        }

        public Customer getCustomer2()
        {
            return new Customer() { FirstName = "Jimmie", LastName = "Brown", EmailAddress = "sam.houston@gmail.com", ContactNumber = "713-924-2222" };

        }

        public Customer getCustomer3()
        {
            return new Customer() { FirstName = "John", LastName = "Hodson", EmailAddress = "john.hodson@gmail.com", ContactNumber = "713-924-5555" };
        }



        public Customer getCustomerWithVehicle()
        {
             Customer c = new Customer() { FirstName = "Kristine", LastName = "Tyler", EmailAddress = "sam.houston@gmail.com", ContactNumber = "713-924-5555" };
             c.Vehicles.Add(new Vehicle { Year = 1996, Make = "Toyota", Model = "Camry" });
              return c;
        }


        public void AddOrUpdateCustomer(Customer customer)
        {
            using (var uow = new BaseUnitOfWork<FixMyCarEntities>())
            {
                using (var repo = new BaseRepository<Customer>(uow))
                {
                    repo.AddOrUpdate(customer);
                }
                uow.Save();
                newCustId = customer.CustomerId;
            }

        }

    }
}
