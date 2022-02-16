using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
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
            double totalFare = 0;
            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * MAXIMUM_COST_PER_MIN;
            }
            catch ( CabInvoiceException)
            {
                if (distance <= 0)
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                if (time <= 0)
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invaid Time");
            }
            //Return Max amount as Fare form total and Minimum Fare.
            return Math.Max(totalFare,MINIMUM_COST);
        }
    }
    //RideType Enum
    public enum RideType
    {
        NORMAL,
        PREMIUM
    }
}