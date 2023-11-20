//Stina Hedman
//NET23

using DBFirst.Data;
using DBFirst.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace DBFirst
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string input;
            int choise;
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("1: Print Companys");
                Console.WriteLine("2: Add Customer");

                input = Console.ReadLine();
                //whileloop checking input can be parsed into int between 1-2
                while (!Int32.TryParse(input, out choise) || choise < 1 || choise > 2)
                {
                    Console.WriteLine("input a number between 1-2");
                    input = Console.ReadLine();
                }

                //switch case moving user to their chosen option in menu.
                switch (choise)
                {
                    case 1:

                        using (var context = new NorthContext())
                        {
                            //creates a list of all the customers in the database
                            var customers = from c in context.Customers
                                            select c;
                            var customerList = new List<Customer>();
                            foreach (var customer in customers)
                            {
                                customerList.Add(customer);
                            }

                            //Sorts list of customers by CompanyName
                            List<Customer> SortedList = customerList.OrderBy(o => o.CompanyName).ToList();

                            Console.WriteLine("Print list in ascending or descending order?");
                            Console.WriteLine("1. Decending");
                            Console.WriteLine("2. Ascending");
                            Console.WriteLine("Input 1 or 2:");

                            input = Console.ReadLine();
                            //while loop checking input can be parsed into int between 1-2
                            while (!Int32.TryParse(input, out choise) || choise < 1 || choise > 2)
                            {
                                Console.WriteLine("input a number between 1-2");
                                input = Console.ReadLine();
                            }
                            if (choise == 1)
                            {
                                //Prints descending list of companys
                                SortedList.Reverse();
                                for (int i = 0; i < SortedList.Count(); i++)
                                {
                                    Console.WriteLine($"nr: {i}.");
                                    Console.WriteLine("Company name : " + SortedList[i].CompanyName);
                                    Console.WriteLine("Country : " + SortedList[i].Country);
                                    Console.WriteLine("Region : " + SortedList[i].Region);
                                    Console.WriteLine("Phone number: " + SortedList[i].Phone);
                                    Console.WriteLine("Active Orders: " + SortedList[i].Orders.Count);
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                //Prints ascending list of companys
                                for (int i = 0; i < SortedList.Count(); i++)
                                {
                                    Console.WriteLine($"nr: {i}.");
                                    Console.WriteLine("Company name : " + SortedList[i].CompanyName);
                                    Console.WriteLine("Country : " + SortedList[i].Country);
                                    Console.WriteLine("Region : " + SortedList[i].Region);
                                    Console.WriteLine("Phone number: " + SortedList[i].Phone);
                                    Console.WriteLine("Active Orders: " + SortedList[i].Orders.Count);
                                    Console.WriteLine();
                                }

                            }
                            
                            bool moreInfo = true;

                            //whileloop for while you want more info about the company
                            while (moreInfo)
                            {
                                Console.WriteLine("Do you want more information about a companyorder? y/n?");


                                //whileloop checking input is valid
                                bool checkInput = true;
                                input = Console.ReadLine();
                                Char inp;

                                while (checkInput)
                                {
                                    inp = input.First();
                                    if (inp == 'y' || inp == 'Y')
                                    {
                                        Console.WriteLine("it worked!!");
                                        Console.WriteLine("Enter the nr above customer to print more information. Else press enter to return.");
                                        input = Console.ReadLine();
                                        while (!Int32.TryParse(input, out choise) || choise < 0 || choise > SortedList.Count() - 1)
                                        {
                                            Console.WriteLine("Press Enter to return to main menu, or write the number presented above each customer to print additional information.");
                                            input = Console.ReadLine();

                                        }
                                        if (input.IsNullOrEmpty())
                                        {
                                            moreInfo = false;
                                            break;
                                        }
                                        else
                                        {
                                            int index = Int32.Parse(input);
                                            Console.WriteLine($"Company name : {SortedList[index].CompanyName}");
                                            Console.WriteLine($"Contact name : {SortedList[index].ContactName}");
                                            Console.WriteLine($"Contact title : {SortedList[index].ContactTitle}");
                                            Console.WriteLine($"Adress : {SortedList[index].Address}");
                                            Console.WriteLine($"City : {SortedList[index].City}");
                                            Console.WriteLine($"Region : {SortedList[index].Region}");
                                            Console.WriteLine($"Postal code : {SortedList[index].PostalCode}");
                                            Console.WriteLine($"Country : {SortedList[index].Country}");
                                            Console.WriteLine($"Phone : {SortedList[index].Phone}");
                                            Console.WriteLine($"Fax : {SortedList[index].Fax}");
                                            Console.WriteLine();
                                            Console.WriteLine("Orders : ");
                                            Console.WriteLine();

                                            using (var nContext = new NorthContext())
                                            {
                                                var orders = from o in nContext.Orders
                                                             select o;

                                                int count = 1;
                                                foreach (Order o in orders)
                                                {
                                                    //går igenom orderlistan och kollar om det finns ordrar som matchar kundID, finns det smidigare sätt?
                                                    if (SortedList[index].CustomerId == o.CustomerId)
                                                    {
                                                        //skriv ut
                                                        Console.WriteLine($"Order {count} :");
                                                        count++;
                                                        Console.WriteLine($"Order ID: {o.OrderId}");
                                                        Console.WriteLine($"Customer ID: {o.CustomerId}");
                                                        Console.WriteLine($"Employee ID: {o.EmployeeId}");
                                                        Console.WriteLine($"Order Date: {o.OrderDate}");
                                                        Console.WriteLine($"Required Date: {o.RequiredDate}");
                                                        Console.WriteLine($"Shipped Date: {o.ShippedDate}");
                                                        Console.WriteLine($"Ship Via: {o.ShipVia}");
                                                        Console.WriteLine($"Freight: {o.Freight}");
                                                        Console.WriteLine($"Ship Name: {o.ShipName}");
                                                        Console.WriteLine($"Ship Adress: {o.ShipAddress}");
                                                        Console.WriteLine($"Ship City: {o.ShipCity}");
                                                        Console.WriteLine($"Ship Region: {o.ShipRegion}");
                                                        Console.WriteLine($"Ship Postal Code: {o.ShipPostalCode}");
                                                        Console.WriteLine($"Ship Country: {o.ShipCountry}");
                                                        Console.WriteLine($"Customer: {o.Customer}");
                                                        Console.WriteLine($"Employee: {o.Employee}");
                                                        Console.WriteLine();
                                                    }
                                                }
                                            }
                                        }
                                        Console.ReadLine();
                                    }
                                    else if (inp != 'y' || inp != 'Y' || inp != 'n' || inp != 'N')
                                    {
                                        Console.WriteLine("Enter 'y' or 'n'.");
                                        input = Console.ReadLine();
                                    }
                                    else
                                    {
                                        checkInput = false;
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 2:
                        {
                        using (NorthContext context = new NorthContext())
                            {
                                //Info to enter about new customer
                                Console.WriteLine("Enter full name of customer : ");
                                string newName = Console.ReadLine();

                                Console.WriteLine("Customer Contact name : ");
                                string newContactName = Console.ReadLine();

                                Console.WriteLine("Customer Contact title : ");
                                string newContactTitle = Console.ReadLine();

                                Console.WriteLine("Customer Address : ");
                                string newAddress = Console.ReadLine();

                                Console.WriteLine("Customer City : ");
                                string newCity = Console.ReadLine();

                                Console.WriteLine("Customer Region : ");
                                string newRegion = Console.ReadLine();

                                Console.WriteLine("Customer PostalCode : ");
                                string newPostalCode = Console.ReadLine();

                                Console.WriteLine("Customer Country : ");
                                string newCountry = Console.ReadLine();

                                Console.WriteLine("Customer Phonenumber : ");
                                string newPhone = Console.ReadLine();

                                Console.WriteLine("Customer Fax: ");
                                string newFax = Console.ReadLine();
    

                                //creates a new customer object
                                Random r = new Random();
                                Customer newCustomer = new Customer()
                                {
                                    CompanyName = newName,
                                    ContactName = newContactName,
                                    ContactTitle = newContactTitle,
                                    Address = newAddress,
                                    City = newCity,
                                    PostalCode = newPostalCode,
                                    Region = newRegion,
                                    Country = newCountry,
                                    Phone = newPhone,
                                    Fax = newFax,

                                    CustomerId = r.Next(10000, 99999).ToString(),
                                };

                                //adds new customer to database and saves changes.
                                context.Customers.Add(newCustomer);
                                context.SaveChanges();
                                Console.WriteLine("Added new costumer.");
                            }
                        }
                        break;
                }

            }
        }
    }
}
