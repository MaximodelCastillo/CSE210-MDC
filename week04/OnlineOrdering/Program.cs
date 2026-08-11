using System;
using System.Globalization;

namespace OnlineOrdering
{
    /// <summary>
    /// Creates two orders (one USA customer, one international customer) and
    /// displays the packing label, shipping label, and total price of each.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // ---- Order 1: a customer in the USA ----
            Address usaAddress = new Address("123 Oak Street", "Springfield", "IL", "USA");
            Customer usaCustomer = new Customer("John Smith", usaAddress);

            Order order1 = new Order(usaCustomer);
            order1.AddProduct(new Product("Running Shoes", "SH-1001", 89.99, 1));
            order1.AddProduct(new Product("Water Bottle", "WB-2002", 12.50, 2));
            order1.AddProduct(new Product("Yoga Mat", "YM-3003", 24.99, 1));

            // ---- Order 2: an international customer ----
            Address intlAddress = new Address("45 Maple Avenue", "Toronto", "ON", "Canada");
            Customer intlCustomer = new Customer("Maria Gonzalez", intlAddress);

            Order order2 = new Order(intlCustomer);
            order2.AddProduct(new Product("Resistance Bands", "RB-4004", 15.00, 3));
            order2.AddProduct(new Product("Jump Rope", "JR-5005", 8.99, 2));

            DisplayOrder(order1, 1);
            Console.WriteLine();
            DisplayOrder(order2, 2);
        }

        static void DisplayOrder(Order order, int number)
        {
            Console.WriteLine($"========== ORDER {number} ==========");
            Console.WriteLine("Packing Label:");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine("Shipping Label:");
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine();
            string total = order.GetTotalCost().ToString("0.00", CultureInfo.InvariantCulture);
            Console.WriteLine($"Total Price: ${total}");
        }
    }
}
