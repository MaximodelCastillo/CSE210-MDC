using System;

namespace OnlineOrdering
{
    /// <summary>
    /// A customer: a name and an Address object. The USA check is delegated
    /// to the address.
    /// </summary>
    class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        // Read-only accessors needed for the shipping label.
        public string Name => _name;
        public Address Address => _address;

        public bool IsInUSA() => _address.IsInUSA();
    }
}
