using System;
using System.Collections.Generic;


namespace BeastHunterHubUI
{
    public class CityModel : MapObjectModel
    {
        #region Fields

        private int _minItemsAmountInShop;
        private int _playerReputation;

        #endregion


        #region Properties

        public Action<CityModel> OnChangePlayerReputationHandler { get; set; }

        public FractionData Fraction { get; private set; } 
        public List<CitizenModel> Citizens { get; private set; }
        public List<BaseItemData> ShopItemsPool { get; private set; }
        public ItemSlotStorage ShopStorage { get; private set; }
        public ItemSlotStorage BuyBackStorage { get; private set; }

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

        public CityModel(MapObjectData mapObjectData) : base(mapObjectData)
        {
            CityData cityData = mapObjectData as CityData;

            _minItemsAmountInShop = cityData.MinItemsAmountInShop;

            Fraction = cityData.Fraction;
            PlayerReputation = cityData.StartReputation;

            Citizens = new List<CitizenModel>();
            for (int i = 0; i < cityData.Citizens.Length; i++)
            {
                CitizenModel newCitizen = new CitizenModel(cityData.Citizens[i]);
                Citizens.Add(newCitizen);
            }

            ShopItemsPool = new List<BaseItemData>();
            for (int i = 0; i < cityData.ShopItemsPool.Length; i++)
            {
                ShopItemsPool.Add(cityData.ShopItemsPool[i]);
            }

            ShopStorage = new ItemSlotStorage(cityData.ShopSlotAmount, ItemStorageType.ShopStorage);
            BuyBackStorage = new ItemSlotStorage(cityData.ShopSlotAmount, ItemStorageType.ShopBuyBackStorage);

            UpdateShopItems();
        }

        #endregion


        #region Methods

        public void UpdateShopItems()
        {
            ShopStorage.ClearSlots();

            int itemAmount = UnityEngine.Random.Range(_minItemsAmountInShop, ShopStorage.GetSlotsCount());

            for (int i = 0; i < itemAmount; i++)
            {
                int randomItemIndex = UnityEngine.Random.Range(0, ShopItemsPool.Count-1);
                BaseItemModel itemModel = HubUIServices.SharedInstance.
                    ItemInitializeService.InitializeItemModel(ShopItemsPool[randomItemIndex]);
                ShopStorage.PutElement(i, itemModel);
            }
        }

        #endregion
    }
}
