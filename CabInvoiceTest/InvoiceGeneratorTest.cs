using CabInvoiceGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CabInvoiceTest
{
    [TestClass]
    public class InvoiceGeneratorTest
    {
        [TestMethod]
        [TestCategory("NormalRideFare")]
        public void GivenDistanceAndTime_ShouldReturnTotalFareForNormalRide()
        {
            double excepted = 25;
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
            double excepted = 40;
            double distance = 2.0;
            int time = 5;
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM);
            double fare = invoice.CalculateFare(distance, time);
            Assert.AreEqual(excepted, fare);
        }
    }
}