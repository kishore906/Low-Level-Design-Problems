using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.CommonEnum;

namespace Parking_Lot.FareStrategyPattern.ConcreteStrategies
{
    public class PremiumRateStrategy : ParkingFeeStrategy
    {
        public double CalculateFee(string vehicleType, int duration, DurationType durationType)
        {
            // Premium rates with higher multipliers
            switch (vehicleType.ToLower())
            {
                case "car":
                    return durationType == DurationType.HOURS
                            ? duration * 15.0   // $15 per hour for cars
                            : duration * 15.0 * 24;  // Daily rate

                case "bike":
                    return durationType == DurationType.HOURS
                            ? duration * 8.0    // $8 per hour for bikes
                            : duration * 8.0 * 24;  // Daily rate

                case "auto":
                    return durationType == DurationType.HOURS
                            ? duration * 12.0   // $12 per hour for autos
                            : duration * 12.0 * 24;  // Daily rate

                default:
                    return durationType == DurationType.HOURS
                            ? duration * 20.0   // $20 per hour for other vehicles
                            : duration * 20.0 * 24;  // Daily rate
            }
        }
    }
}
