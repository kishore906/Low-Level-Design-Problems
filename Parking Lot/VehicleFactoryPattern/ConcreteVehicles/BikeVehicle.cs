using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.FareStrategyPattern;

namespace Parking_Lot.VehicleFactoryPattern.ConcreteVehicles
{
    public class BikeVehicle : Vehicle
    {
        public BikeVehicle(string licensePlate, string vehicleType, ParkingFeeStrategy parkingFeeStrategy) : base(licensePlate, vehicleType, parkingFeeStrategy)
        {
        }
    }
}
