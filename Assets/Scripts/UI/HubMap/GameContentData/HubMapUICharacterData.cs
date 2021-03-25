using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICharacter", menuName = "CreateData/HubMapUIData/HubMapUICharacter", order = 0)]
    public class HubMapUICharacterData : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private BaseItem[] _startItems;
        private HubMapUIStorage _backpack;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public HubMapUIStorage Backpack => _backpack;
        public BaseItem[] StartItems => (BaseItem[])_startItems.Clone();

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
