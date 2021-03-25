using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUINewPlayerData", menuName = "CreateData/HubMapUIData/HubMapUINewPlayerData", order = 0)]
    public class HubMapUINewPlayerData: ScriptableObject
    {
        #region Fields

        [SerializeField] private int _goldAmount;
        [SerializeField] private int _inventorySlotsAmount;
        [SerializeField] private BaseItem[] _inventoryItems;
        [SerializeField] private HubMapUICityReputation[] _citiesReputation;
        [SerializeField] private HubMapUICharacterData[] _characters;
        [SerializeField] private int _charactersInventorySlotAmount;

        #endregion


        #region Properties

        public int GoldAmount => _goldAmount;
        public int InventorySlotsAmount => _inventorySlotsAmount;
        public BaseItem[] InventoryItems => (BaseItem[])_inventoryItems.Clone();
        public HubMapUICityReputation[] CitiesReputation => (HubMapUICityReputation[])_citiesReputation.Clone();
        public HubMapUICharacterData[] Characters => (HubMapUICharacterData[])_characters.Clone();
        public int CharactersInventorySlotAmount => _charactersInventorySlotAmount;

        #endregion
    }
}
