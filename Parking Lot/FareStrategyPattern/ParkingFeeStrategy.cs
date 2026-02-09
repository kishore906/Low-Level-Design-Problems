using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.CommonEnum;

namespace Parking_Lot.FareStrategyPattern
{
    public interface ParkingFeeStrategy
    {
        /**
     - Calculate parking fee based on vehicle type and duration
     -
     - @param vehicleType Type of vehicle being parked
     - @param duration Duration of parking (in hours or days)
     - @param durationType Type of duration (HOURS or DAYS)
     - @return Calculated parking fee
     */
        double CalculateFee(string vehicleType, int duration, DurationType durationType);
    }
}
