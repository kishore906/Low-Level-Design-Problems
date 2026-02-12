using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.StatePattern.ATMContext;

namespace ATM_Machine.StatePattern.ConcreteATMStateClasses
{
    // SelectOperationState : Represents the state where a user selects an operation after successful authentication.
    public class SelectOperationState : ATMState
    {
        public SelectOperationState()
        {
            Console.WriteLine("ATM is in SelectOperationState - Please select an operation");
            Console.WriteLine("1. Withdraw Cash");
            Console.WriteLine("2. Check Balance");
        }

        public string GetStateName()
        {
            return "SelectOperationState";
        }

        public ATMState Next(ATMMachineContext context)
        {
            if (context.GetCurrentCard() == null)
            {
                return context.GetStateFactory().CreateIdleState();
            }

            if (context.GetSelectedOperation() != null)
            {
                return context.GetStateFactory().CreateTransactionState();
            }
            return this;
        }
    }
}
