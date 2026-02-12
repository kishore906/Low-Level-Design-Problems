using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.Classes
{
    // This class represents user's bank account
    public class Account
    {
        private string accountNumber;
        private double balance;

        // Constructor
        public Account(string accountNumber, double initialBalance)
        {
            this.accountNumber = accountNumber;
            this.balance = initialBalance;
        }

        public string GetAccountNumber() { 
            return accountNumber;
        }

        public double GetBalance() { return balance; }

        public void Deposit(double amount) { 
            balance += amount;
        }

        public bool Withdraw(double amount) { 
            if(balance >= amount)
            {
                balance -= amount;
                return true;
            }
            return false;
        }
    }
}
