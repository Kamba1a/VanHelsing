using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICity", menuName = "CreateData/HubMapUIData/HubMapUICity", order = 0)]
    public class HubMapUICityData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField][TextArea(3,10)] private string _description;
        [SerializeField] private HubMapUIFractionData _fraction;
        [SerializeField] private HubMapUICitizenData[] _citizens;
        [SerializeField] private BaseItem[] _shopItemsPool;
        [SerializeField] private int _startReputation;
        [SerializeField] private int _shopSlotAmount;
        [SerializeField] private int _minItemsAmountInShop;
        [SerializeField] private bool _isBlockedAtStart;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public HubMapUIFractionData Fraction => _fraction;
        public HubMapUICitizenData[] Citizens => _citizens;
        public BaseItem[] ShopItemsPool => _shopItemsPool;
        public int StartReputation => _startReputation;
        public int ShopSlotAmount => _shopSlotAmount;
        public int MinItemsAmountInShop => _minItemsAmountInShop;
        public bool IsBlockedAtStart => _isBlockedAtStart;

        #endregion
    }
}
