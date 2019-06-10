using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TollCollectorLib
{
    namespace BillingSystem
    {
        public class Account
        {
            public readonly static IEnumerable<Account> SomeAccounts = new List<Account>
            {
                new Account("BSF-846-WA")
            };

            public Account(string license)
                => License = license;

            public string License { get; }

            internal void Charge(decimal toll) 
                => // Dummy charge action
                Console.WriteLine($"Charging toll: {toll}");

            internal static async Task<Account> LookupAccountAsync(string license)
            {
                await Task.Delay(300);
                Account account = Account.SomeAccounts.Where(a => a.License == license).SingleOrDefault();
                return account is null ? null : account; // because nullable didn't pick up SingleOrDefault yet.
            }


        }

        public class Owner
        {
            public Owner(string state, string plate)
            {
                State = state;
                Plate = plate;
            }

            public string State { get; }
            public string Plate { get; }

            internal static async Task<Owner> LookupOwnerAsync(string state, string plate)
            {
                await Task.Delay(300);
                return new Owner(state, plate);
            }

            internal void SendBill(decimal finalToll)
                => // Dummy send Bill Action
                Console.WriteLine($"Sending bill: {finalToll}");
        }
    }

}
