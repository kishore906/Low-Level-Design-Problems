# Parking Lot System (Low Level Design)

A comprehensive C# implementation of a Parking Lot system demonstrating key Object-Oriented Design (OOD) principles and Design Patterns. This project serves as a practical example of how to structure a scalable and maintainable application for Low Level Design (LLD) interviews and real-world scenarios.

# Aim or Rules of the System

Setup:

• The parking lot has multiple slots available for parking.
• Different types of vehicles (bike, car, truck) can occupy different slot sizes.
• Each vehicle is issued a parking ticket upon entry.
• The system calculates the parking fee based on the duration of stay and vehicle type.

Exit and Payment:

• A vehicle needs to make a payment before exiting.
• Multiple payment methods (Cash, Card) should be supported.
• Once payment is successful, the vehicle is allowed to exit, and the parking slot is freed.

Illegal Actions:

• A vehicle cannot park in an already occupied slot.
• Vehicles cannot vacate without completing the payment process.

## 🚀 Features

- **Multi-Floor Parking**: Supports parking lots with multiple floors, each with configurable capacity.
- **Diverse Vehicle Support**: Handles various vehicle types including Cars, Bikes, and generic "Other" vehicles.
- **Flexible Spot Management**: Different parking spots (Car Spots, Bike Spots) allocated based on vehicle type.
- **Dynamic Fee Calculation**: Pluggable strategies for calculating parking fees (e.g., Basic Hourly Rate, Premium Rate).
- **Multiple Payment Options**: Extensible payment processing (e.g., Credit Card, Cash).
- **Robust Error Handling**: Manages edge cases like full parking, invalid spots, etc.

## 🏗️ Design Patterns Used

This project heavily utilizes standard design patterns to ensure loose coupling and high cohesion:

### 1. Builder Pattern

**Usage**: constructing the `ParkingLot` object.

- **Why**: Configuring a parking lot with multiple floors and varying spot counts can be complex. The `ParkingLotBuilder` allows step-by-step construction, making the client code clean and readable.
- **File**: `ParkingLotBuilderPattern/ParkingLotBuilder.cs`

### 2. Factory Pattern

**Usage**: Creating `Vehicle` instances.

- **Why**: Centralizes the logic for object creation. If new vehicle types are added, the client code (`Program.cs`) doesn't need to change; only the `VehicleFactory` needs an update.
- **File**: `VehicleFactoryPattern/VehicleFactory.cs`

### 3. Strategy Pattern

**Usage**: Fee Calculation and Payment Processing.

- **Why**:
  - **Fees**: Different vehicles or conditions might require different pricing models (e.g., flat rate vs. hourly). `ParkingFeeStrategy` allows switching algorithms at runtime.
  - **Payments**: Users may pay via different methods. `PaymentStrategy` allows adding new payment methods (e.g., UPI, Wallet) without modifying the core logic.
- **Files**:
  - `FareStrategyPattern/ParkingFeeStrategy.cs`
  - `PaymentStrategyPattern/PaymentStrategy.cs`

### 4. Single Responsibility Principle (SRP)

- **ParkingLot**: Manages the collection of floors.
- **ParkingFloor**: Manages the spots on a specific floor.
- **ParkingSpot**: Manages its own availability and vehicle state.

## 📂 Project Structure

```
Parking Lot/
├── CommonEnum/             # Shared enumerations (e.g., VehicleType, DurationType)
├── FareStrategyPattern/    # Valid strategy interface and concrete implementations for fees
├── ParkingLotBuilderPattern/ # Builder for creating the parking lot
├── ParkingLotController/   # Main controller managing the lot logic (ParkingLot class)
├── ParkingSpots/           # Abstract ParkingSpot and concrete implementations
├── PaymentStrategyPattern/ # Strategy interface and implementations for payments
├── VehicleFactoryPattern/  # Factory and concrete vehicle classes
└── Program.cs              # Entry point demonstrating the usage
```

### Example Usage

The `Program.cs` demonstrates a scenario where:

1.  A multi-floor parking lot is built.
2.  Vehicles (Cars, Bikes) are created using the Factory.
3.  Vehicles are parked in available spots.
4.  Parking fees are calculated based on duration.
5.  Payment is processed using a selected strategy (Cash/Card).

## 🔮 Future Improvements

- Add concurrency support for multi-threaded access.
