using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.Classes;
using ATM_Machine.StatePattern.ATMContext;

namespace ATM_Machine.StatePattern.ConcreteATMStateClasses
{
    // IdleState : Represents the initial state of the ATM, waiting for a user to insert a card.
    public class IdleState : ATMState
    {
        public IdleState()
        {
            Console.WriteLine("ATM is in IdleState - Please insert your card");
        }

        public string GetStateName()
        {
            return "IdleState";
        }

        public ATMState Next(ATMMachineContext context)
        {
            if(context.GetCurrentCard() != null)
            {
                return context.GetStateFactory().CreateHasCardState();
            }
            return this;
        }
    }
}
