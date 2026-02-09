using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.VehicleFactoryPattern;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Parking_Lot.ParkingSpots
{
    /*
     Abstract Parking Spot Class: This class will serve as the base class for all types of parking spots and will define the common properties and methods shared by all spots.
     */
    public abstract class ParkingSpot
    {
        private int SpotNumber {  get; set; }
        private bool IsOccupied { get; set; }
        private Vehicle Vehicle { get; set; }
        private string SpotType { get; set; }

        // Constructor to initialize parking spot with spot number and type
        public ParkingSpot(int spotNumber, string spotType)
        {
            SpotNumber = spotNumber;
            IsOccupied = false;
            SpotType = spotType;
        }

        // Abstract method to check if a vehicle can park in this spot
        public abstract bool CanParkVehicle(Vehicle vehicle);

        // Method to check if the spot is occupied
        public bool IsSpotOccupied()
        {
            return IsOccupied;
        }

        // Getter for spot number
        public int GetSpotNumber()
        {
            return SpotNumber;
        }

        // Getter for the vehicle parked in the spot
        public Vehicle GetVehicle()
        {
            return Vehicle;
        }

        // Getter for spot type
        public string GetSpotType()
        {
            return SpotType;
        }

        // Method to park a vehicle in the spot
        public void ParkVehicle(Vehicle vehicle) {
            // Check if spot is already occupied
            if (IsOccupied)
            {
                throw new InvalidOperationException("Spot is already occupied.");
            }

            // Check if the vehicle can be parked in this spot
            if (!CanParkVehicle(vehicle))
            {
                throw new ArgumentException($"This spot is not suitable for {vehicle.GetVehicleType()}");
            }

            // if all good park the vehicle
            Vehicle = vehicle;
            IsOccupied = true;
        }

        // Method to vacate the parking spot
        public void Vacate() {
            // Check if the spot is already vacant
            if (!IsOccupied)
            {
                throw new InvalidOperationException("Spot is already vacant.");
            }

            Vehicle = null!;
            IsOccupied = false;
        }
    }
}
