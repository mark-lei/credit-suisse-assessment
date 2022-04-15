using System;
using System.Collections.Generic;
using System.Text;

namespace CreditSuise_Assessment
{
    /// <summary>
    /// Utility methods for source data creation and console logging
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// generates a list of all products
        /// </summary>
        /// <returns>List of strings of all products</returns>
        public static IList<string> GetAllProducts()
        {
            return new List<string>()
            {
                "iPhone 12",
                "Samsung S10",
                "Google Pixel 6"
            };
        }

        /// <summary>
        /// writes the dictionary of all supplied prices to the console
        /// </summary>
        /// <param name="currentPrices">dictionary of prices</param>
        public static void LogPrices(IReadOnlyDictionary<string, decimal> currentPrices)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Latest prices;"); 
            foreach (var currentPrice in currentPrices)
            {
                sb.Append(String.Format("{0,-15}: {1,-10:F2}", currentPrice.Key, currentPrice.Value));
            }

            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// writes the execution-starting message to the console
        /// </summary>
        public static void LogStarting(List<OrderComponent> orders)
        {
            var sb = new StringBuilder();
            sb.AppendLine("#############################");
            sb.AppendLine(" Starting with target order prices;");
            foreach (var order in orders)
            {
                sb.AppendLine(String.Format(" -  {0,-15}: {1,-10:F2}", order.Product, order.ThresholdPrice));
            }
            sb.AppendLine("#############################");

            Console.Write(sb.ToString());
        }

        /// <summary>
        /// writes the execution-finished message to the console
        /// </summary>
        public static void LogFinished()
        {
            Console.WriteLine("#############################");
            Console.WriteLine(" FINISHED");
            Console.WriteLine("#############################");
        }

        /// <summary>
        /// writes the product-purchaed message to the console
        /// </summary>
        /// <param name="product">Product purchsed</param>
        /// <param name="price">purchse price</param> 
        public static void LogPurchase(string product, decimal price)
        {
            Console.WriteLine(String.Format("{0} purchased at {1,-10:F2}",product, price));
        }

        /// <summary>
        /// writes the an error message to the console
        /// </summary>
        /// <param name="ex">exception to log</param>
        public static void LogError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
