using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class PlayerModel
    {
        #region Properties

        public Action<int> OnChangeGoldAmount { get; set; }

        public int GoldAmount { get; private set; }
        public ItemStorage Inventory { get; private set; }

        #endregion


        #region ClassLifeCycle

        public PlayerModel(PlayerSettingsStruct settings)
        {
            GoldAmount = settings.GoldAmount;

            Inventory = new ItemStorage(settings.InventorySlotsAmount, ItemStorageType.GeneralInventory);
            for (int i = 0; i < settings.InventoryItems.Length; i++)
            {
                BaseItemModel itemModel = HubUIServices.SharedInstance.
                    ItemInitializeService.InitializeItemModel(settings.InventoryItems[i]);
                Inventory.PutItem(i, itemModel);
            }
        }

        #endregion


        #region Methods

        public bool AddGold(int goldAmount)
        {
            if(goldAmount > 0)
            {
                GoldAmount += goldAmount;
                OnChangeGoldAmount?.Invoke(GoldAmount);
                return true;
            }
            Debug.LogError("goldAmount parameter is less than or equal to zero");
            return false;
        }

        public bool TakeGold(int goldAmount)
        {
            if (goldAmount > 0)
            {
                GoldAmount -= goldAmount;
                OnChangeGoldAmount?.Invoke(GoldAmount);
                return true;
            }
            Debug.LogError("goldAmount parameter is less than or equal to zero");
            return false;
        }

        #endregion
    }
}
