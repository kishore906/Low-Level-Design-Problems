using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.FareStrategyPattern.ConcreteStrategies;
using Parking_Lot.ParkingFloors;
using Parking_Lot.ParkingLotController;
using Parking_Lot.ParkingSpots.ConcreteParkingSpots;
using Parking_Lot.VehicleFactoryPattern.ConcreteVehicles;


namespace Parking_Lot.ParkingLotBuilder
{
    /**
 - Builder class for creating a flexible and extensible parking lot.
 - Provides a fluent interface for constructing multi-floor parking lots.

    The ParkingLot class would then be responsible for managing multiple parking floors, while each floor would handle its own parking spots. This approach adheres to the Single Responsibility Principle, ensuring that each class has a clear and distinct responsibility.
 */

    public class ParkingLotBuilder
    {
        // List of floors to be added to the parking lot
        private List<ParkingFloor> Floors;

        // Constructor initializes the list of floors
        public ParkingLotBuilder() { 
            Floors = new List<ParkingFloor>();
        }

        // Adds a pre-configured parking floor to the parking lot.
        public ParkingLotBuilder AddFloor(ParkingFloor floor)
        {
            Floors.Add(floor);
            return this;
        }

        // Creates a floor with specified numbers of different vehicle parking
        public ParkingLotBuilder CreateFloor(int floorNumber, int numOfCarSpots, int numOfBikeSpots, params int[] otherSpotCounts) {

            // Create a new parking floor
            ParkingFloor floor = new ParkingFloor(floorNumber);

            // Add car spots
            for (int i = 0; i < numOfCarSpots; i++)
            {
                floor.AddParkingSpot(new CarParkingSpot(i + 1, "Car"));
            }
            // Add bike spots
            for (int i = 0; i < numOfBikeSpots; i++)
            {
                floor.AddParkingSpot(new BikeParkingSpot(numOfCarSpots + i + 1, "Bike"));
            }

            // Add other types of spots if provided
            int spotOffset = numOfCarSpots + numOfBikeSpots;

            for(int i = 0; i < otherSpotCounts.Length; i++)
            {
                for(int j = 0; j < otherSpotCounts[i]; j++)
                {
                    // Dynamically add other vehicle type spots
                    // In a real system, we might want a more robust way to handle different vehicle types
                    floor.AddParkingSpot(new OtherVehicleParkingSpot(spotOffset + j + 1, "Other"));
                }

                // Update the spot offset for the next type of vehicle
                spotOffset += otherSpotCounts[i];
            }

            // Add the configured floor to the list of floors
            Floors.Add(floor);
            return this;
        }

        // Builds and returns the fully configured parking lot.
        public ParkingLot Build()
        {
            return new ParkingLot(Floors);
        }
    }
}
