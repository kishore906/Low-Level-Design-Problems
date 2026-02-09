using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Parking_Lot.ParkingSpots;

namespace Parking_Lot.ParkingFloors
{
    /*
     To extend the solution for a multi-floor Parking Lot, we can create a concrete ParkingFloor class and encapsulate the logic of handling parking spots within this class.
     */
    public class ParkingFloor
    {
        // List of parking spots on this floor
        private List<ParkingSpot> Spots;

        // Unique identifier for the floor
        private int FloorNumber { get; set; }

        // Constructor
        public ParkingFloor(int floorNumber) { 
            FloorNumber = floorNumber;
            Spots = new List<ParkingSpot>();
        }

        // Adds a parking spot to this floor
        public void AddParkingSpot(ParkingSpot spot) { 
            Spots.Add(spot);
        }

        // Finds an available parking spot for a specific vehicle type
        public ParkingSpot FindAvailableSpot(string vehicleType) {
            // iterate over all spots to fnd an available spot matching the type
            foreach (var spot in Spots)
            {
                if (!spot.IsSpotOccupied() && spot.GetSpotType().Equals(vehicleType, StringComparison.OrdinalIgnoreCase)) { 
                return spot;
                }
            }
            return null!; // no available spot
        }

        // Retrieves all parking spots on this floor.
        public List<ParkingSpot> GetParkingSpots()
        {
            return Spots;
        }

        public int GetFloorNumber()
        {
            return FloorNumber;
        }
    }
}
