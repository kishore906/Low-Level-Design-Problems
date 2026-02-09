using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Lot.PaymentStrategyPattern
{
    public class Payment
    {
        private double Amount { get; set; }
        private PaymentStrategy PaymentStrategy { get; set; } // PaymentStrategy interface

        // Constructor to initialize the payment amount and payment strategy
        public Payment(double amount, PaymentStrategy paymentStrategy)
        {
            Amount = amount;
            PaymentStrategy = paymentStrategy;
        }

        // Process the payment using the assigned strategy
        public void ProcessPayment()
        {
            if(Amount > 0)
            {
                PaymentStrategy.ProcessPayment(Amount); // delegating to strategy
            }
            else
            {
                Console.WriteLine("Invalid payment amount.");
            }
        }
    }
}
