using System;
using System.Collections.Generic;

namespace CreditSuise_Assessment
{
    /// <summary>
    /// Win32 entry point container
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Win32 entry point
        /// </summary>
        /// <param name="args">command line args</param>
        public static void Main(string[] args)
        {
            /* 
             * generate a few simulated orders. Product names need to align with Utilities.GetAllProducts() method
             * "iPhone 12","Samsung S10","Google Pixel 6"
             */
            var orders = new List<OrderComponent>();
            orders.Add(new OrderComponent() { Product = "iPhone 12", ThresholdPrice = 41.11m });
            orders.Add(new OrderComponent() { Product = "iPhone 12", ThresholdPrice = 50.72m });
            orders.Add(new OrderComponent() { Product = "iPhone 12", ThresholdPrice = 53.91m });
            orders.Add(new OrderComponent() { Product = "Samsung S10", ThresholdPrice = 49.01m });
            orders.Add(new OrderComponent() { Product = "Samsung S10", ThresholdPrice = 49.17m });
            orders.Add(new OrderComponent() { Product = "Google Pixel 6", ThresholdPrice = 71.99m });

            Utilities.LogStarting(orders); 

            // start processing orders
            var ordersProcessor = new OrdersProcessor();
            ordersProcessor.Process(orders);

            // execution finished
            Console.ReadLine();
        }
    }
}
