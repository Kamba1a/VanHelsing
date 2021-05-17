using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseItemStorage : BaseStorage<BaseItemModel, ItemStorageType>
    {
        public virtual Sprite GetItemIconBySlot(int slotIndex)
        {
            if (_elementSlots[slotIndex] != null)
            {
                return _elementSlots[slotIndex].Icon;
            }
            else
            {
                return null;
            }
        }
    }
}
