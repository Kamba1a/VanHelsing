using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICharacter", menuName = "CreateData/HubMapUIData/HubMapUICharacter", order = 0)]
    public class HubMapUICharacter : ScriptableObject
    {
        #region Fields

        [SerializeField] private Sprite _portrait;
        [SerializeField] private BaseItem[] _startItems;
        private HubMapUIStorage _backpack;

        #endregion


        #region Properties

        public Sprite Portrait => _portrait;
        public HubMapUIStorage Backpack => _backpack;

        #endregion

        public void OnEnable()
        {
            _backpack = new HubMapUIStorage(Data.HubMapData.CharactersEquipmentSlotsAmount);
        }


        #region Methods

        public void SetStartEquipment()
        {
            for (int i = 0; i < _startItems.Length; i++)
            {
                _backpack.PutItem(i, _startItems[i]);
            }
        }

        #endregion
    }
}
