//using UnityEngine;
//using System.Collections.Generic;


//namespace BeastHunter
//{
//    public class HubMapUICharacterClothModel
//    {
//        private HubMapUIClothSlotModel[] _equipmentSlots;

//        public HubMapUICharacterClothModel(HubMapUIClothType[] clothTypes)
//        {
//            _equipmentSlots = new HubMapUIClothSlotModel[clothTypes.Length];
//            for (int i = 0; i < _equipmentSlots.Length; i++)
//            {
//                _equipmentSlots[i] = new HubMapUIClothSlotModel(clothTypes[i]);
//            }
//        }

//        //public bool PutItem(HubMapUIEquipmentType equipmentType, BaseItem item)
//        //{
//        //    //return _equipmentSlots[slotIndex].PutItem(item);
//        //}

//        //public HubMapUIEquipmentSlotModel[] GetAllSlots()
//        //{
//        //    return (HubMapUIEquipmentSlotModel[])_equipmentSlots.Clone(); ;
//        //}

//        //private HubMapUIEquipmentSlotModel FindFirstSlotByType(HubMapUIEquipmentType equipmentType)
//        //{
//        //    for (int i = 0; i < _equipmentSlots.Length; i++)
//        //    {
//        //        if (_equipmentSlots[i].EquipmentType == equipmentType)
//        //        {
//        //            return _equipmentSlots[i];
//        //        }
//        //    }
//        //    Debug.LogError(this + ": slot of requested type not exist in equipment");
//        //    return null;
//        //}
//    }
//}
