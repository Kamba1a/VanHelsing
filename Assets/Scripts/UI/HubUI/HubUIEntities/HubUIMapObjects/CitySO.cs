using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "City", menuName = "CreateData/HubUIData/City", order = 0)]
    public class CitySO : MapObjectSO
    {
        #region Fields

        [SerializeField] private CityStruct _cityData;

        //toremove
        [Header("City data")]
        [SerializeField] private FractionSO _fraction;
        [SerializeField] private CitizenSO[] _citizens;
        [SerializeField] private BaseItemSO[] _shopItemsPool;
        [SerializeField] private int _startReputation;
        [SerializeField] private int _shopSlotAmount;
        [SerializeField] private int _minItemsAmountInShop;

        #endregion


        #region Properties

        public CityStruct CityData => _cityData;

        //toremove
        public FractionSO Fraction => _fraction;
        public CitizenSO[] Citizens => _citizens;
        public BaseItemSO[] ShopItemsPool => _shopItemsPool;
        public int StartReputation => _startReputation;
        public int ShopSlotAmount => _shopSlotAmount;
        public int MinItemsAmountInShop => _minItemsAmountInShop;

        #endregion


        #region HubMapUIMapObjectData

        public override MapObjectType GetMapObjectType()
        {
            return MapObjectType.City;
        }

        #endregion
    }
}
