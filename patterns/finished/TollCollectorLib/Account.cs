using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

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

            internal static async Task<Account?> LookupAccountAsync(string license)
            {
                await Task.Delay(300);
                var account = Account.SomeAccounts.Where(a => a.License == license).SingleOrDefault();
                return (account is null ? null : account); // because nullable didn't pick up SingleOrDefault yet.
            }
        }
    }
}
