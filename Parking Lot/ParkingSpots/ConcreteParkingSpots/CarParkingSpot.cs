using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.VehicleFactoryPattern;

namespace Parking_Lot.ParkingSpots.ConcreteParkingSpots
{
    public class CarParkingSpot : ParkingSpot
    {
        // Constructor
        public CarParkingSpot(int spotNumber, string spotType) : base(spotNumber, spotType) { }

        // overrided abstract method (providing implementation)
        public override bool CanParkVehicle(Vehicle vehicle)
        {
            return "Car".Equals(vehicle.GetVehicleType(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
