using UnityEngine;


namespace BeastHunterHubUI
{
    public class ShopService
    {
        #region Constants

        private const float SELL_PRICE_PERCENT = 0.6f;

        #endregion


        #region Methods

        public int CountSellPrice(BaseItemModel item)
        {
            return CountSellPrice(GetItemPrice(item));
        }

        public int GetItemPrice(BaseItemModel item)
        {
            return item.ShopPrice;
        }

        private int CountSellPrice(int fullPrice)
        {
            return Mathf.RoundToInt(fullPrice * SELL_PRICE_PERCENT);
        }

        #endregion
    }
}
