using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapUINewPlayerData", order = 0)]
    public class HubMapUINewPlayerData: ScriptableObject
    {
        #region Fields

        private int _goldAmount;
        private int _inventorySlotsAmount;
        private BaseItem[] _inventoryItems;
        private HubMapUICityReputation[] _citiesReputation;
        private HubMapUICharacterData[] _characters;
        private int _charactersInventorySlotAmount;

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
