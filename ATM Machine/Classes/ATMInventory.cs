using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.Enums;

namespace ATM_Machine.Classes
{
    // This class manages ATM's cash inventory
    public class ATMInventory
    {
        private readonly Dictionary<CashType, int> cashInventory;

        // Constructor
        public ATMInventory() { 
            cashInventory = new Dictionary<CashType, int>();
            InitializeInventory();
        }

        // Initilaize inventory with some cash
        public void InitializeInventory() {
            cashInventory[CashType.BILL_100] = 10;
            cashInventory[CashType.BILL_50] = 10;
            cashInventory[CashType.BILL_20] = 20;
            cashInventory[CashType.BILL_10] = 30;
            cashInventory[CashType.BILL_5] = 20;
            cashInventory[CashType.BILL_1] = 50;
        }

        // Get total cash available in the ATM
        public int GetTotalCash() {
            int total = 0;

            foreach (var entry in cashInventory) {
                //Console.WriteLine(entry.Key);
                total += (int)entry.Key * entry.Value; // enum cast to int
            }
            return total;
        }

        // Check if ATM has sufficient cash for a withdrawal
        public bool HasSufficientCash(int amount)
        {
            return GetTotalCash() >= amount;
        }

        // Add cash to inventory (for maintenance/refill)
        public void AddCash(CashType cashType, int count) {
            cashInventory[cashType] += count;
        }

        // Dispense cash for withdrawal
        public Dictionary<CashType, int>? DispenseCash(int amount)
        {
            // Check if ATM has enough cash or not
            if(!HasSufficientCash(amount)) return null;

            var dispensedCash = new Dictionary<CashType, int>(); // what we plan to give to user
            int remainingAmount = amount; // how much is still left to fulfill

            // dispense from largest denomination to smallest

            /*
             Array: array of enum values
            [BILL_100, BILL_50, BILL_20, BILL_10, BILL_5, BILL_1]
             */
            Array values = Enum.GetValues(typeof(CashType));

            /*
             Above Array is not strongly types, we can't use directly LINQ on it
            
            So, we use Cast<CashType>()
            What it does:
            ==> Converts Array → IEnumerable<CashType>
            ==> Makes each element strongly typed
             */
            IEnumerable<CashType> cashTypes = values.Cast<CashType>();

            /*
             .OrderByDescending(c => (int)c) ==> Sorts enum values by their integer value from largest to smallest
             */
            IEnumerable<CashType> sortedCashTypes = cashTypes.OrderByDescending(c => (int)c);

            foreach(CashType cashType in sortedCashTypes)
            {
                Console.WriteLine((int)cashType); // 100 → 50 → 20 → 10 → 5 → 1

                int denomination = (int)cashType; // means 100, 50, 20,....
                int available = cashInventory[cashType]; // total notes available for each denomination i,e 100 -> 10, 50 -> 10 etc

                /*
                 | Note | Remaining | Max possible | Available | We take |
                 | ----     | ---------       | ------------        | ---------     | ------- |
                 | 100     | 186            | 1                      | 10             | 1       |
                 | 50       | 86               | 1                      | 10            | 1       |
                 | 20       | 36              | 1                       | 20            | 1       |
                 | 10       | 16              | 1                       | 30            | 1       |
                 | 5         | 6                | 1                       | 20             | 1       |
                 | 1         | 1                | 1                       | 50             | 1       |
                 */
                int count = Math.Min(remainingAmount / denomination, available);

                if(count > 0)
                {
                    dispensedCash[cashType] = count; // Record what you’re giving
                    remainingAmount -= count * denomination; // Reduce remaining amount
                    cashInventory[cashType] -= count; //    Reduce ATM inventory

                    // until above if remainingAmount = 0; means dispense succeeds
                }
            }

            // If exact change is not possible -> we do rollback
            if (remainingAmount > 0)
            {
                foreach (var entry in dispensedCash)
                {
                    cashInventory[entry.Key] += entry.Value;  // rollback
                }
                return null;
            }

            return dispensedCash;
        } 
    }
}
