using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem;

namespace BankingSystem
{
    internal class BankProcess
    {
        /// <summary>
        /// The interface of the bank app and it prompts the user for what they would like to do
        /// </summary>
        /// <returns>the number the user selected to specify which option was chosen</returns>
        public static int MainMenu()
        {
            Account currentUser;
            currentUser = null;
            Console.Clear();
            Center("**** Welcome to CSC Banking System ****\n");
            DrawLine();
            Console.WriteLine("|{0}|", AlignText(0, ""));
            Console.WriteLine("|{0}|", AlignText(35, "1. Login for Existing Customers"));
            Console.WriteLine("|{0}|", AlignText(35, "2. Open a new Account"));
            Console.WriteLine("|{0}|", AlignText(35, "3. Exit"));
            Console.WriteLine("|{0}|", AlignText(0, ""));
            DrawLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n{0}", AlignText(36, "Enter your choice : ", "L"));
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                return 0;
            }

        }

        /// <summary>
        /// After the user is logged in, this is the menu shown to allow them select an option
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        /// <returns>The number the user selected to specify which option was chosen</returns>
        public static int LoggedInMenu(Account currentUser)
        {
            Console.Clear();
            Center("**** GTBPI Banking System | Welcome " + currentUser.FirstName + " ****\n");
            DrawLine();
            Console.WriteLine("|{0}|", AlignText(0, ""));
            Console.WriteLine("|{0}|", AlignText(37, "1. Deposit Money"));
            Console.WriteLine("|{0}|", AlignText(37, "2. Withdraw Money"));
            Console.WriteLine("|{0}|", AlignText(37, "3. Tranfer Money"));
            Console.WriteLine("|{0}|", AlignText(37, "4. Show My Account Details"));
            Console.WriteLine("|{0}|", AlignText(37, "5. Logout"));
            Console.WriteLine("|{0}|", AlignText(0, ""));
            DrawLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n{0}", AlignText(38, "Enter your choice : ", "L"));
            try
            {
                return (int.Parse(Console.ReadLine()));
            }
            catch (FormatException)
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Allows a user to create a new account
        /// </summary>
        public static void SignUp()
        {
            AccountNumberGenerator number = new AccountNumberGenerator();
            int accountNumber = number.GenerateAccountNumber();

            Console.Clear();
            Center("Signup Page");
            DrawLine();
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Set your pin: ");
            int pin = int.Parse(Console.ReadLine());
            Console.Write("How much do you want to deposit: ");
            double deposit = double.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            DrawLine();
            
            Account account = new Account(accountNumber, firstName, lastName, pin, deposit);
            AccountsDatabase.accounts.Add(account);
            Console.WriteLine("Account signup successful | Your generated account number is " + accountNumber);
            Console.ReadKey();

        }

        /// <summary>
        /// Allows the current logged in user to deposit money into their account
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        public static void Deposit(Account currentUser)
        {
            Console.Clear();
            Center("Deposit Money");
            DrawLine();
            Console.WriteLine("How much would you like to deposit? ");
            double deposit = Convert.ToDouble(Console.ReadLine());
            currentUser.Balance = (currentUser.Balance + deposit);
            Console.WriteLine("Deposit successful. Your new balance is " + currentUser.Balance);
        }

        /// <summary>
        /// Allows the current logged in user to withdraw money from their account
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        public static void Withdraw(Account currentUser)
        {
            Console.Clear();
            Console.WriteLine("How much would you like to withdraw? ");
            double withdrawal = Convert.ToDouble(Console.ReadLine());

            // check if user has enough money to withdraw
            if (withdrawal > currentUser.Balance)
            {
                Console.WriteLine("Insufficient funds.");
            }
            else
            {
                double balance = currentUser.Balance - withdrawal;
                currentUser.Balance = (balance);
            }
            Console.WriteLine("Withdrawal succesful. New balance is " + currentUser.Balance);
        }

        /// <summary>
        /// Shows the current balance of the user
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        public static void ShowBalance(Account currentUser)
        {

            Console.WriteLine("Current balance is " + currentUser.Balance);
        }

        /// <summary>
        /// Shows the account details of the current logged in user
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        public static void ShowDetails(Account currentUser)
        {
            Console.Clear();
            Center("Account Details");
            Console.WriteLine("Name: "+ currentUser.FirstName + " " + currentUser.LastName);
            Console.WriteLine("Account Number: " + currentUser.AccountNumber);
            Console.WriteLine("Account Balance: " + currentUser.Balance);

        }

        /// <summary>
        /// Allows transfer of money between accounts
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        public static void Transfer(Account currentUser)
        {
            Console.Clear();
            Center("Transfer Money");
            DrawLine();
            Console.Write("Enter the amount you want to transfer: ");
            double transferAmount = double.Parse(Console.ReadLine());
            while (true)
            {
                try
                {
                    if (transferAmount <= currentUser.Balance)
                    {
                        Console.WriteLine("Enter the account number you would like to transfer to: ");
                        int receiverAccountNumber = int.Parse(Console.ReadLine());
                        Account receiver = AccountsDatabase.accounts.FirstOrDefault(a => a.AccountNumber == receiverAccountNumber);
                        if (receiver != null)
                        {
                            // subtract the amount from the users balance
                            double newBalance = currentUser.Balance - transferAmount;
                            currentUser.Balance = (newBalance);
                            // add the new amount to the receiver's account
                            receiver.Balance = (receiver.Balance + transferAmount);

                            Console.WriteLine($"Transferred {transferAmount:C} to {receiver.FirstName} {receiver.LastName} (Account No. {receiver.AccountNumber}) successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Account not found. Try again");
                        }



                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance for this transaction");
                    }

                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            
           
            

        }
        /// <summary>
        /// Allows the user to be able to log out of their current session
        /// </summary>
        /// <param name="currentUser">The current user logged in</param>
        public static void Logout()
        {
            Console.WriteLine("\n");
            Center("Thanks for using our service!");
            Center("You have been successfully logged out!");
            Center("Press any key to go back to the main menu!");
            Console.ReadKey();
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);


        }

        /// <summary>
        /// Allows a user to be able to log into their account as it verifies their account details
        /// </summary>
        public static void Login()
        {
            Console.Clear();
            Center("Login Page");
            DrawLine();
            Account currentUser;
            Console.WriteLine("Enter your account number: ");
            int accountNumber = 0;
            while (true)
            {
                try
                {
                    accountNumber = Convert.ToInt32(Console.ReadLine());
                    // check against our db to see if the account exists
                    currentUser = AccountsDatabase.accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                    if (currentUser != null)
                    {
                        Console.WriteLine("Enter your pin: ");
                        int userPin = 0;
                        while (true)
                        {
                            try
                            {
                                userPin = Convert.ToInt32(Console.ReadLine());
                                if (currentUser.Pin == userPin)
                                {
                                    bool loggedInFlag = true;
                                    while (loggedInFlag)
                                    {
                                        switch (LoggedInMenu(currentUser))
                                        {
                                            case 1:
                                                Deposit(currentUser);
                                                break;
                                            case 2:
                                                Withdraw(currentUser);
                                                break;
                                            case 3:
                                                Transfer(currentUser);
                                                break;
                                            case 4:
                                                ShowDetails(currentUser);
                                                break;
                                            case 5:
                                                loggedInFlag = false;
                                                Logout();
                                                
                                                break;
                                            default:
                                                Console.WriteLine("Incorrect Option. Try again");
                                                break;
                                        }
                                        if (loggedInFlag)
                                        {
                                            Console.ReadKey();

                                        }

                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect Pin. Try again.");
                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized account number. Try again");
                    }
                    

                }
                catch (Exception e)
                {
                    Console.WriteLine("Unrecognized account number. Try again" + e.Message);
                }
            }
            


        }

        /// <summary>
        /// Aligns text in the console
        /// </summary>
        /// <param name="SpacesToAdd">Amount of space to be added</param>
        /// <param name="Msg">Text to be aligned</param>
        /// <param name="Alignment">Specifies whether text should be aligned to the right or left</param>
        /// <returns></returns>
        public static string AlignText(int SpacesToAdd, string Msg, string Alignment = "R")
        {
            if (Alignment == "L")
                Msg = Msg.PadLeft(SpacesToAdd + Msg.Length);
            else
            {
                Msg = Msg.PadLeft(SpacesToAdd + Msg.Length);
                Msg = Msg.PadRight((98 - Msg.Length) + Msg.Length);
            }
            return Msg;
        } 

        /// <summary>
        /// Draws a line
        /// </summary>
        public static void DrawLine()
        {
            Console.WriteLine("+--------------------------------------------------------------------------------------------------+");
        }

        /// <summary>
        /// Centers a text
        /// </summary>
        /// <param name="message">text to be centered</param>
        public static void Center(string message)
        {
            int spaces = 50 + (message.Length / 2);
            Console.WriteLine(message.PadLeft(spaces));
        }
    }
}
