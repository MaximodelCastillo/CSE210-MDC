using System;
using System.Collections.Generic;
using OnlineOrdering;

#pragma warning disable CA1050 // Declare types in namespaces
public class Order(Customer customer)
#pragma warning restore CA1050 // Declare types in namespaces
{
    private List<Product> _products = new List<Product>();
    private Customer _customer = customer;

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;

        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }

        if (_customer.LivesInUSA())
        {
            total += 5;
        }
        else
        {
            total += 35;
        }

        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label\n";

        foreach (Product product in _products)
        {
            label += $"{product.GetName()} - ID: {product.GetProductId()}\n";
        }

        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}
