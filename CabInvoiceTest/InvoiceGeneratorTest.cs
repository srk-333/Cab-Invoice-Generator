using CabInvoiceGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CabInvoiceTest
{
    [TestClass]
    public class InvoiceGeneratorTest
    {
        public InvoiceGenerator premium;
        public InvoiceGenerator normal;  
        [TestInitialize]
        public void Setup()
        {
            normal = new InvoiceGenerator(RideType.NORMAL);
            premium = new InvoiceGenerator(RideType.PREMIUM);
        }
        /* Test Methods For UC-1 And Uc-5
         * RideType- NORMAL And PREMIUM.
         */
        [TestMethod]
        [TestCategory("NormalRideFare")]
        public void GivenDistanceAndTime_ShouldReturnTotalFareForNormalRide()
        {
            double excepted = 25.0;
            double distance = 2.0;
            int time = 5;
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL);
            double fare = invoice.CalculateFare(distance, time);
            Assert.AreEqual(excepted, fare);
        }
        [TestMethod]
        [TestCategory("PremiumRideFare")]
        public void GivenDistanceAndTime_ShouldReturnTotalFareForPremiumRide()
        {
            double excepted = 40.0;
            double distance = 2.0;
            int time = 5;
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM);
            double fare = invoice.CalculateFare(distance, time);
            Assert.AreEqual(excepted, fare);
        }
        [TestMethod]
        [TestCategory("NegativeDistanceValue")]
        public void GivenNegativeDistance_ShouldReturnInvalidDistanceException()
        {
            string expected = "Distance Cann't be Negative";
            double distance = -1.0;
            int time = 5;
            try
            {             
                double fare = normal.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }
        [TestMethod]
        [TestCategory("NegativeTimeValue")]
        public void GivenNegativeTime_ShouldReturnInvalidTimeException()
        {
            string expected = "Time Cann't be Negative";
            double distance = 3.0;
            int time = -5;
            try
            {
                double fare = premium.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }
        /* Test Methods For UC-2
         * Multiple Ride
         */
        [TestMethod]
        [TestCategory("MultipleRideFareForNormalRideType")]
        public void GivenMultipleRide_ShouldReturnTotalAggregateFareForNormalRide()
        {
            Ride[] rides = { new Ride(3.0, 5), new Ride(4.0, 10) };
            double expected = 85.0 ;
            double actual = normal.MultipleRide(rides);         
            Assert.AreEqual(expected , actual);
        }
        [TestMethod]
        [TestCategory("MultipleRideFareForPremiumRideType")]
        public void GivenMultipleRide_ShouldReturnTotalAggregateFareForPremiumRide()
        {
            Ride[] rides = { new Ride(2.0, 5), new Ride(3.0, 10) };
            double expected = 105.0;
            double actual = premium.MultipleRide(rides);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("NullRide")]
        public void GivenNullRide_ShouldReturnNullRideException()
        {
            Ride[] rides = { };
            string expected = "Null Ride";
            try
            {
                double actual = premium.MultipleRide(rides);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(expected , ex.Message);
            }
        }
        /* Test Methods For UC-3
         *  Enhanced Invoice Generation
         */
        [TestMethod]
        [TestCategory("Enhanced Invoice For Normal Ride")]
        public void GivenNoOfRides_ShouldReturnEnhancedInvoice_ForNormalRide()
        {
            Ride[] rides = { new Ride(3.0, 5), new Ride(4.0, 10) };
            double totalfare = 85.0;
            InvoiceSummary excepted = new InvoiceSummary(rides.Length, totalfare);
            InvoiceSummary actual = normal.EnhancedInvoice(rides);
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        [TestCategory("Enhanced Invoice For Premium Ride")]
        public void GivenNoOfRides_ShouldReturnEnhancedInvoice_ForPremiumRide()
        {
            Ride[] rides = { new Ride(2.0, 5), new Ride(3.0, 10) };
            double totalfare = 105.0;
            InvoiceSummary excepted = new InvoiceSummary(rides.Length, totalfare);
            InvoiceSummary actual = premium.EnhancedInvoice(rides);
            Assert.AreEqual(excepted, actual);
        }
        /* Test Methods For UC-4
        *  Enhanced Invoice Generation as per UserId
        */
        [TestMethod]
        [TestCategory("Enhanced Invoice For Normal Ride")]
        public void GivenUserId_ShouldReturnInvoiceForUser()
        {
            Ride[] rides = { new Ride(3.0, 5), new Ride(4.0, 10) };
            double totalfare = 85.0;
            InvoiceSummary invoiceSummary = new InvoiceSummary(rides.Length, totalfare);
            User expected = new User("Saurav", invoiceSummary);
            User actual = normal.InvoiceService(rides , "Saurav");
            Assert.AreEqual(expected, actual);
        }
    }
}