using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.FareStrategyPattern;

namespace Parking_Lot.VehicleFactoryPattern.ConcreteVehicles
{
    public class OtherVehicle : Vehicle
    {
        public OtherVehicle(string licensePlate, string vehicleType, ParkingFeeStrategy parkingFeeStrategy) : base(licensePlate, vehicleType, parkingFeeStrategy)
        {
        }
    }
}
