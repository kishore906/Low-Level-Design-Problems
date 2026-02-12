# ATM Machine System Design

This project is a Low-Level Design (LLD) implementation of an **ATM (Automated Teller Machine)** system in C#. It demonstrates the use of behavioral design patterns to manage the complex states and transitions of an ATM.

## 📌 Project Overview

The ATM system simulates the lifecycle of a user transaction, starting from card insertion to cash dispensing and card return. It handles various operations such as:

- Card Validation
- PIN Authentication
- Cash Withdrawal (with denomination logic)
- Balance Inquiry
- State Management (Idle, HasCard, SelectOperation, Transaction)

## 🏗️ Design Patterns Used

The core architecture relies on the following design patterns:

### 1. State Design Pattern (Core)

The ATM's behavior changes depending on its internal state. The **State Pattern** allows the `ATMMachineContext` to delegate behavior to the current state object.

- **State Interface**: `ATMState`
- **Concrete States**:
  - `IdleState`: Waiting for card insertion.
  - `HasCardState`: Card inserted, waiting for PIN.
  - `SelectOperationState`: User authenticated, waiting for transaction selection.
  - `TransactionState`: Processing withdrawal or balance check.

### 2. Singleton Pattern

Used in `ATMStateFactory`.

- Ensures only **one instance** of the factory exists throughout the application lifecycle to manage state creation efficiently.

### 3. Factory Pattern

Used in `ATMStateFactory`.

- Centralizes the creation of state objects (`CreateIdleState`, `CreateHasCardState`, etc.).
- Decouples the `ATMMachineContext` from the specific concrete state classes.

## 📂 Project Structure

```
ATM Machine/
├── Classes/
│   ├── Account.cs        # Represents a bank account
│   ├── ATMInventory.cs   # Manages cash breakdown (100s, 50s, etc.)
│   └── Card.cs           # Represents a user's physical card
├── Enums/
│   ├── CashType.cs       # Denominations (1, 5, 10, 20, 50, 100)
│   └── TransactionType.cs # Types of transactions
├── StatePattern/
│   ├── ATMContext/
│   │   └── ATMMachineContext.cs # Context class holding the current state
│   ├── ATMFactory/
│   │   └── ATMStateFactory.cs   # Singleton Factory for states
│   ├── ConcreteATMStateClasses/ # Implementation of various states
│   └── ATMState.cs              # State Interface
└── Program.cs            # Entry point / Demo simulation
```

## 🚀 How It Works

1. **Initialization**: The ATM starts in `IdleState` with a loaded cash inventory.
2. **Card Insertion**: User inserts a card -> State transitions to `HasCardState`.
3. **Authentication**: User enters PIN.
   - If correct: Transition to `SelectOperationState`.
   - If incorrect: Error displayed, remains in current state.
4. **Operation**: User selects logic (e.g., Withdraw Cash).
   - Transition to `TransactionState`.
5. **Processing**:
   - `ATMInventory` calculates the best combination of bills to dispense (e.g., greedy approach).
   - Account balance is updated.
6. **Completion**: Card is returned, and ATM resets to `IdleState`.
