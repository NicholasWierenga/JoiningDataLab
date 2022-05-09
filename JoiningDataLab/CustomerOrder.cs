using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoiningDataLab
{
    public class CustomerOrder
    {
        public string CustomerName { get; set; }
        public string? Item { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public List<CustomerOrder> orderTable { get; set; }

        public void Add(string CustomerName, string? Item, decimal? Price, int? Quantity) // Used to add an order then sorts it by name.
        {
            if (orderTable == null) // We use this to make sure we have a table to look at.
            {
                orderTable = getOrders();
            }

            orderTable.Add(new CustomerOrder() { CustomerName = CustomerName, Item = Item, Price = Price, Quantity = Quantity });

            orderTable = orderTable.OrderBy(x => x.CustomerName).ToList();
        }

        public List<CustomerOrder> getOrders() // Populates list with initial orders
        {
            List<CustomerOrder> orderTable = new List<CustomerOrder>()
            {
                new CustomerOrder() { CustomerName = "Acme Hardware", Item = "Mouse", Price = 25m, Quantity = 3 },
                new CustomerOrder() { CustomerName = "Joe’s Chicago Pizza", Item = null, Price = null, Quantity = null },
                new CustomerOrder() { CustomerName = "Acme Hardware", Item = "Keyboard", Price = 45m, Quantity = 2 },
                new CustomerOrder() { CustomerName = "Falls Realty", Item = "MacBook", Price = 800m, Quantity = 2 },
                new CustomerOrder() { CustomerName = "Julie's Morning Diner", Item = "iPad", Price = 525m, Quantity = 1 },
                new CustomerOrder() { CustomerName = "Julie's Morning Diner", Item = "Credit Card Reader", Price = 45m, Quantity = 1 },
            };

            return orderTable.OrderBy(x => x.CustomerName).ToList();
        }

        public void PrintAllHeader()
        {
            string custName = "";
            int padLength = 25;
            decimal? totalCost = 0;

            if (orderTable == null) // We use this to make sure we have a table to look at.
            {
                orderTable = getOrders();
            }

            for (int i = 0; i < orderTable.Count; i++)
            {
                if (custName != orderTable[i].CustomerName) // This is to check if after i increments the customer name is changed. If so, we print the name.
                {
                    custName = orderTable[i].CustomerName;
                    Console.WriteLine(custName);
                    if (orderTable[i].Item != null) // Checks if we are looking at a null row and prints out header if not.
                    {
                        Console.WriteLine("\t" + "Item".PadRight(padLength) + "Price".PadRight(padLength) + "Quantity".PadRight(padLength) + "Total");
                    }
                }
                if (orderTable[i].Item == null)
                {
                    Console.WriteLine("\t***NO ORDERS***");
                }
                else
                {
                    totalCost += orderTable[i].Quantity * orderTable[i].Price;

                    Console.WriteLine("\t" + orderTable[i].Item.PadRight(padLength) + orderTable[i].Price.ToString().PadRight(padLength)
                        + orderTable[i].Quantity.ToString().PadRight(padLength) + (orderTable[i].Quantity * orderTable[i].Price));

                    if (i + 1 == orderTable.Count)
                    {
                        Console.WriteLine("\t" + "Total".PadRight(3 * padLength) + totalCost);
                    }
                    else if (custName != orderTable[i + 1].CustomerName)
                    {
                        Console.WriteLine("\t" + "Total".PadRight(3 * padLength) + totalCost);
                        totalCost = 0;
                    }
                }
            }
        }

        public void PrintAllShorter()
        {
            string custName = "";
            int padLength = 25;

            if (orderTable == null)
            {
                orderTable = getOrders();
            }

            Console.WriteLine("Customer".PadRight(padLength) + "Item".PadRight(padLength) + "Price".PadRight(padLength) + "Quantity".PadRight(padLength) + "Total");

            for (int i = 0; i < orderTable.Count; i++)
            {
                if (custName != orderTable[i].CustomerName)
                {
                    custName = orderTable[i].CustomerName;
                    
                    Console.Write(custName.PadRight(padLength));
                }
                else
                {
                    Console.Write("".PadRight(padLength));
                }

                if (orderTable[i].Item == null)
                {
                    Console.WriteLine("*** NO ORDERS ***");
                }
                else
                {
                    Console.WriteLine(orderTable[i].Item.PadRight(padLength) + orderTable[i].Price.ToString().PadRight(padLength)
                                        + orderTable[i].Quantity.ToString().PadRight(padLength) + (orderTable[i].Quantity * orderTable[i].Price));
                }
            }
        }
    }
}
