using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICity", menuName = "CreateData/HubMapUIData/HubMapUICity", order = 0)]
    public class HubMapUICity : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField][TextArea(3,10)] private string _description;
        [SerializeField] private Sprite _fraction;
        [SerializeField] private HubMapUICitizen[] _citizens;
        [SerializeField] private BaseItem[] _shopItemsPool;

        #endregion


        #region Properties
        public string Name => _name;
        public string Description => _description;
        public Sprite Fraction => _fraction;
        public HubMapUICitizen[] Citizens => _citizens;
        public BaseItem[] ShopItemsPool => _shopItemsPool;

        #endregion
    }
}
