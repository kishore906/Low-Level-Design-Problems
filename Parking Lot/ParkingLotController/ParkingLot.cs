using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.ParkingFloors;
using Parking_Lot.ParkingSpots;
using Parking_Lot.VehicleFactoryPattern;

namespace Parking_Lot.ParkingLotController
{
    // Class representing a parking lot with multiple floors
    public class ParkingLot
    {
        // List of parking floors in the parking lot 
        private List<ParkingFloor> Floors;

        // Constructor to initialize the parking lot with given floors
        public ParkingLot(List<ParkingFloor> floors) {
            Floors = floors;
        }

        // Method to find the first available parking spot for a given vehicle type
        public ParkingSpot FindAvailableSpot(string vehicleType)
        {
            foreach(var floor in Floors)
            {
                ParkingSpot spot = floor.FindAvailableSpot(vehicleType);
                if (spot != null)
                {
                    return spot; // Return the first available spot found
                }
            }
            return null!; // Return null if no spot is available
        }

        // Method to park a vehicle in an available spot
        public ParkingSpot ParkVehicle(Vehicle vehicle)
        {
            ParkingSpot spot = FindAvailableSpot(vehicle.GetVehicleType());
            if (spot != null)
            {
                spot.ParkVehicle(vehicle); // Park the vehicle in the found spot
                Console.WriteLine(
                    "Vehicle parked successfully in spot: " + spot.GetSpotNumber());
                return spot;
            }

            // If no spot is available, notify the user
            Console.WriteLine(
                "No parking spots available for " + vehicle.GetVehicleType() + "!");

            return null!;
        }

        // Method to vacate a parking spot
        public void VacateSpot(ParkingSpot spot, Vehicle vehicle)
        {
            // Ensure the spot is occupied and the vehicle matches before vacating
            if (spot != null && spot.IsSpotOccupied()
                && spot.GetVehicle().Equals(vehicle))
            {
                spot.Vacate(); // Free up the parking spot
                Console.WriteLine(vehicle.GetVehicleType()
                    + " vacated the spot: " + spot.GetSpotNumber());
            }
            else
            {
                Console.WriteLine("Invalid operation! Either the spot is already vacant "
                                   + "or the vehicle does not match.");
            }
        }

        // Method to retrieve a parking spot by its spot number
        public ParkingSpot GetSpotByNumber(int spotNumber)
        {
            foreach(ParkingFloor floor  in Floors)
            {
                foreach(ParkingSpot spot in floor.GetParkingSpots())
                {
                    if (spot.GetSpotNumber() == spotNumber)
                    {
                        return spot; // Return the parking spot if found
                    }
                }
            }
            return null!; // Return null if no spot with the given number exists
        }

        // Getter method to retrieve the list of parking floors
        public List<ParkingFloor> GetFloors()
        {
            return Floors;
        }
    }
}
