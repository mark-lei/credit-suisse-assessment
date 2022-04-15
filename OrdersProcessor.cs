using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditSuise_Assessment
{
    /// <summary>
    /// orchestration class for retrieving prices and checking orders to purchase
    /// </summary>
    public class OrdersProcessor
    {
        /// <summary>
        /// PriceStream supplying prices for all products
        /// </summary>
        private readonly PriceStream priceStream = new PriceStream();

        /// <summary>
        /// checks and processes all supplied orders until a purchase has been made for
        ///  every product
        /// </summary>
        /// <param name="orders">List of orders to make puchases for</param>
        public void Process(IList<OrderComponent> orders)
        {
            bool finished = false;

            // create a dictionary of all orders grouped by product name
            var productOrders = CreateGroupedOrders(orders);

            // keep checking prices until every product has been purchased
            while (!finished)
            {
                try
                {
                    // get current prices
                    var currentPrices = priceStream.Read();

                    // check orders against current prices
                    ProcessCurrentPrices(productOrders, currentPrices);

                    // check whether we've finished; when all orders have shutdown
                    finished = orders.All(w => w.Shutdown);
                }
                catch (Exception ex)
                {
                    Utilities.LogError(ex);
                    // errors in individual components are handled in the OrderComponent. An error
                    // here implies the PriceStream has failed so stop further processing
                    finished = true;
                }
            }

            Utilities.LogFinished();
        }

        /// <summary>
        /// creates a dictionary of product orders keyed by product name from the
        ///  supplied List of orders
        /// </summary>
        /// <param name="orders">List of all orders</param>
        /// <returns>dictionary of product orders keyed by product name</returns>
        private IDictionary<string, List<OrderComponent>> CreateGroupedOrders(IList<OrderComponent> orders)
        {
            return orders.GroupBy(g => g.Product).ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// checks all prices against all orders
        /// </summary>
        /// <param name="orders">Dictionary of all orders keyed by product name</param>
        /// <param name="currentPrices">Dictionary of all prices keyed by product name</param>
        private void ProcessCurrentPrices(IDictionary<string, List<OrderComponent>> orders, IReadOnlyDictionary<string, decimal> currentPrices) 
        {
            Utilities.LogPrices(currentPrices);

            // get all orders for each product where a purchase hasn't been made and a price has been provided
            var activeProductOrders = orders.Where(w => w.Value.Any(o => !o.Shutdown) && currentPrices.ContainsKey(w.Key));
            
            foreach (var activeProductOrder in activeProductOrders)
            {
                decimal receivedPrice = currentPrices[activeProductOrder.Key];

                // sequentially check all orders for this product
                foreach (var order in activeProductOrder.Value)
                {
                    if (order.Shutdown)
                    {
                        // skip any order which has shutdown. This will be when an exception occured on the OrderComponent
                        continue;
                    }

                    if (order.CheckAndPurchase(receivedPrice))
                    {
                        // once any 1 order for a product has been completed then stop
                        //  processing that product, thus ensuring only one component instance makes the purchase
                        ShutdownAllOrdersForProduct(activeProductOrder);
                        break;
                    }
                }
            }
        }

        private void ShutdownAllOrdersForProduct(KeyValuePair<string, List<OrderComponent>> activeProductOrder)
        {
            activeProductOrder.Value.ForEach(f => f.Shutdown = true);
        }
    }
}