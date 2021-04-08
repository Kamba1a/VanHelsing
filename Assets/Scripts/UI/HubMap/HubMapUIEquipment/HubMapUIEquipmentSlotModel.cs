using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIEquipmentSlotModel
    {
        public BaseItem Item { get; private set; }
        public HubMapUIEquipmentType EquipmentType { get; private set; }

        private Sprite Icon; //to delete?


        public HubMapUIEquipmentSlotModel(HubMapUIEquipmentType equipmentType, Sprite icon)
        {
            EquipmentType = equipmentType;
            Icon = icon;
        }


        public bool PutItem(BaseItem item)
        {
            if (item.ItemStruct.EquipmentType == EquipmentType)
            {
                if (Item == null)
                {
                    Item = item;
                    return true;
                }
                else
                {
                    Debug.Log("There is already an item in the slot");
                }
            }
            else
            {
                Debug.Log("The wrong type of equipment");
            }
            return false;
        }

        public BaseItem TakeItem()
        {
            if (Item != null)
            {
                BaseItem item = Item;
                Item = null;
                return item;
            }
            else
            {
                Debug.Log("Slot is empty");
                return null;
            }
        }
    }
}
