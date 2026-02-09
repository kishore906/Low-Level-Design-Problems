using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.CommonEnum;

namespace Parking_Lot.FareStrategyPattern.ConcreteStrategies
{
    public class BasicHourlyRateStrategy : ParkingFeeStrategy
    {
        public double CalculateFee(string vehicleType, int duration, DurationType durationType)
        {
            // differnt rates for differnt vehicle types
            switch(vehicleType.ToLower())
            {
                case "car":
                    return durationType == DurationType.HOURS 
                        ? duration * 10.0  // $10 per hour for car
                        : duration * 10.0 * 24; // Daily rate

                case "bike":
                    return durationType == DurationType.HOURS
                        ? duration * 5.0 // $5 per hour for bikes
                        : duration * 5.0 * 24; // daily rate

                case "auto":
                    return durationType == DurationType.HOURS
                        ? duration * 8.0 // $8 per hour for autos
                        : duration * 8.0 * 24; // daily rate

                default:
                    return durationType == DurationType.HOURS
                        ? duration * 15.0 // $15 per hour for other vehicles
                        : duration * 15.0 * 24; // daily rate
            }
        }
    }
}
