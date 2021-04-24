using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    [Serializable]
    public class PlayerSettingsStruct
    {
        #region Fields

        [SerializeField] private int _goldAmount;
        [SerializeField] private int _inventorySlotsAmount;
        [SerializeField] private BaseItemData[] _inventoryItems;

        #endregion


        #region Properties

        public int GoldAmount => _goldAmount;
        public int InventorySlotsAmount => _inventorySlotsAmount;
        public BaseItemData[] InventoryItems => (BaseItemData[])_inventoryItems.Clone();

        #endregion
    }
}
