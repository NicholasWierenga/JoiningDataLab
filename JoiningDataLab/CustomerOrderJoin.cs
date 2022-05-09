using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoiningDataLab
{
    class CustomerOrderJoin
    {
        List<Customer> customer;
        List<Order> orders;

        public CustomerOrderJoin()
        {
            customer = new List<Customer>()
            {
                new Customer() {CustomerID = 1, CustomerName = "Acme Hardware"},
                new Customer() {CustomerID = 2, CustomerName = "Falls Realty"},
                new Customer() {CustomerID = 3, CustomerName = "Julie's Morning Diner"},
                new Customer() {CustomerID = 6, CustomerName = "Joe’s Chicago Pizza"},
                new Customer() {CustomerID = 4, CustomerName = "Boeing"},
                new Customer() {CustomerID = 5, CustomerName = "Apple"},
            };
            orders = new List<Order>()
            {
                new Order() {CustomerID = 1, OrderID = 1, Item = "Mouse", Price = 25m, Quantity = 3},
                new Order() {CustomerID = 1, OrderID = 2, Item = "Keyboard", Price = 45m, Quantity = 2},
                new Order() {CustomerID = 2, OrderID = 3, Item = "MacBook", Price = 800m, Quantity = 2},
                new Order() {CustomerID = 3, OrderID = 4, Item = "iPad", Price = 525m, Quantity = 1},
                new Order() {CustomerID = 3, OrderID = 5, Item = "Credit Card Reader", Price = 45m, Quantity = 1},
                new Order() {CustomerID = 4, OrderID = 6, Item = "Fire Extinguisher", Price = 85m, Quantity = 1},
                new Order() {CustomerID = 5, OrderID = 7, Item = null, Price = null, Quantity = null},
            };
            SortList(); // customer isn't alphabetical by name, this fixes that.
        }

        public void Add(int CustomerID, string CustomerName, string? Item, decimal? Price, int? Quantity)
        {   // Used to add an order then sorts it by name. It puts the data into correct lists or tells user what is wrong.

            int OrderID = orders.Max(order => order.OrderID) + 1; // Keeps OrderID unique.
            
            if (customer.Exists(cust => cust.CustomerID == CustomerID && cust.CustomerName != CustomerName))
            { // Checks if they put in a customerID that exists, but has wrong customername
                string correctName = customer.First(cust => cust.CustomerID == CustomerID).CustomerName;
                Console.WriteLine("You entered a valid customer ID, but wrong customer name. " + CustomerID + " corresponds to " + correctName + ".");
                Console.WriteLine("No addition made, please try again.");
                return;
            }
            else if (customer.Exists(cust => cust.CustomerName == CustomerName && cust.CustomerID != CustomerID))
            { // Checks if they put in a customername that exists, but has wrong customerID
                int correctID = customer.First(cust => cust.CustomerName == CustomerName).CustomerID;
                Console.WriteLine("You entered a valid customer name, but wrong customer ID. " + CustomerName + " corresponds to " + correctID + ".");
                Console.WriteLine("No addition made, please try again.");
                return;
            }
            else if (!customer.Exists(cust => cust.CustomerName == CustomerName && cust.CustomerID == CustomerID))
            { // Checks if we are dealing with a new customer name with a new customer ID too, so we add it to customers.
                customer.Add(new Customer() { CustomerName = CustomerName, CustomerID = CustomerID });
                orders.Add(new Order() { CustomerID = CustomerID, OrderID = OrderID, Item = Item, Price = Price, Quantity = Quantity});
            }
            else // The last combination of customer name and customer ID is where both exist and correspond to eachother, so we don't need to change customers.
            {
                orders.Add(new Order() { CustomerID = CustomerID, OrderID = OrderID, Item = Item, Price = Price, Quantity = Quantity });
            }

            SortList(); // We call this again because we might've mixed up customer again, which would mix up the print method results.
        }

        public void SortList()
        {
            customer = customer.OrderBy(x => x.CustomerName).ToList();
        }

        public void PrintAllJoinHeader()
        {
            string custName = "";
            int padLength = 25;
            decimal? totalCost = 0;
            List<Order> custOrders = new List<Order>();

            for (int i = 0; i < customer.Count; i++)
            {
                Console.WriteLine(customer[i].CustomerName);

                custOrders = orders.Where(order => order.CustomerID == customer[i].CustomerID).ToList(); // Gets list of orders corresponding to CustomerID

                if (!custOrders.Any(order => order.Item != null || order.Price != null || order.Quantity != null))
                {   // Checks if custOrders has any non-null orders in it.
                    Console.WriteLine("\t*** NO ORDERS ***");
                }
                else
                {
                    Console.WriteLine("\t" + "Item".PadRight(padLength) + "Price".PadRight(padLength) + "Quantity".PadRight(padLength) + "Total");

                    for (int j = 0; j < custOrders.Count; j++)
                    {
                        if (custOrders[j].Item == null && custOrders[j].Price == null && custOrders[j].Quantity == null)
                        {
                            continue; // Skips lines that are entirely null.
                        }

                        totalCost += (custOrders[j].Quantity ?? 0) * (custOrders[j].Price ?? 0);

                        Console.WriteLine("\t" + ((custOrders[j].Item ?? "Not Given").PadRight(padLength))
                            + ((custOrders[j].Price ?? 0).ToString().PadRight(padLength))
                            + ((custOrders[j].Quantity ?? 0).ToString().PadRight(padLength))
                            + ((custOrders[j].Quantity * custOrders[j].Price) ?? 0));
                    }

                    Console.WriteLine("\t" + "Total".PadRight(3 * padLength) + totalCost);
                    totalCost = 0;
                }
            }
        }

        public void PrintAllJoinShorter()
        {
            string custName = "";
            int padLength = 25;
            List<Order> custOrders = new List<Order>();

            Console.WriteLine("Customer".PadRight(padLength) + "Item".PadRight(padLength) + "Price".PadRight(padLength) + "Quantity".PadRight(padLength) + "Total");

            for (int i = 0; i < customer.Count; i++)
            {
                Console.Write(customer[i].CustomerName.PadRight(padLength));

                custOrders = orders.Where(order => order.CustomerID == customer[i].CustomerID).ToList(); // Gets list of orders corresponding to CustomerID


                if (!custOrders.Any(order => order.Item != null || order.Price != null || order.Quantity != null))
                {   // Checks if custOrders has any non-null orders in it.
                    Console.WriteLine("*** NO ORDERS ***");
                }
                else
                {
                    for (int j = 0; j < custOrders.Count; j++) // We run through all orders corresponding to our customerID and print them out.
                    {
                        if (custOrders[j].Item == null && custOrders[j].Price == null && custOrders[j].Quantity == null)
                        {
                            continue; // Skips lines that are entirely null.
                        }
                        if (j > 0) // To pad orders after the first one, which was right padded already.
                        {
                            Console.Write("".PadRight(padLength));
                        }
                        
                        Console.WriteLine(((custOrders[j].Item ?? "Not Given").PadRight(padLength))
                            + ((custOrders[j].Price ?? 0).ToString().PadRight(padLength))
                            + ((custOrders[j].Quantity ?? 0).ToString().PadRight(padLength))
                            + ((custOrders[j].Quantity * custOrders[j].Price) ?? 0));
                    }
                }
            }
        }
    }
}
