using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    internal class Account
    {
        int accountNumber;
        string firstName;
        string lastName;
        int pin;
        double balance;

        /// <summary>
        /// Creates a new instance of the Account class with the specified account number, first name, last name, pin and balance
        /// </summary>
        /// <param name="accountNumber">The account number for this account</param>
        /// <param name="firstName">The first name associated with this account</param>
        /// <param name="lastName">The last name associated with this account</param>
        /// <param name="pin">The private pin associated with this account</param>
        /// <param name="balance">The balace of the account</param>
        public Account( int accountNumber, string firstName, string lastName, int pin, double balance) 
        {
            this.accountNumber = accountNumber;
            this.firstName = firstName;
            this.lastName= lastName;
            this.pin = pin;
            this.balance = balance;
        }

        /// <summary>
        /// Creats a database to store user data
        /// </summary>
        static class AccountDatabase
        {
            public static ConcurrentBag<Account> accounts = new ConcurrentBag<Account>();
        }

        /// <summary>
        /// Gets or Sets the account number for the current account
        /// </summary>
        public int AccountNumber { get { return accountNumber; } set { balance = value;  } }
        /// <summary>
        /// Gets or Sets the first name for the current account
        /// </summary>
        public string FirstName { get => firstName ; set => firstName = value; }
        /// <summary>
        /// Gets or Sets the last name for the current account
        /// </summary>
        public string LastName { get => lastName; set=> lastName = value; }
        /// <summary>
        /// Gets or Sets the Pin for the current account
        /// </summary>
        public int Pin { get => pin; set => pin = value; }
        /// <summary>
        /// Gets or Sets the Balance for the current account
        /// </summary>
        public double Balance { get => balance; set => balance = value; }

        

    }

}
