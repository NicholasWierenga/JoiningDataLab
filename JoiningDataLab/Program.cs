namespace JoiningDataLab
{   
    public class Program
    {
        public static void Main()
        {
            CustomerOrder customerOrders = new CustomerOrder();
            customerOrders.Add("Apple", null, null, null);

            customerOrders.PrintAllHeader();
            Console.WriteLine();
            customerOrders.PrintAllShorter();
            Console.WriteLine();

            //Below is only using the extra part of the lab, not the assigned part.
            //CustomerOrderJoin join = new CustomerOrderJoin();
            //join.Add(4, "Julie's Morning Diner", "garh", 534, 2);
            //join.Add(7, "Julie's Morning Diner", "garh", 534, 2);
            //join.Add(3, "Julie's Morning Diner", null, null, null); // rows with no item, price, and quantity aren't printed, but are added.
            //join.Add(3, "Julie's Morning Diner", null, null, 2);
            //join.PrintAllJoinHeader(); // These prints let rows containing at least 1 item, price, or quantity that isn't null.
            //Console.WriteLine();
            //join.PrintAllJoinShorter();
        }
    }
}