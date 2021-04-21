namespace BeastHunterHubUI
{
    public class PlayerModel
    {
        #region Properties

        public int GoldAmount { get; set; }
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
    }
}
