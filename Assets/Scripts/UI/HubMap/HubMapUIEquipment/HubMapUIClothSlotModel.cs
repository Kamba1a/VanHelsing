//using UnityEngine;


//namespace BeastHunter
//{
//    public class HubMapUIClothSlotModel
//    {
//        public HubMapUIBaseItemModel Item { get; private set; }
//        public HubMapUIClothType SlotType { get; private set; }
//        public Sprite SlotIcon { get; private set; }


//        public HubMapUIClothSlotModel(HubMapUIClothType slotType)
//        {
//            SlotType = slotType;
//            SlotIcon = Data.HubMapData.GetClothSlotSpriteByType(slotType);
//        }


//        public void InitializeBehaviour(HubMapUIEquipmentSlotBehaviour behaviour)
//        {
//            Behaviour = behaviour;
//            //todo
//        }


//        //public bool PutItem(BaseItem item)
//        //{
//        //    if (item.EquipmentType == EquipmentType)
//        //    {
//        //        if (Item == null)
//        //        {
//        //            Item = item;
//        //            return true;
//        //        }
//        //        else
//        //        {
//        //            Debug.Log("There is already an item in the slot");
//        //        }
//        //    }
//        //    else
//        //    {
//        //        Debug.Log("The wrong type of equipment");
//        //    }
//        //    return false;
//        //}

//        //public BaseItem TakeItem()
//        //{
//        //    if (Item != null)
//        //    {
//        //        BaseItem item = Item;
//        //        Item = null;
//        //        return item;
//        //    }
//        //    else
//        //    {
//        //        Debug.Log("Slot is empty");
//        //        return null;
//        //    }
//        //}
//    }
//}
