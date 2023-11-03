using DBFirst.Data;
using DBFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DBFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthContext())
            {
                Console.WriteLine("Welcome to the NorthWind Database");

                while (true)
                {
                    Console.WriteLine("1. View Customers");
                    Console.WriteLine("2. Add Customer");
                    
                    int answer = int.Parse(Console.ReadLine());

                    IQueryable<Customer> customers = null;

                    if (answer == 1)
                    {
                        Console.WriteLine("Do you want to view ASC or DESC?");
                        string userView = Console.ReadLine();

                        if (userView == "ASC")
                        {
                            customers = context.Customers
                                .Include(c => c.Orders)
                                .OrderBy(c => c.CompanyName);
                        }
                        else if (userView == "DESC")
                        {
                            customers = context.Customers
                                .Include(c => c.Orders)
                                .OrderByDescending(c => c.CompanyName.ToLower());
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice for sorting.");
                            continue; 
                        }

                        var customerList = customers.ToList();

                        for (var i = 0; i < customerList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {customerList[i].CompanyName}\n\n" +
                                $"COUNTRY/REGION: {customerList[i].Country}/{customerList[i].Region}\n" +
                                $"PHONE NUMBER: {customerList[i].Phone}\n" +
                                $"AMOUNT OF ORDERS: {customerList[i].Orders.Count()}");
                            Console.WriteLine("--------------------\n");
                        }

                        Console.Write("Select customer number to view more info: ");
                        int userChoice = int.Parse(Console.ReadLine());

                        if (userChoice >= 1 && userChoice <= customers.Count())
                        {
                            var selectedCustomer = customers.ToList().ElementAt(userChoice - 1);

                            Console.Clear();
                            Console.WriteLine($"{userChoice}. {selectedCustomer.CompanyName}\n\n" +
                                $"ADDRESS: {selectedCustomer.Address} -  {selectedCustomer.PostalCode} {selectedCustomer.City}\n" +
                                $"COUNTRY/REGION: {selectedCustomer.Country} / {selectedCustomer.Region}\n" +
                                $"CONTACT PERSON: {selectedCustomer.ContactName} - {selectedCustomer.ContactTitle}\n" +
                                $"PHONE/FAX: {selectedCustomer.Phone} / {selectedCustomer.Fax}\n" +
                                $"AMOUNT OF ORDERS: {selectedCustomer.Orders.Count()}");

                            Console.WriteLine();
                            foreach (var order in selectedCustomer.Orders)
                            {
                                Console.WriteLine($"ORDER DATE: {order.OrderDate}\n" +  
                                    $"SHIP NAME: {order.ShipName}\n" +
                                    $"SHIP ADRESS: {order.ShipAddress}");
                                Console.WriteLine();
                            }
                                
                            
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid customer number. Please enter a valid number.");
                        }

                    }
                    else if (answer == 2)
                    
                        {
                            Customer newCustomer = new Customer();

                            Console.Write("Enter Company Name: ");
                            newCustomer.CompanyName = Console.ReadLine();

                            Console.Write("Enter Contact Name: ");
                            newCustomer.ContactName = Console.ReadLine();

                            Console.Write("Enter Contact Title: ");
                            newCustomer.ContactTitle = Console.ReadLine();

                            Console.Write("Enter Address: ");
                            newCustomer.Address = Console.ReadLine();

                            Console.Write("Enter City: ");
                            newCustomer.City = Console.ReadLine();

                            Console.Write("Enter Region: ");
                            newCustomer.Region = Console.ReadLine();

                            Console.Write("Enter Postal Code: ");
                            newCustomer.PostalCode = Console.ReadLine();

                            Console.Write("Enter Country: ");
                            newCustomer.Country = Console.ReadLine();

                            Console.Write("Enter Phone: ");
                            newCustomer.Phone = Console.ReadLine();

                            Console.Write("Enter Fax: ");
                            newCustomer.Fax = Console.ReadLine();

                            
                            Random rand = new Random();
                            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                            newCustomer.CustomerId = new string(Enumerable.Repeat(chars, 5)
                                .Select(s => s[rand.Next(s.Length)]).ToArray());

                            
                            context.Customers.Add(newCustomer);
                            context.SaveChanges();

                            Console.WriteLine("Customer added successfully!");
                        }

                    }

                }
            }
        }
    }



