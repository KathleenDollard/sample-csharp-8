using System;
using System.Collections.Generic;
using System.Text;

namespace TollCollectorLib
{
    namespace BillingSystem
    {
        public class Account
        {
            public readonly static IEnumerable<Account> SomeAccounts = new List<Account>
            {
                new Account("BSF-846")
            };

            public Account(string license)
                => License = license;

            public string License { get; }

            internal void Charge(decimal toll)
            {
                // DoNothing for now();
            }
        }
    }
}
