using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIPlayerModel
    {
        public int GoldAmount { get; set; }
        public HubMapUIStorage Inventory { get; private set; }
        public List<HubMapUICharacterModel> Characters { get; private set; }
        public int CharactersEquipmentSlotAmount { get; private set; }


        public HubMapUIPlayerModel(HubMapUIPlayerData data)
        {
            GoldAmount = data.GoldAmount;
            CharactersEquipmentSlotAmount = data.CharactersInventorySlotAmount;

            Inventory = new HubMapUIStorage(data.InventorySlotsAmount);
            for (int i = 0; i < data.InventoryItems.Length; i++)
            {
                Inventory.PutItem(i, data.InventoryItems[i]);
            }

            Characters = new List<HubMapUICharacterModel>();
            for (int i = 0; i < data.Characters.Length; i++)
            {
                HubMapUICharacterModel character = new HubMapUICharacterModel(data.Characters[i], CharactersEquipmentSlotAmount);
                Characters.Add(character);
            }
        }


        //public float GetCityReputation(HubMapUICityData city)
        //{
        //    return _citiesReputation.Find(rep => rep.City == city).Reputation;
        //}

        //public void AddCityReputation(HubMapUICityData city, int reputationAmount)
        //{
        //    HubMapUICityReputation cityReputation = _citiesReputation.Find(rep => rep.City == city);
        //    cityReputation.Reputation += reputationAmount;
        //    OnChangeReputationHandler?.Invoke(cityReputation);
        //}
    }
}
