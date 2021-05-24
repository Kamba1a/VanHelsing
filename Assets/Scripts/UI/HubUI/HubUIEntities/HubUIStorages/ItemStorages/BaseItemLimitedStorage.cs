using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseItemLimitedStorage : BaseLimitedStorage<BaseItemModel, ItemStorageType>
    {
        public BaseItemLimitedStorage(int slotsAmount, ItemStorageType storageType) : base(slotsAmount, storageType) { }


        public override Sprite GetElementSpriteBySlot(int slotIndex)
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
