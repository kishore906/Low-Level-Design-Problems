using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.CommonEnum;
using Parking_Lot.FareStrategyPattern;

namespace Parking_Lot.VehicleFactoryPattern
{
    public abstract class Vehicle
    {
        private string LicensePlate { get; set; } // Stores the vehicle's license plate number
        private string VehicleType { get; set; } // STores the type of the vehicle (eg: car, bike, truck)
        private ParkingFeeStrategy FeeStrategy { get; set; } // Strategy for calculating parking fees

        // // Constructor to initialize a vehicle with its license plate, type, and fee strategy
        public Vehicle(string licensePlate, string vehicleType, ParkingFeeStrategy parkingFeeStrategy) {
            LicensePlate = licensePlate;
            VehicleType = vehicleType;
            FeeStrategy = parkingFeeStrategy;
        }

        
        // Getter method to retrieve the vehicle's license plate number
        public string GetLicensePlate() { 
            return LicensePlate;
        }

        // public getter method to retrieve vehicle type
        public string GetVehicleType() {
            return VehicleType;
        }

        // Method to calculate parking fee based on duration and duration type
        public double CalculateFee(int duration, DurationType durationType) {
            return FeeStrategy.CalculateFee(VehicleType, duration, durationType);
        }
    }
}
