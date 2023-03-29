using BankingSystem;
using System.Collections.Concurrent;


namespace BankingSystem
{
    class Program
    {

        public static void Main(string[] args)
        {
            // Library of users to call upon
            Account account1 = new Account(21547807, "Ayomide", "Hassan", 2344, 323.23);
            Account account2 = new Account(21214277, "Jack", "Sparrow", 6096, 4000.00);
            Account account3 = new Account(21214277, "John", "Smith", 1234, 50.0 );

            AccountsDatabase.accounts.Add(account1);
            AccountsDatabase.accounts.Add(account2);

            while (true)
            {
                switch (BankProcess.MainMenu())
                {
                    case 1: BankProcess.Login();
                        break;
                    case 2: BankProcess.SignUp();
                        break;
                    case 3:
                        Console.WriteLine("\n");
                        BankProcess.Center("Thanks for using our service!");
                        BankProcess.Center("Press any key to close the console!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n");
                        BankProcess.Center("Incorrect Option | Try Again!");
                        break;
                }
            }
            
        }
    }
    static class AccountsDatabase
    {
        public static ConcurrentBag<Account> accounts = new ConcurrentBag<Account>();
        
    }
}