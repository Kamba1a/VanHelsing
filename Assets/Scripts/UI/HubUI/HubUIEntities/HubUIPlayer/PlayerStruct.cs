using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    [Serializable]
    public struct PlayerStruct
    {
        #region Fields

        [SerializeField] private int _rank;
        [SerializeField] private int _goldAmount;
        [SerializeField] private int _inventorySlotsAmount;
        [SerializeField] private BaseItemData[] _inventoryItems;
        [SerializeField] private CharacterData[] _availableCharacters;

        #endregion


        #region Properties

        public int Rank => _rank;
        public int GoldAmount => _goldAmount;
        public int InventorySlotsAmount => _inventorySlotsAmount;
        public BaseItemData[] InventoryItems => (BaseItemData[])_inventoryItems?.Clone();
        public CharacterData[] AvailableCharacters => (CharacterData[])_availableCharacters?.Clone();

        #endregion
    }
}
