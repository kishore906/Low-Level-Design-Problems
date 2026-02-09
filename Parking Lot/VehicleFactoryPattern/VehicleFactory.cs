using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.FareStrategyPattern;
using Parking_Lot.VehicleFactoryPattern.ConcreteVehicles;

namespace Parking_Lot.VehicleFactoryPattern
{
    // class which return concrete vehicle class object (i.e. Car, Bike, Other)
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(string vehicleType, string licensePlate, ParkingFeeStrategy feeStrategy) {
            if(string.Equals("Car", vehicleType, StringComparison.OrdinalIgnoreCase))
            {
                return new CarVehicle(licensePlate, vehicleType, feeStrategy);
            }else if (string.Equals("Bike", vehicleType, StringComparison.OrdinalIgnoreCase))
            {
                return new BikeVehicle(licensePlate, vehicleType, feeStrategy);
            }
            return new OtherVehicle(licensePlate, vehicleType, feeStrategy);
        }
    }
}
