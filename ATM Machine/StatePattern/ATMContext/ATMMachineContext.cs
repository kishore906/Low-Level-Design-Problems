using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.Classes;
using ATM_Machine.Enums;
using ATM_Machine.StatePattern.ATMFactory;
using ATM_Machine.StatePattern.ConcreteATMStateClasses;

namespace ATM_Machine.StatePattern.ATMContext
{
    // The ATM Machine context class: class which contains machine's core functionality and operations
    // This class holds the current state of the ATM and delegates behavior to it.
    public class ATMMachineContext
    {
        // Current state of ATM (Idle, HasCard, SelectOperation, Transaction)
        private ATMState currentState;

        // Currently inserted Card
        private Card currentCard = null!;

        // Bank account linked to the inserted card
        private Account currentAccount = null!;

        // ATM cash inventory
        private ATMInventory atmInventory;

        // Simplified in-memory account storage (AccountNumber -> Account)
        private Dictionary<string, Account> accounts;

        // Factory to create state objects (Singleton + Factory pattern)
        private ATMStateFactory stateFactory;

        // Selected transaction type (Withdraw, Check Balance, etc.)
        private TransactionType? selectedOperation;

        // Constructor: Initialize ATM in Idle state
        public ATMMachineContext()
        {
            this.stateFactory = ATMStateFactory.GetInstance();
            this.currentState = stateFactory.CreateIdleState();
            this.atmInventory = new ATMInventory();
            this.accounts = new Dictionary<string, Account>();

            Console.WriteLine("ATM initialized in: " + currentState.GetStateName());
        }

        // Move ATM to the next state based on current state logic
        public void AdvanceState() {
            ATMState nextState = currentState.Next(this);
            currentState = nextState;
            Console.WriteLine("Current state: " + currentState.GetStateName());
        }

        // Card insertion operation
        public void InsertCard(Card card) {
            if (currentState is IdleState)
            {
                Console.WriteLine("Card inserted");
                this.currentCard = card;
                AdvanceState();
            }
            else
            {
                Console.WriteLine("Cannot insert card in " + currentState.GetStateName());
            }
        }

        // PIN authentication
        public void EnterPIN(int pin) { 
            if(currentState is HasCardState)
            {
                if (currentCard.ValidatePin(pin))
                {
                    Console.WriteLine("PIN authenticated successfully.");

                    // Fetch linked account
                    currentAccount = accounts[currentCard.GetAccountNumber()];

                    AdvanceState();
                }
                else
                {
                    Console.WriteLine("Invalid PIN. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Cannot enter PIN in " + currentState.GetStateName());
            }
        }

        // Select transaction operation (Withdraw, Balance check, etc.)
        public void SelectOperation(TransactionType transactionType) {
            if (currentState is SelectOperationState)
            {
                Console.WriteLine("Selected operation: " + transactionType);
                this.selectedOperation = transactionType;
                AdvanceState();
            }
            else
            {
                Console.WriteLine("Cannot select operation in " + currentState.GetStateName());
            }
        }

        // Perform the selected transaction
        public void PerformTransaction(double amount) { 
            if(currentState is TransactionState)
            {
                try
                {
                    if (selectedOperation == TransactionType.WITHDRAW_CASH)
                    {
                        PerformWithdrawal(amount);
                    }
                    else if (selectedOperation == TransactionType.CHECK_BALANCE)
                    {
                        CheckBalance();
                    }

                    // After transaction, move to next state (e.g., ask for another operation)
                    AdvanceState();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Transaction Failed: ", ex.Message);

                    // Reset to Select Operation State
                    currentState = stateFactory.CreateSelectOperationState();
                }
            }
            else
            {
                Console.WriteLine("Cannot perform transaction in " + currentState.GetStateName());
            }
        }

        // Return card to user and reset ATM
        public void ReturnCard() {
            if (currentState is HasCardState
                || currentState is SelectOperationState
                || currentState is TransactionState)
            {
                Console.WriteLine("Card returned to customer");
                ResetATM();
            }
            else
            {
                Console.WriteLine("No card to return in " + currentState.GetStateName());
            }
        }

        // Cancel current transaction
        public void CancelTransaction() {
            if (currentState is TransactionState)
            {
                Console.WriteLine("Transaction cancelled");
                ReturnCard();
            }
            else
            {
                Console.WriteLine("No transaction to cancel in " + currentState.GetStateName());
            }
        }

        // Perform cash withdrawal
        private void PerformWithdrawal(double amount) {
            // 1. Check account balance
            if (!currentAccount.Withdraw(amount))
            {
                throw new Exception("Insufficient funds in account");
            }

            // 2. Check ATM cash availability
            if (!atmInventory.HasSufficientCash((int)amount))
            {
                currentAccount.Deposit(amount); // rollback account withdrawal
                throw new Exception("Insufficient cash in ATM");
            }

            // 3. Try to dispense exact cash 
            var dispensedCash = atmInventory.DispenseCash((int)amount);

            if (dispensedCash == null)
            {
                currentAccount.Deposit(amount); // rollback
                throw new Exception("Unable to dispense exact amount");
            }

            Console.WriteLine("Transaction successful. Please collect your cash:");

            foreach (var entry in dispensedCash)
            {
                Console.WriteLine($"{entry.Value} x ${(int)entry.Key}");
            }
        }

        //Check account balance
        private void CheckBalance() {
            Console.WriteLine("Your current balance is: $" + currentAccount.GetBalance());
        }

        // Reset ATM to Idle state
        private void ResetATM() {
            this.currentCard = null!;
            this.currentAccount = null!;
            this.selectedOperation = null;
            this.currentState = stateFactory.CreateIdleState();
        }

        // Getters and setters
        public ATMState GetCurrentState() => currentState;
        public void SetCurrentState(ATMState state) => currentState = state;
        public Card GetCurrentCard() => currentCard;
        public Account GetCurrentAccount() => currentAccount;
        public ATMInventory GetATMInventory() => atmInventory;
        public TransactionType? GetSelectedOperation() => selectedOperation;
        public ATMStateFactory GetStateFactory() => stateFactory;

        // Add demo accounts
        public void AddAccount(Account account)
        {
            accounts[account.GetAccountNumber()] = account;
        }

        // Fetch account by account number
        public Account GetAccount(string accountNumber)
        {
            return accounts[accountNumber];
        }
    }
}
