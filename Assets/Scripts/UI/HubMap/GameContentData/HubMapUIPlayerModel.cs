using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastHunter
{
    public class HubMapUIPlayerModel
    {
        public int GoldAmount { get; set; }
        public HubMapUIStorage Inventory { get; private set; }
        public List<HubMapUICityReputation> CitiesReputation { get; private set; }
        public List<HubMapUICharacterModel> Characters { get; private set; }


        public HubMapUIPlayerModel(HubMapUINewPlayerData newPlayerData)
        {
            GoldAmount = newPlayerData.GoldAmount;

            Inventory = new HubMapUIStorage(newPlayerData.InventorySlotsAmount);
            for (int i = 0; i < newPlayerData.InventoryItems.Length; i++)
            {
                Inventory.PutItem(i, newPlayerData.InventoryItems[i]);
            }

            CitiesReputation = new List<HubMapUICityReputation>();
            for (int i = 0; i < newPlayerData.CitiesReputation.Length; i++)
            {
                CitiesReputation.Add(newPlayerData.CitiesReputation[i]);
            }

            Characters = new List<HubMapUICharacterModel>();
            for (int i = 0; i < newPlayerData.Characters.Length; i++)
            {
                HubMapUICharacterModel character = new HubMapUICharacterModel(newPlayerData.Characters[i], newPlayerData.CharactersInventorySlotAmount);
                Characters.Add(character);
            }
        }
    }
}
