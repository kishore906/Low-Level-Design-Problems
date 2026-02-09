using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.ParkingSpots;
using Parking_Lot.VehicleFactoryPattern;

namespace Parking_Lot.ParkingLotController
{
    /*
      In a parking lot, we can have multiple parking spots for different types of vehicles, such as cars, bikes, autos, scooters, etc. To efficiently manage these spots, we will start by defining a common abstract class for parking spots and then create concrete implementations for each type of vehicle.
     */
    public class ParkingLotWithoutFloors
    {
        private List<ParkingSpot> ParkingSpots { get; set; }

        // Constructor to initialize the parking lot with parking spots
        public ParkingLotWithoutFloors(List<ParkingSpot> parkingSpots) {
            ParkingSpots = parkingSpots;
        }

        // Method to find an available spot based on vehicle type
        public ParkingSpot FindAvailableSpot(string vehicleType) { 
            foreach (var spot in ParkingSpots)
            {
                if(!spot.IsSpotOccupied() && spot.GetSpotType().Equals(vehicleType))
                {
                    return spot; // Found an available spot for the vehicle type
                }
            }
            return null!; // No available spot found for the given vehicle type
        }

        // Method to park a vehicle
        public ParkingSpot ParkVehicle(Vehicle vehicle) {
            ParkingSpot spot = FindAvailableSpot(vehicle.GetVehicleType());

            if (spot != null)
            {
                spot.ParkVehicle(vehicle); // Mark the spot as occupied
                Console.WriteLine(
                    "Vehicle parked successfully in spot: " + spot.GetSpotNumber());
                return spot;
            }

            Console.WriteLine(
                "No parking spots available for " + vehicle.GetVehicleType() + "!");

            return null!;
        }

        // Method to vacate a parking spot
        public void VacateSpot(ParkingSpot spot, Vehicle vehicle)
        {
            if (spot != null && spot.IsSpotOccupied()
                && spot.GetVehicle().Equals(vehicle))
            {
                spot.Vacate(); // Free the spot
                Console.WriteLine(vehicle.GetVehicleType()
                    + " vacated the spot: " + spot.GetSpotNumber());
            }
            else
            {
                Console.WriteLine("Invalid operation! Either the spot is already vacant "
                                   + "or the vehicle does not match.");
            }
        }

        // Method to find a spot by its number
        public ParkingSpot GetSpotByNumber(int spotNumber) { 
            foreach(var spot in ParkingSpots)
            {
                if(spot.GetSpotNumber() == spotNumber)
                {
                    return spot;
                }
            }
            return null!; // Spot NOt Found
        }

        // Getter for parking spots
        public List<ParkingSpot> GetParkingSpots()
        {
            return ParkingSpots;
        }
    }
}
