using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.Classes
{
    // This class represents user's bank card
    public class Card
    {
        private string cardNumber;
        private int pin;
        private string accountNumber;

        // Constructor
        public Card(string cardNumber, int pin, string accountNumber) {
            this.cardNumber = cardNumber;
            this.pin = pin;
            this.accountNumber = accountNumber;
        }

        // getters and setters
        public string GetCardNumber()
        {
            return cardNumber;
        }

        public bool ValidatePin(int enteredPin) {
            return this.pin == enteredPin;
        }

        public string GetAccountNumber()
        {
            return accountNumber;
        }
    }
}
