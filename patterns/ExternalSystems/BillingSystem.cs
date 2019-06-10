using System;
using System.Collections.Generic;
using System.Linq;
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
                new Account("BSF-846-WA")
            };

            public Account(string license)
                => License = license;

            public string License { get; }

            public void Charge(decimal toll) 
                => // Dummy charge action
                Console.WriteLine($"Charging toll: {toll}");

            public static async Task<Account?> LookupAccountAsync(string license)
            {
                await Task.Delay(300);
                Account account = SomeAccounts.Where(a => a.License == license).SingleOrDefault();
                // temporary because nullable didn't pick up SingleOrDefault yet.
                return account is null ? null : account; 
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
