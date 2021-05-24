using UnityEngine;


namespace BeastHunterHubUI
{
    public class CharacterLimitedStorage : BaseLimitedStorage<CharacterModel, CharacterStorageType>
    {
        public CharacterLimitedStorage(int slotsAmount, CharacterStorageType storageType) : base(slotsAmount, storageType) { }

        public override bool PutElement(int slotIndex, CharacterModel character)
        {
            bool isSucceful = false;

            if (_elementSlots[slotIndex] == null)
            {
                _elementSlots[slotIndex] = character;
                isSucceful = true;
            }
            else
            {
                isSucceful = PutElementToFirstEmptySlot(character);
            }

            if (isSucceful)
            {
                OnPutElementToSlot(slotIndex, character);
            }
            else
            {
                HubUIServices.SharedInstance.GameMessages.Notice(StorageType + " is full");
            }

            return isSucceful;
        }

        public override bool PutElementToFirstEmptySlot(CharacterModel character)
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    return PutElement(i, character);
                }
            }
            return false;
        }

        public override Sprite GetElementSpriteBySlot(int slotIndex)
        {
            if (_elementSlots[slotIndex] != null)
            {
                return _elementSlots[slotIndex].Portrait;
            }
            else
            {
                return null;
            }
        }
    }
}
