using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUICityModel
    {
        private int _minItemsAmountInShop;
        private int _playerReputation;


        public Action<HubMapUICityModel> OnChangePlayerReputationHandler { get; set; }


        public int DataInstanceID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public HubMapUIFractionData Fraction { get; private set; } 
        public List<HubMapUICitizenModel> Citizens { get; private set; }
        public List<BaseItem> ShopItemsPool { get; private set; }
        public HubMapUIStorage ShopStorage { get; private set; }
        public HubMapUIStorage BuyBackStorage { get; private set; }

        public int PlayerReputation
        {
            get
            {
                return _playerReputation;
            }
            set
            {
                if (value != _playerReputation)
                {
                    _playerReputation = value;
                    OnChangePlayerReputationHandler?.Invoke(this);
                }
            }
        }


        public HubMapUICityModel(HubMapUICityData data)
        {
            _minItemsAmountInShop = data.MinItemsAmountInShop;
            DataInstanceID = data.GetInstanceID();
            Name = data.Name;
            Description = data.Description;
            Fraction = data.Fraction;
            PlayerReputation = data.StartReputation;

            Citizens = new List<HubMapUICitizenModel>();
            for (int i = 0; i < data.Citizens.Length; i++)
            {
                HubMapUICitizenModel newCitizen = new HubMapUICitizenModel(data.Citizens[i]);
                Citizens.Add(newCitizen);
            }

            ShopItemsPool = new List<BaseItem>();
            for (int i = 0; i < data.ShopItemsPool.Length; i++)
            {
                ShopItemsPool.Add(data.ShopItemsPool[i]);
            }

            ShopStorage = new HubMapUIStorage(data.ShopSlotAmount);
            BuyBackStorage = new HubMapUIStorage(data.ShopSlotAmount);

            UpdateShopItems();
        }


        public void UpdateShopItems()
        {
            int itemAmount = UnityEngine.Random.Range(_minItemsAmountInShop, ShopStorage.GetSlotsCount());

            for (int i = 0; i < itemAmount; i++)
            {
                int randomItemIndex = UnityEngine.Random.Range(0, ShopItemsPool.Count-1);
                ShopStorage.PutItem(i, ShopItemsPool[randomItemIndex]);
            }
        }
    }
}
