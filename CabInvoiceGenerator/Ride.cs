using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class Ride
    {
        //variable
        public double distance;
        public int time;
        //Parameterized Constructor
        public Ride(double distance, int time)
        {
            this.distance = distance;
            this.time = time;
        }
    }
}