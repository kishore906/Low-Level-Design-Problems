using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Machine.StatePattern.ATMContext;

namespace ATM_Machine.StatePattern
{
    /*
     Why State Pattern?

    ==> Encapsulates state-specific behaviour
    ==> Manages state transitions (Idle, HasCard, SelectCard, CashWithdrawal, CheckBalance)
    ==> Prevents invalid operations based on current state
     */

    //  ATM State Interface : Defines the contract for all ATM states, ensuring consistency across different states by defining the necessary operations.
    public interface ATMState
    {
        // Get the name of the current state
        string GetStateName();

        // Method to handle state transitions
        ATMState Next(ATMMachineContext context);
    }
}
