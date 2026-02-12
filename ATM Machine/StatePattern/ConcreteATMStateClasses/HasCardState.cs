using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.StatePattern.ATMContext;

namespace ATM_Machine.StatePattern.ConcreteATMStateClasses
{
    // HasCardState : Indicates that a card has been inserted and requires PIN authentication before proceeding.‍
    public class HasCardState : ATMState
    {
        public HasCardState() {
            Console.WriteLine("ATM is in HasCardState - Please enter your PIN");
        }

        public string GetStateName()
        {
            return "HasCardState";
        }

        public ATMState Next(ATMMachineContext context)
        {
            if (context.GetCurrentCard() == null)
            {
                return context.GetStateFactory().CreateIdleState();
            }
            if (context.GetCurrentAccount() != null)
            {
                return context.GetStateFactory().CreateSelectOperationState();
            }
            return this;
        }
    }
}
