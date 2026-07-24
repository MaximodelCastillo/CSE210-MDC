using System;

class Program
{
    static void Main(string[] args)
    {
        // ----------------------------
        // Order 1 (USA)
        // ----------------------------

        Address address1 = new Address(
            "123 Main Street",
            "Phoenix",
            "AZ",
            "USA");

        Customer customer1 = new Customer(
            "John Smith",
            address1);

        Order order1 = new Order(customer1);

        order1.AddProduct(new Product("Laptop", "P1001", 900.00, 1));
        order1.AddProduct(new Product("Mouse", "P1002", 25.00, 2));
        order1.AddProduct(new Product("Keyboard", "P1003", 60.00, 1));

        // ----------------------------
        // Order 2 (Canada)
        // ----------------------------

        Address address2 = new Address(
            "456 King Street",
            "Toronto",
            "ON",
            "Canada");

        Customer customer2 = new Customer(
            "Maria Lopez",
            address2);

        Order order2 = new Order(customer2);

        order2.AddProduct(new Product("Camera", "P2001", 700.00, 1));
        order2.AddProduct(new Product("Tripod", "P2002", 120.00, 1));

        // ----------------------------
        // Display Order 1
        // ----------------------------

        Console.WriteLine("=================================");
        Console.WriteLine("ORDER 1");
        Console.WriteLine("=================================");

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine();

        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine();

        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}");

        // ----------------------------
        // Display Order 2
        // ----------------------------

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("ORDER 2");
        Console.WriteLine("=================================");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine();

        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine();

        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}");
    }
}
