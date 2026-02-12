using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.StatePattern.ConcreteATMStateClasses;

namespace ATM_Machine.StatePattern.ATMFactory
{
    // ATM State Factory : A singleton factory class responsible for creating instances of different ATM states.
    public class ATMStateFactory
    {
        private static ATMStateFactory instance = null;

        private ATMStateFactory() { }

        public static ATMStateFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ATMStateFactory();
            }
            return instance;
        }

        public ATMState CreateIdleState()
        {
            return new IdleState();
        }

        public ATMState CreateHasCardState()
        {
            return new HasCardState();
        }

        public ATMState CreateSelectOperationState()
        {
            return new SelectOperationState();
        }

        public ATMState CreateTransactionState()
        {
            return new TransactionState();
        }
    }
}
