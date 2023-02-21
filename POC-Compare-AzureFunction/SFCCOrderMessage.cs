using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunctionApp_POC
{


    public class OrderMessage
    {
        public string OrderNumber { get; set; }
        public Customer Customer { get; set; }
        public Item[] Items { get; set; }
        public decimal TotalPrice { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingMethod { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class Item
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
