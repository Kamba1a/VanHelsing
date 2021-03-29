using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIShopService
    {
        private const float SELL_PRICE_PERCENT = 0.6f;


        public int CountSellPrice(BaseItem item)
        {
            return CountSellPrice(GetItemPrice(item));
        }

        public int GetItemPrice(BaseItem item)
        {
            return item.ItemStruct.ShopPrice;
        }

        private int CountSellPrice(int fullPrice)
        {
            return Mathf.RoundToInt(fullPrice * SELL_PRICE_PERCENT);
        }
    }
}
