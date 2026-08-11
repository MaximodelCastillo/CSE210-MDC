using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    /// <summary>
    /// An order: a customer and a list of products. Can total the order and
    /// produce packing and shipping labels.
    /// </summary>
    class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product) => _products.Add(product);

        // Sum of all product totals plus one-time shipping: $5 in the USA,
        // $35 everywhere else.
        public double GetTotalCost()
        {
            double total = 0;
            foreach (Product product in _products)
            {
                total += product.GetTotalCost();
            }
            total += _customer.IsInUSA() ? 5 : 35;
            return total;
        }

        // One line per product: name and product id.
        public string GetPackingLabel()
        {
            List<string> lines = new List<string>();
            foreach (Product product in _products)
            {
                lines.Add($"{product.Name} (ID: {product.ProductId})");
            }
            return string.Join("\n", lines);
        }

        // Customer name followed by the full address.
        public string GetShippingLabel()
        {
            return $"{_customer.Name}\n{_customer.Address.GetFullAddress()}";
        }
    }
}
