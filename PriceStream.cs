using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CreditSuise_Assessment
{
    /// <summary>
    /// Stream generator publishing per-request prices 
    /// </summary>
    public class PriceStream 
    {
        /// <summary>
        /// static instance of Random ensures random-ish values over time
        /// </summary>
        private static readonly Random random = new Random(); 

        /// <summary>
        /// List of all products that a price will be published for
        /// </summary>
        private readonly IList<string> allProducts;

        /// <summary>
        /// initializes a new instance of the <see cref="PriceStream"/> class
        /// </summary>
        public PriceStream()
        {
            this.allProducts = allProducts = Utilities.GetAllProducts();
        }

        /// <summary>
        /// emits prices
        /// </summary>
        /// <returns>Readonly dictionary of prices keyed by product name</returns>
        public IReadOnlyDictionary<string, decimal> Read()
        {
            return GetNextRandomPrices();
        }

        /// <summary>
        /// creates a dictionary of prices for all products
        /// </summary>
        /// <returns>ReadOnlyDictionary of prices keyed by product name</returns>
        private IReadOnlyDictionary<string, decimal> GetNextRandomPrices()
        {
            var productPrices = allProducts.ToDictionary(k => k, v => CreateRandomPrice());
            return new ReadOnlyDictionary<string, decimal>((IDictionary<string, decimal>)productPrices);
        }

        /// <summary>
        /// creates a random price between 1 and 500 with 2pd
        /// </summary>
        /// <returns>decimal price between 1 and 500 with 2pd</returns>
        private decimal CreateRandomPrice()
        {
            var nextRandom = random.Next(1, 50000);
            return new decimal(nextRandom) / 100;
        }
    }
}
