using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        Dictionary<string, InvoiceSummary> invoiceService = new Dictionary<string, InvoiceSummary>();
        public RideType rideType;
        // readonly Variables
        public readonly double MINIMUM_COST_PER_KM;
        public readonly int MAXIMUM_COST_PER_MIN;
        public readonly int MINIMUM_COST;
        //Constructor For Initializing Data as Per RideType.
        public InvoiceGenerator( RideType type)
        {
            this.rideType = type;
            try
            {
                //Initializing Data for Normal RideType
                if (this.rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.MAXIMUM_COST_PER_MIN = 1;
                    this.MINIMUM_COST = 5;
                }
                //Initializing Data for Premium RideType
                if (this.rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.MAXIMUM_COST_PER_MIN = 2;
                    this.MINIMUM_COST = 20;
                }
            }
            //Exception Catching
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDETYPES, "Invaid Ride Type");
            }
        }
        //Method to Calculate Fare as per given Distance And Time for both RideTpes.
        public double CalculateFare(double distance , int time)
        {
            if (distance <= 0)
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Distance Cann't be Negative");
            if (time <= 0)
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Time Cann't be Negative");
            double totalFare = distance * MINIMUM_COST_PER_KM + time * MAXIMUM_COST_PER_MIN;
            //Return Max amount as Fare form total and Minimum Fare.
            return Math.Max(totalFare,MINIMUM_COST);
        }
        //Multiple Ride Fare Calculation
        public double MultipleRide(Ride[] rides)
        {
            double totalFare = 0;
            //check Condition for Null Ride and Ride Length.
            if (rides == null || rides.Length == 0 )
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Null Ride");
            foreach (Ride ride in rides)
            {
                totalFare += CalculateFare(ride.distance, ride.time);
            }
            return Math.Max(totalFare, MINIMUM_COST);          
        }
        //Method to Genetare Enhanced Invoice.
        public InvoiceSummary EnhancedInvoice(Ride[] rides)
        {
            double result = MultipleRide(rides);
            return new InvoiceSummary(rides.Length, result);
        }
        //Method to Get Invoice For a user
        public User InvoiceService(Ride[] rides , string userId)
        {
            InvoiceSummary result = EnhancedInvoice(rides);
            return new User(userId, result);
        }
    }
    //RideType Enum
    public enum RideType
    {
        NORMAL,
        PREMIUM,
    }
}