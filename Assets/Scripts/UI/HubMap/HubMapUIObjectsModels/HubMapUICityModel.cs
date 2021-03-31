﻿using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUICityModel : HubMapUIMapObjectModel
    {
        #region Fields

        private int _minItemsAmountInShop;
        private int _playerReputation;

        #endregion


        #region Properties

        public Action<HubMapUICityModel> OnChangePlayerReputationHandler { get; set; }

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

        #endregion


        #region ClassLifeCycle

        public HubMapUICityModel(HubMapUICityData data)
        {
            _minItemsAmountInShop = data.MinItemsAmountInShop;

            DataInstanceID = data.GetInstanceID();
            Name = data.Name;
            Description = data.Description;
            IsBlocked = data.IsBlockedAtStart;

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

        #endregion


        #region Methods

        public void UpdateShopItems()
        {
            int itemAmount = UnityEngine.Random.Range(_minItemsAmountInShop, ShopStorage.GetSlotsCount());

            for (int i = 0; i < itemAmount; i++)
            {
                int randomItemIndex = UnityEngine.Random.Range(0, ShopItemsPool.Count-1);
                ShopStorage.PutItem(i, ShopItemsPool[randomItemIndex]);
            }
        }

        #endregion
    }
}
