using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "NewCiy", menuName = "HubMap/City", order = 0)]
    public class TempCityData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 10)] private string _description;
        [SerializeField] private Sprite _fraction;
        [SerializeField] private TempCitizenData[] _citizens;
        [SerializeField] private float _playerReputation;
        [SerializeField] private TempItemData[] _sellingItems;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Sprite Fraction => _fraction;
        public TempCitizenData[] Citizens => _citizens;
        public float CurrentPlayerReputation => _playerReputation;
        public TempItemData[] SellingItems => _sellingItems;

        #endregion
    }
}
