using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Policy;
using DotLiquid;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Linq;

namespace FunctionApp_POC
{
    public static class Function1
    {
        [FunctionName("Function1")]


        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");



            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            OrderMessage data = JsonConvert.DeserializeObject<OrderMessage>(requestBody);


            // build the Liquid template
            var template = new StringBuilder();
            template.AppendLine("{");
            template.AppendLine("  \"orderId\": \"" + data.OrderNumber + "\",");
            template.AppendLine("  \"customer\": {");
            template.AppendLine("    \"firstName\": \"" + data.Customer.FirstName + "\",");
            template.AppendLine("    \"lastName\": \"" + data.Customer.LastName + "\",");
            template.AppendLine("    \"email\": \"" + data.Customer.Email + "\",");
            template.AppendLine("    \"phoneNumber\": \"" + data.Customer.PhoneNumber + "\"");
            template.AppendLine("  },");
            template.AppendLine("  \"items\": [");
            foreach (dynamic item in data.Items)
            {
                template.AppendLine("    {");
                template.AppendLine("      \"productId\": \"" + item.ProductId + "\",");
                template.AppendLine("      \"productName\": \"" + item.ProductName + "\",");
                template.AppendLine("      \"quantity\": " + item.Quantity + ",");
                template.AppendLine("      \"price\": " + item.Price);
                template.Append("    }");
                if (item != data.Items.Last())
                {
                    template.Append(",");
                }
                template.AppendLine();
            }
            template.AppendLine("  ],");
            template.AppendLine("  \"totalPrice\": " + data.TotalPrice + ",");
            template.AppendLine("  \"shippingAddress\": {");
            template.AppendLine("    \"addressLine1\": \"" + data.ShippingAddress.AddressLine1 + "\",");
            template.AppendLine("    \"addressLine2\": \"" + data.ShippingAddress.AddressLine2 + "\",");
            template.AppendLine("    \"city\": \"" + data.ShippingAddress.City + "\",");
            template.AppendLine("    \"state\": \"" + data.ShippingAddress.State + "\",");
            template.AppendLine("    \"postalCode\": \"" + data.ShippingAddress.PostalCode + "\",");
            template.AppendLine("    \"country\": \"" + data.ShippingAddress.Country + "\"");
            template.AppendLine("  },");
            template.AppendLine("  \"billingAddress\": {");
            template.AppendLine("    \"addressLine1\": \"" + data.BillingAddress.AddressLine1 + "\",");
            template.AppendLine("    \"addressLine2\": \"" + data.BillingAddress.AddressLine2 + "\",");
            template.AppendLine("    \"city\": \"" + data.BillingAddress.City + "\",");
            template.AppendLine("    \"state\": \"" + data.BillingAddress.State + "\",");
            template.AppendLine("    \"postalCode\": \"" + data.BillingAddress.PostalCode + "\",");
            template.AppendLine("    \"country\": \"" + data.BillingAddress.Country + "\"");
            template.AppendLine("  },");
            template.AppendLine("  \"paymentMethod\": \"" + data.PaymentMethod + "\",");
            template.AppendLine("  \"shippingMethod\": \"" + data.ShippingMethod + "\",");
            template.AppendLine("  \"orderDate\": \"" + data.OrderDate + "\",");
            template.AppendLine("  \"status\": \"New\"");
            template.AppendLine("}");

            var liquidTemplate = template.ToString();




            return new OkObjectResult(liquidTemplate);
        }
    }
}
