using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FixMyCar.Entities.Models;
using FixMyCar.Repositories.Repositories;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace FixMyCar.Repositories.Tests
{
    /// <summary>
    /// Summary description for VehicleTest
    /// </summary>
    [TestClass]
    public class VehicleTest
    {
        DataSetup dataSetup = null;

        [TestInitialize]
        public void Setup()
        {
            dataSetup = new DataSetup();
           
        }


        [TestMethod]
        public void CustomerVehile_Add_SingleRecord()
        {
            Customer tcust = dataSetup.getCustomerWithVehicle();
                dataSetup.AddOrUpdateCustomer(tcust);

            IList<Customer> customers = new List<Customer>();
            using (var repo = new BaseRepository<Customer>())
            {
                Customer tcust2 = repo.FindById(tcust.CustomerId);
                Assert.IsNotNull(tcust2);
                Assert.AreEqual(tcust2.Vehicles.Count, 1);
            }
        }
    }
}

