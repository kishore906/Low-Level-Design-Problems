using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking_Lot.PaymentStrategyPattern
{
    public interface PaymentStrategy
    {
        void ProcessPayment(double amount);
    }
}
