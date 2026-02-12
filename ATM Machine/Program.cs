
using ATM_Machine.Classes;
using ATM_Machine.Enums;
using ATM_Machine.StatePattern.ATMContext;

/*
ATMInventory aTMInventory = new ATMInventory();

int cash = aTMInventory.GetTotalCash();
Console.WriteLine($"Total cash: {cash}");

var dispensedCash = aTMInventory.DispenseCash(186);
if (dispensedCash == null)
{
    Console.WriteLine("Cannot dispense exact amount.");
}
else
{
    foreach (var item in dispensedCash)
    {
        Console.WriteLine($"${(int)item.Key} -> {item.Value}");
    }
}
*/

// Create and initialize ATM
ATMMachineContext atm =  new ATMMachineContext();

// Add sample accounts
atm.AddAccount(new Account("123456", 1000.0));
atm.AddAccount(new Account("654321", 500.0));

try
{
    // Sample workflow
    Console.WriteLine("===== Starting ATM Demo =====");

    // Insert Card
    atm.InsertCard(new Card("123456", 1234, "654321"));

    // Enter PIN
    atm.EnterPIN(1234);

    // Select Operation
    atm.SelectOperation(TransactionType.WITHDRAW_CASH);

    // Perform transaction
    atm.PerformTransaction(100.0);

    // Select another operation
    atm.SelectOperation(TransactionType.CHECK_BALANCE);

    // Perform balance check
    atm.PerformTransaction(0.0);

    // Return card
    atm.ReturnCard();

    Console.WriteLine("=== ATM Demo Completed ===");
}
catch(Exception ex)
{
    Console.WriteLine("Error: ", ex.Message);
}

/*

Output :

ATM is in Idle State - Please insert your card
ATM initialized in: IdleState

=== Starting ATM Demo ===
Card inserted
ATM is in Has Card State - Please enter your PIN
Current state: HasCardState
PIN authenticated successfully

ATM is in Select Operation State - Please select an operation
1. Withdraw Cash
2. Check Balance
Current state: SelectOperationState
Selected operation: WITHDRAW_CASH
ATM is in Transaction State
Current state: TransactionState
Transaction successful. Please collect your cash:
1 x $100

ATM is in Select Operation State - Please select an operation
1. Withdraw Cash
2. Check Balance
Current state: SelectOperationState
Selected operation: CHECK_BALANCE
ATM is in Transaction State
Current state: TransactionState
Your current balance is: $400.0

ATM is in Select Operation State - Please select an operation
1. Withdraw Cash
2. Check Balance
Current state: SelectOperationState
Card returned to customer
ATM is in Idle State - Please insert your card
=== ATM Demo Completed ===

Process finished with exit code 0


*/