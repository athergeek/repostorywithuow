using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

using FixMyCar.Repositories.interfaces;
using FixMyCar.Repositories.Repositories;
using FixMyCar.Entities;
using FixMyCar.Entities.Models;
using System.Collections.Generic;
using Moq;
using System.Linq;


namespace FixMyCar.Repositories.Tests
{
    [TestClass]
    public class CustomerTest   
    {
        DataSetup dataSetup = null;

        [TestInitialize]
        public void Setup() {
            dataSetup = new DataSetup();
        }

        [TestMethod]
        public void Customer_Add_SingleRecord()
        {
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer2());
            IList<Customer> customers = new List<Customer>();
            using (var repo = new BaseRepository<Customer>())
            {
                customers = (IList<Customer>)repo.findAll();
                Assert.AreEqual(customers.Count, 1);
                repo.Delete(customers);
                repo.save();
            }
        }

        [TestMethod]
        public void Customer_Update_SingleRecord()
        {


            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer1());
            Customer c = null;
            IList<Customer> customers = new List<Customer>();
            using (CustomerRepository repo = new CustomerRepository())
            {
                customers = (IList<Customer>)repo.findCustomerByPhoneNumber(dataSetup.getCustomer1());
                c = customers[0];
                c.ContactNumber = "123-456-7890";
                repo.AddOrUpdate(c);
                repo.save();
            }


            using (CustomerRepository repo = new CustomerRepository())
            {
                Customer temp = new Customer();
                Customer temp2 = new Customer();
                temp.ContactNumber = "123-456-7890";
                customers = (IList<Customer>)repo.findCustomerByPhoneNumber(temp);
                temp2 = customers[0];
                Assert.AreEqual(customers.Count, 1);
                Assert.AreEqual(temp2.ContactNumber, "123-456-7890");
                repo.Delete(customers);
                repo.save();
            }

        }

        [TestMethod]
        public void Customer_FindAll_Customers()
        {
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer1());
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer2());
            IList<Customer> customers = new List<Customer>();
            using (var repo = new BaseRepository<Customer>())
            {
                customers = (IList<Customer>)repo.findAll();
                Assert.AreEqual(customers.Count, 3);
                repo.Delete(customers);
                repo.save();
            }

        }

        [TestMethod]
        public void Customer_FindById_SingleRecord()
        {
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer1());
            IList<Customer> customers = new List<Customer>();
            using (var repo = new BaseRepository<Customer>())
            {
                Customer c = repo.FindById(dataSetup.newCustId);
                Assert.IsNotNull(c);
                repo.Delete(customers);
                repo.save();
            }

        }

        [TestMethod]
        public void Customer_FindCustomerByPhoneNumber_SingleRecord()
        {


            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer2());
            IList<Customer> customers = new List<Customer>();
            using (CustomerRepository repo = new CustomerRepository())
            {
                customers = (IList<Customer>)repo.findCustomerByPhoneNumber(dataSetup.getCustomer2());
                Assert.IsNotNull(customers[0]);
                repo.Delete(customers);
                repo.save();
            }

        }



        [TestMethod]
        public void Customer_findCustomerByFirstNameAndLastName_SingleRecord()
        {
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer2());
            IList<Customer> customers = new List<Customer>();
            using (CustomerRepository repo = new CustomerRepository())
            {
                customers = (IList<Customer>)repo.findCustomerByFirstNameAndLastName(dataSetup.getCustomer2());
                Assert.IsNotNull(customers[0]);
                repo.Delete(customers);
                repo.save();
            }

        }

        [TestMethod]
        public void Customer_DeleteById_SingleRecord()
        {

            Customer temp = dataSetup.getCustomer3();
            dataSetup.AddOrUpdateCustomer(temp);
            IList<Customer> customers = new List<Customer>();
            using (var repo = new BaseRepository<Customer>())
            {
                repo.Delete(temp);
                repo.save();
                Customer c = repo.FindById(dataSetup.newCustId);
                Assert.IsNull(c);
            }

        }



        [TestMethod]
        public void Customer_DeleteAll_MultipleRecord()
        {
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer1());
            dataSetup.AddOrUpdateCustomer(dataSetup.getCustomer2());

            IList<Customer> customers = new List<Customer>();
            using (var repo = new BaseRepository<Customer>())
            {
                customers = (IList<Customer>)repo.findAll();
                repo.Delete(customers);
                repo.save();
                customers = (IList<Customer>)repo.findAll();
                Assert.AreEqual(customers.Count, 0);

            }

        }



        private void DeleteCustomer(IList<Customer> customers) {
            using (var uow = new BaseUnitOfWork<FixMyCarEntities>())
            {
                using (var repo = new BaseRepository<Customer>(uow))
                {
                    repo.Delete(customers);
                }
                uow.Save();
            }
        }
    }
}
