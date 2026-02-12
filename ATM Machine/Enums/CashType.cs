using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.Enums
{
    // This enum represents different cash denominations available in ATM. Each denomination has an associated value
    public enum CashType
    {
        BILL_100 = 100,
        BILL_50 = 50,
        BILL_20 = 20,
        BILL_10 = 10,
        BILL_5 = 5,
        BILL_1 = 1
    }
}

/*
 Usage in C#

CashType c = CashType.BILL_50;

int value = (int)c;   // 50
Console.WriteLine(value); // 50

 */