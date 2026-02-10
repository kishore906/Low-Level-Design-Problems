using System.Runtime.CompilerServices;
using Parking_Lot.CommonEnum;
using Parking_Lot.FareStrategyPattern;
using Parking_Lot.FareStrategyPattern.ConcreteStrategies;
using Parking_Lot.ParkingLotBuilder;
using Parking_Lot.ParkingLotController;
using Parking_Lot.ParkingSpots;
using Parking_Lot.ParkingSpots.ConcreteParkingSpots;
using Parking_Lot.PaymentStrategyPattern;
using Parking_Lot.PaymentStrategyPattern.ConcretePaymentStrategies;
using Parking_Lot.VehicleFactoryPattern;

// ------- without floors ----------

// Initialize parking spots
//List<ParkingSpot> parkingSpots = new List<ParkingSpot>();
//parkingSpots.Add(new CarParkingSpot(1, "Car"));
//parkingSpots.Add(new CarParkingSpot(2, "Car"));
//parkingSpots.Add(new BikeParkingSpot(3, "Bike"));
//parkingSpots.Add(new BikeParkingSpot(4, "Bike"));

// initialize parking lot
//ParkingLot parkingLot =  new ParkingLot(parkingSpots);

// --------------------------------------------------------------------------

// ------ with floors ------

// Create a parking lot with multiple floors and varied spot configurations
ParkingLot parkingLot =
    new ParkingLotBuilder()
        // First floor: 2 car spots, 2 bike spots
        .CreateFloor(1, 2, 2)
        // Second floor: 3 car spots, 1 bike spot, 1 other vehicle spot
        .CreateFloor(2, 3, 1, 1)
        .Build();

// ------------ common code --------------

// Create fee strategies
ParkingFeeStrategy basicHourlyRateStrategy = new BasicHourlyRateStrategy();
ParkingFeeStrategy premiumRateStrategy = new PremiumRateStrategy();

// Create vehicles using Factory Pattern with fee strategies
Vehicle car1 = VehicleFactory.CreateVehicle("Car", "CAR123", basicHourlyRateStrategy);
Vehicle car2 = VehicleFactory.CreateVehicle("Car", "CAR345", basicHourlyRateStrategy);

Vehicle bike1 = VehicleFactory.CreateVehicle("Bike", "BIKE456", premiumRateStrategy);
Vehicle bike2 = VehicleFactory.CreateVehicle("Bike", "BIKE123", premiumRateStrategy);

// Park vehicles
ParkingSpot carSpot = parkingLot.ParkVehicle(car1);
ParkingSpot bikeSpot = parkingLot.ParkVehicle(bike1);

ParkingSpot carSpot2 = parkingLot.ParkVehicle(car2);
ParkingSpot bikeSpot2 = parkingLot.ParkVehicle(bike2);

Console.WriteLine("\nSelect payment method for your vehicle:");
Console.WriteLine("1. Credit Card");
Console.WriteLine("2. Cash");

int paymentMethod;
while (!int.TryParse(Console.ReadLine(), out paymentMethod))
{
    Console.WriteLine("Please enter a valid number (1 or 2).");
}

// Process payments using Strategy Patterns
if(carSpot != null) {
    // Calculate fee using the specific strategy for the vehicle
    double carFee = car1.CalculateFee(2, DurationType.HOURS);
    PaymentStrategy carPaymentStrategy = GetPaymentStrategy(paymentMethod);
    carPaymentStrategy.ProcessPayment(carFee);
    parkingLot.VacateSpot(carSpot, car1);
}

if (bikeSpot != null)
{
    // Calculate fee using the specific strategy for the vehicle
    double bikeFee = bike1.CalculateFee(3, DurationType.HOURS);
    PaymentStrategy bikePaymentStrategy =
            GetPaymentStrategy(paymentMethod);
    bikePaymentStrategy.ProcessPayment(bikeFee);
    parkingLot.VacateSpot(bikeSpot, bike1);
}

PaymentStrategy GetPaymentStrategy(int paymentMethod) {
    switch (paymentMethod)
    {
        case 1:
            return new CreditCardPayment();
        case 2:
            return new CashPayment();
        default:
            Console.WriteLine("Invalid choice! Default to Credit card payment.");
            return new CreditCardPayment();
    }
}

Console.ReadKey();

/*

OUTPUT:

Vehicle parked successfully in spot: 1
Vehicle parked successfully in spot: 3
Vehicle parked successfully in spot: 2
Vehicle parked successfully in spot: 4
Select payment method for your vehicle:
1. Credit Card
2. Cash
1
Processing credit card payment of $20.0
Car vacated the spot: 1
Processing credit card payment of $24.0
Bike vacated the spot: 3

Process finished with exit code 0

*/