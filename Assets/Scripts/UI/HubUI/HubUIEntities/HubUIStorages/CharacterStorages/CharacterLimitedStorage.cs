using System.Collections.Generic;


namespace BeastHunterHubUI
{
    public class CharacterLimitedStorage : BaseCharacterLimitedStorage
    {
        public override CharacterStorageType StorageType { get; protected set; }


        public CharacterLimitedStorage(int slotsAmount, CharacterStorageType storageType)
        {
            StorageType = storageType;
            _elementSlots = new List<CharacterModel>();

            for (int i = 0; i < slotsAmount; i++)
            {
                _elementSlots.Add(null);
            }
        }


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
    }
}
