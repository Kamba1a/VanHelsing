using UnityEngine;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIEquipmentModel
    {
        private List<HubMapUIEquipmentSlotModel> _equipmentSlots; //cloth and accessories only

        public HubMapUIEquipmentModel(HubMapUIEquipmentData data)
        {
            _equipmentSlots = new List<HubMapUIEquipmentSlotModel>();
            for (int i = 0; i < data.EquipmentSlots.Count; i++)
            {
                _equipmentSlots.Add(new HubMapUIEquipmentSlotModel
                    (data.EquipmentSlots[i],
                    data.GetEquipmentSlotSpriteByType(data.EquipmentSlots[i])));
            }
        }

        //public bool PutItem(HubMapUIEquipmentType equipmentType, BaseItem item)
        //{
        //    //return _equipmentSlots[slotIndex].PutItem(item);
        //}

        private HubMapUIEquipmentSlotModel FindFirstSlotByType(HubMapUIEquipmentType equipmentType)
        {
            for (int i = 0; i < _equipmentSlots.Count; i++)
            {
                if (_equipmentSlots[i].EquipmentType == equipmentType)
                {
                    return _equipmentSlots[i];
                }
            }
            Debug.LogError(this + ": slot of requested type not exist in equipment");
            return null;
        }
    }
}
