using System;
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

        public HubMapUICityModel(HubMapUIMapObjectData mapObjectData) : base(mapObjectData)
        {
            HubMapUICityData cityData = mapObjectData as HubMapUICityData;

            _minItemsAmountInShop = cityData.MinItemsAmountInShop;

            Fraction = cityData.Fraction;
            PlayerReputation = cityData.StartReputation;

            Citizens = new List<HubMapUICitizenModel>();
            for (int i = 0; i < cityData.Citizens.Length; i++)
            {
                HubMapUICitizenModel newCitizen = new HubMapUICitizenModel(cityData.Citizens[i]);
                Citizens.Add(newCitizen);
            }

            ShopItemsPool = new List<BaseItem>();
            for (int i = 0; i < cityData.ShopItemsPool.Length; i++)
            {
                ShopItemsPool.Add(cityData.ShopItemsPool[i]);
            }

            ShopStorage = new HubMapUIStorage(cityData.ShopSlotAmount);
            BuyBackStorage = new HubMapUIStorage(cityData.ShopSlotAmount);

            UpdateShopItems();
        }

        #endregion


        #region Methods

        public void UpdateShopItems()
        {
            ShopStorage.Clear();

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
