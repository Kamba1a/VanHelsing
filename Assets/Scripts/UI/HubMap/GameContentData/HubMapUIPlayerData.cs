using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIPlayerData", menuName = "CreateData/HubMapUIData/HubMapUIPlayerData", order = 0)]
    public class HubMapUIPlayerData: ScriptableObject
    {
        #region Fields

        [SerializeField] private int _goldAmount;
        [SerializeField] private int _inventorySlotsAmount;
        [SerializeField] private BaseItem[] _inventoryItems;
        [SerializeField] private HubMapUICharacterData[] _characters;
        [SerializeField] private int _charactersInventorySlotAmount;

        #endregion


        #region Properties

        public int GoldAmount => _goldAmount;
        public int InventorySlotsAmount => _inventorySlotsAmount;
        public BaseItem[] InventoryItems => (BaseItem[])_inventoryItems.Clone();
        public HubMapUICharacterData[] Characters => (HubMapUICharacterData[])_characters.Clone();
        public int CharactersInventorySlotAmount => _charactersInventorySlotAmount;

        #endregion
    }
}
