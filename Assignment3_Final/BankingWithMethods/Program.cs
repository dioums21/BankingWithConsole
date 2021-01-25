using System;
using static System.Console;

namespace BankingWithMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            AppLogin();
            string CompanyName = "", SavingAcct = ""; //Initiators for Saving Plans for Overview purpose
            double StockOwned = 0, SavingBal = 0; //Initiators for Stock purchase for Overview purpose
            double userBalance = 7857.45; //Initial User Balance
            var SessionPin = new System.Random();
            int userPin = SessionPin.Next(100000, 999999);
            WriteLine("Your PIN for this session is: {0}",userPin);
            WriteLine("Your current balance is: {0}", userBalance.ToString("c"));
            int userSelection;
            MenuSelection();
            Write("Selection: ");
            userSelection = Convert.ToInt32(ReadLine());
            while (userSelection != 6) //Program will run until user enters "6" to Exit it
            {
                switch (userSelection)
                {
                    case 1:
                        WriteLine("***Deposit***");
                        Write("Deposit Amount: $");
                        double UserAmount = Convert.ToDouble(ReadLine());
                        userBalance += UserAmount;
                        WriteLine("Your new balance is: {0}", userBalance.ToString("c"));
                        break;
                    case 2:
                        WriteLine("***Saving Plans***");
                        string saving = "!";
                        double newBal1 = 0, deposit = 0;
                        SavingPlansAndInterests(userBalance,ref saving, ref newBal1, ref deposit);
                        SavingAcct = saving; 
                        SavingBal = deposit;
                        userBalance = newBal1;
                        break;
                    case 3:
                        WriteLine("***Purchase Stocks***");
                        string company="!";
                        double newBal = 0, stock = 0;
                        CompanyStockSelection(userBalance,ref newBal, ref stock, ref company);
                        userBalance = newBal;
                        StockOwned = stock;
                        CompanyName = company;
                        break;
                    case 4:
                        WriteLine("***Change PIN number***");
                        PinNumberChange(userPin);
                        break;
                    case 5:
                        WriteLine("***Asset Overview***");
                        WriteLine("Deposit Account: {0}",userBalance.ToString("c"));
                        WriteLine("=================================");
                        WriteLine("Savings Account Plan: {0} Plan",SavingAcct);
                        WriteLine("Balance: {0}", SavingBal.ToString("c"));
                        WriteLine("=================================");
                        WriteLine("Stock Purchased: {0} - {1}",CompanyName,StockOwned.ToString("c"));
                        break;
                }
                WriteLine("=================================");
                MenuSelection();
                Write("Selection: ");
                userSelection = Convert.ToInt32(ReadLine());

            }
            WriteLine("You are now exiting the Program");
            WriteLine("Thank you for your Business!!!");
        }
        public static void AppLogin()
        {   // General Application Login style
            string userName = "mdiallo8", password = "progX21", userNameInput, userPassInput; //Username and Password must be written correctly for login
            WriteLine("\t==========================");
            WriteLine("\tWelcome to Centennial Bank");
            WriteLine("\t==========================");
            WriteLine("LOGIN");
            Write("User Name: ");
            userNameInput = Convert.ToString(ReadLine());
            Write("Password : ");
            userPassInput = Convert.ToString(ReadLine());
            while (userNameInput != userName || userPassInput != password) // Loop runs when Username and/or Password is incorrect
            {
                WriteLine("Wrong Password");
                Write("User Name: ");
                userNameInput = Convert.ToString(ReadLine());
                Write("Password : ");
                userPassInput = Convert.ToString(ReadLine());
            }
            WriteLine("Login Successfull");
        }
        public static void SavingPlansAndInterests(double balance, ref string saving, ref double newBal1,ref double deposit)
        {
            bool valid = false; //Bool created for loop output
            double Intrst = 0, bal = 0;
            double[,] MinBalInts = { {3000,6000,12000 },    //Minimum Balances row
                                    {0.012,0.016,0.023 }};  //Iterest Rates row
            string[] Savings = { "Simple Savings", "Advanced Savings", "Piece-of-Mind Savings" };
            for (int i = 0; i < Savings.Length ; i++)
            {
                WriteLine("{0}. {1} Plan. Must have a minimum balace of {2}",i+1,Savings[i],MinBalInts[0,i].ToString("c"));
            }
            Write("Select a Plan Number: ");
            int select = Convert.ToInt32(ReadLine());
            Write("How much would you like to deposit: $");
            deposit = Convert.ToInt32(ReadLine());
            for (int j = Savings.Length-1; j >= 0; --j)
            {
                if (deposit>=MinBalInts[0,j])
                {
                    valid = true; //This new bool value validates this if-statement
                    saving = Savings[j];
                    Intrst = MinBalInts[1, j];
                    bal = MinBalInts[0, j];
                }
            }
            if (valid) 
            {
                WriteLine("Congratulation! Your Savings balance is {0} " +
                        "with an annual interest rate of {1}", deposit.ToString("c"), Intrst.ToString("P2"));
                newBal1 = balance - deposit;
            }
            else
            {
                WriteLine("You must deposit an amount equal or more than {0}", bal.ToString("c"));
            }
        }
        public static int PinNumberChange(int pin)
        {
            int NewPin, NewPinConf;
            Write("Old PIN: ");
            pin = Convert.ToInt32(ReadLine());
            Write("New PIN: ");
            NewPin = Convert.ToInt32(ReadLine());
            Write("Confirm New PIN: ");
            NewPinConf = Convert.ToInt32(ReadLine());
            while (NewPin<100000||NewPinConf<100000)   
            {
                WriteLine("Invalid PIN combination");
                Write("Old PIN: ");
                pin = Convert.ToInt32(ReadLine());
                Write("New PIN: ");
                NewPin = Convert.ToInt32(ReadLine());
                Write("Confirm New PIN: ");
                NewPinConf = Convert.ToInt32(ReadLine());
            }
            if (NewPin==NewPinConf&&NewPin!=pin&&NewPinConf!=pin)
            {
                WriteLine("Your new PIN number is: {0}",NewPin);
            }
            return pin;
        }
        public static void CompanyStockSelection(double balance, ref double newBal, ref double stock, ref string company)
        {
            string[] Company = {"Royal Bank of Canada","Toronto-Dominion Bank","Bank of Nova-Scotioa",
                "Bank of Montreal","Shopify","Manulife Financial","Telus Corporation","Canadian National",
                "TC Energy","Rogers Communication"};
            double[,] PricePerStock = { { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } }; //Stock Price initiator for each random stock prices
            var StockPrice = new double[10];
            Random RandomStockPrice = new Random();
            WriteLine("Current Stocks");
            for (int i = 0; i < 10; i++) // Lists company names with respect to their random stock prices
            {
                StockPrice[i] = RandomStockPrice.NextDouble()*(200-0.1)+0.1;
                PricePerStock[1, i] = StockPrice[i];
                WriteLine("{0}- {1}: {2}",i+1,Company[i],StockPrice[i].ToString("c"));
            }
            Write("Pick a Company: ");
            int stockAmount;
            double TotalStockPrice;
            int userStock = Convert.ToInt32(ReadLine());
            for (int j = 0; j < 10; j++)
            {
                if (userStock==PricePerStock[0,j])
                {
                    Write("How many stocks would you like to buy from {0}: ",Company[j]);
                    stockAmount = Convert.ToInt32(ReadLine());
                    TotalStockPrice = stockAmount * StockPrice[j];
                    WriteLine("Total price of stock purchase: {0}",TotalStockPrice.ToString("c"));
                    WriteLine("Your new Deposit Account balance is: {0}",(balance-TotalStockPrice).ToString("c"));
                    stock = TotalStockPrice;
                    newBal = balance - TotalStockPrice;
                    company = Company[j];
                }
            }
            

        }
        public static void MenuSelection()
        {   //General Menu Selection
            WriteLine("Please choose from the following options:");
            WriteLine("1- Deposit");
            WriteLine("2- Invest in Savings Plan");
            WriteLine("3- Purchase Stocks");
            WriteLine("4- Change your PIN");
            WriteLine("5- Asset Overview");
            WriteLine("6- Exit Program");
            WriteLine("\n\n");
        }
    }
}
