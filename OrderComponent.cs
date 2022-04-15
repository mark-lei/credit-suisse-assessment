using System;

namespace CreditSuisse_Assessment
{
    /// <summary>
    /// Order processing component
    /// </summary>
    public class OrderComponent
    {
        /// <summary>
        /// product name associated with the order
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// threshold price below which an order is placed
        /// </summary>
        public decimal ThresholdPrice { get; set; }

        /// <summary>
        /// flag to indicate whether order processing is still active
        /// </summary>
        public bool Shutdown { get; set; }

        /// <summary>
        /// checks whether the offered price is below threshold and
        ///  attempts to order if so
        /// </summary>
        /// <param name="receivedPrice">price received to check</param>
        /// <returns>flag to indicate whether a purchase was made</returns> 
        public bool CheckAndPurchase(decimal receivedPrice)
        {
            if (receivedPrice < ThresholdPrice)
            {
                // try and purchase
                return TryPurchase(receivedPrice);
            }

            return false;
        }

        /// <summary>
        /// attempts to execute a purchase at the offered price
        /// </summary> 
        /// <param name="offeredPrice">purchse price</param>
        /// <returns>flag to indicate whether a purchase was made</returns> 
        private bool TryPurchase(decimal offeredPrice)
        {
            try
            {
                // regarless of the outcome below, success or error, we shutdown
                this.Shutdown = true;

                Utilities.LogPurchase(this.Product, offeredPrice);

                return true;

            } catch (Exception ex)
            {
                // if an error occured then assume the purchase was not made but we still shutdown
                Utilities.LogError(ex);                
                return false;
            }
        }
    }
}
