using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Lot.PaymentStrategyPattern.ConcretePaymentStrategies
{
    public class CashPayment : PaymentStrategy
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing cash payment of ${amount}");
            // Logic for cash payment processing
        }
    }
}
