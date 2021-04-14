using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIPocketsStorage : HubMapUIItemStorage
    {
        public Action OnChangeSlotsAmountHandler { get; set; }


        public HubMapUIPocketsStorage() : base(0, HubMapUIItemStorageType.PocketsStorage) { }


        public void AddPockets(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _items.Add(null);
            }

            OnChangeSlotsAmountHandler?.Invoke();
        }

        public bool IsEnoughFreeSlots(int amount)
        {
            int freeSlots = 0;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                {
                    freeSlots++;
                    if (freeSlots >= amount)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemovePockets(int amount)
        {
            List<int> slotsToDelete = new List<int>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                {
                    slotsToDelete.Add(i);
                    if (slotsToDelete.Count >= amount)
                    {
                        break;
                    }
                }
            }

            if (slotsToDelete.Count < amount)
            {
                CustomDebug.Log("RemovePockets is NOT succeful");
                return false;
            }
            else
            {
                for (int i = slotsToDelete.Count-1; i >= 0; i--)
                {
                    _items.RemoveAt(slotsToDelete[i]);
                }
                CustomDebug.Log("RemovePockets is succeful");
                OnChangeSlotsAmountHandler?.Invoke();
                return true;
            }
        }
    }
}
