using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseItemLimitedStorage : BaseLimitedStorage<BaseItemModel, ItemStorageType>
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
