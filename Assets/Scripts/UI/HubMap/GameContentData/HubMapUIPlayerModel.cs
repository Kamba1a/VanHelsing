using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIPlayerModel
    {
        private List<HubMapUICityReputation> _citiesReputation;


        public int GoldAmount { get; set; }
        public HubMapUIStorage Inventory { get; private set; }
        public List<HubMapUICharacterModel> Characters { get; private set; }
        public int CharactersEquipmentSlotAmount { get; private set; }


        public event Action<HubMapUICityReputation> OnChangeReputationHandler;


        public HubMapUIPlayerModel(HubMapUINewPlayerData newPlayerData)
        {
            GoldAmount = newPlayerData.GoldAmount;
            CharactersEquipmentSlotAmount = newPlayerData.CharactersInventorySlotAmount;

            Inventory = new HubMapUIStorage(newPlayerData.InventorySlotsAmount);
            for (int i = 0; i < newPlayerData.InventoryItems.Length; i++)
            {
                Inventory.PutItem(i, newPlayerData.InventoryItems[i]);
            }

            _citiesReputation = new List<HubMapUICityReputation>();
            for (int i = 0; i < newPlayerData.CitiesReputation.Length; i++)
            {
                _citiesReputation.Add(newPlayerData.CitiesReputation[i]);
            }

            Characters = new List<HubMapUICharacterModel>();
            for (int i = 0; i < newPlayerData.Characters.Length; i++)
            {
                HubMapUICharacterModel character = new HubMapUICharacterModel(newPlayerData.Characters[i], CharactersEquipmentSlotAmount);
                Characters.Add(character);
            }
        }


        public float GetCityReputation(HubMapUICityData city)
        {
            return _citiesReputation.Find(rep => rep.City == city).Reputation;
        }

        public void AddCityReputation(HubMapUICityData city, float reputationAmount)
        {
            HubMapUICityReputation cityReputation = _citiesReputation.Find(rep => rep.City == city);
            cityReputation.Reputation += reputationAmount;
            OnChangeReputationHandler?.Invoke(cityReputation);
        }
    }
}
