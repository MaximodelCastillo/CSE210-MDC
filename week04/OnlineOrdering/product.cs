using System;

namespace OnlineOrdering
{
    /// <summary>
    /// A product line item: a name, product id, price per unit, and quantity.
    /// </summary>
    class Product
    {
        private string _name;
        private string _productId;
        private double _price;      // price per unit
        private int _quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            _name = name;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        // Read-only accessors needed for the packing label.
        public string Name => _name;
        public string ProductId => _productId;

        // Total cost of this product = price per unit * quantity.
        public double GetTotalCost() => _price * _quantity;
    }
}
