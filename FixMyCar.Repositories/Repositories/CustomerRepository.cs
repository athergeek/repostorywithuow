using FixMyCar.Entities.Models;
using FixMyCar.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMyCar.Repositories.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {

        public CustomerRepository(IUnitOfWork uow) :base(uow)
        {

        }

        public CustomerRepository(): base()
        {
        }
            
        public IList<Customer> findCustomerByPhoneNumber(Customer customer) {
            return this.set.Where(customers => customers.ContactNumber == customer.ContactNumber).ToList<Customer>();
        }

        public IList<Customer> findCustomerByFirstNameAndLastName(Customer customer)
        {
            return this.set.Where(customers => customers.FirstName.ToLower().Contains(customer.FirstName) &&
                                  customers.LastName.ToLower().Contains(customer.LastName)).ToList<Customer>();


        }


    }
}
