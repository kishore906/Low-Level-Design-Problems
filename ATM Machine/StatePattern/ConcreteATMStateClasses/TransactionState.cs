using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ATM_Machine.StatePattern.ATMContext;

namespace ATM_Machine.StatePattern.ConcreteATMStateClasses
{
    // TransactionState : Represents the state where a user performs a transaction after successful authentication.
    public class TransactionState : ATMState
    {
        public TransactionState()
        {
            Console.WriteLine("ATM is in Transaction State");
        }

        public string GetStateName()
        {
            return "TransactionState";
        }

        public ATMState Next(ATMMachineContext context)
        {
            if (context.GetCurrentCard() == null)
            {
                return context.GetStateFactory().CreateIdleState();
            }

            // After transaction completion, go back to select operation
            return context.GetStateFactory().CreateSelectOperationState();
        }
    }
}
