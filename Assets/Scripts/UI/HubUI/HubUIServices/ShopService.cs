﻿using System.Text;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class ShopService
    {
        #region Constants

        private const float SELL_PRICE_PERCENT = 0.6f;

        #endregion


        private HubUIContext _context;


        public ShopService(HubUIContext context)
        {
            _context = context;
        }


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

        public bool IsPossibleToBuyShopItem(BaseItemModel item, CityModel city)
        {
            if (item != null)
            {
                return
                    item.RequiredReputationForSaleInShop <= city.PlayerReputation &&
                    _context.Player.GoldAmount >= GetItemPrice(item);
            }
            else
            {
                return true;
            }
        }

        public bool IsPossibleToBuyShopItem(BaseItemModel item, CityModel city, out string message)
        {
            bool flag = true;
            StringBuilder sb = new StringBuilder();

            if (item != null)
            {
                if (city.PlayerReputation < item.RequiredReputationForSaleInShop)
                {
                    flag = false;

                    sb.AppendFormat("Недостаточно репутации для покупки");
                    sb.AppendLine();
                    sb.AppendFormat($"Необходимая репутация: {item.RequiredReputationForSaleInShop}");
                }
                if (_context.Player.GoldAmount < GetItemPrice(item))
                {
                    flag = false;

                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }
                    sb.AppendFormat("Недостаточно денег");
                }
            }

            message = sb.ToString();
            return flag;
        }

        private void SellItem(CityModel city, int inventorySlotIndex)
        {
            ItemStorage playerInventory = _context.Player.Inventory;
            BaseItemModel sellingItem = playerInventory.GetItemBySlot(inventorySlotIndex);

            if (sellingItem != null)
            {
                if (city.BuyBackStorage.PutItemToFirstEmptySlot(sellingItem))
                {
                    playerInventory.RemoveItem(inventorySlotIndex);
                    _context.Player.GoldAmount += CountSellPrice(sellingItem);
                }
                else
                {
                    Debug.Log("BuyBack storage is full");
                }
            }
        }

        #endregion
    }
}
