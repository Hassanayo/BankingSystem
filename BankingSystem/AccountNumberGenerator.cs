using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    internal class AccountNumberGenerator
    {
        
        public HashSet<int> usedAccountNumbers = new HashSet<int>();

        /// <summary>
        /// Generate a random 8 digit account number starting with 21 for new users
        /// </summary>
        /// <returns>Account number</returns>
        public int GenerateAccountNumber()
        {
            int accountNumber = 0;
            // ensure the generated account number is not already in the set
            do
            {
                Random random = new Random();
                accountNumber = 21000000 + random.Next(0, 999999);
            }
            while(usedAccountNumbers.Contains(accountNumber));

            usedAccountNumbers.Add(accountNumber);

            return accountNumber;
        }
    }
}
