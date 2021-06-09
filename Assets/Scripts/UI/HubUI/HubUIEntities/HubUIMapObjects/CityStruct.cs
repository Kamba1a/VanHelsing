using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    [Serializable]
    public class CityStruct
    {
        #region Fields

        [SerializeField] private MapObjectStruct _mapObjectData;
        [SerializeField] private FractionSO _fraction;
        [SerializeField] private CitizenSO[] _citizens;
        [SerializeField] private BaseItemSO[] _shopItemsPool;
        [SerializeField] private int _playerReputation;
        [SerializeField] private int _minItemsAmountInShop;

        #endregion


        #region Properties

        public MapObjectStruct MapObjectData => _mapObjectData;
        public FractionSO Fraction => _fraction;
        public CitizenSO[] Citizens => _citizens;
        public BaseItemSO[] ShopItemsPool => _shopItemsPool;
        public int PlayerReputation => _playerReputation;
        public int MinItemsAmountInShop => _minItemsAmountInShop;

        #endregion
    }
}
