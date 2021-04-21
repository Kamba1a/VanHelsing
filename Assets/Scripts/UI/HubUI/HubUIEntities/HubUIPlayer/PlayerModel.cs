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

        public PlayerModel(PlayerData data)
        {
            GoldAmount = data.GoldAmount;

            Inventory = new ItemStorage(data.InventorySlotsAmount, ItemStorageType.GeneralInventory);
            for (int i = 0; i < data.InventoryItems.Length; i++)
            {
                BaseItemModel itemModel = HubUIServices.SharedInstance.
                    ItemInitializeService.InitializeItemModel(data.InventoryItems[i]);
                Inventory.PutItem(i, itemModel);
            }
        }

        #endregion

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
    }
}
