namespace BeastHunterHubUI
{
    class HubUIGameContentInitializeController
    {
        public HubUIGameContentInitializeController(HubUIContext context)
        {
            HubUIGameContentData data = BeastHunter.Data.HubMapData.ContextData;

            context.ShopsSlotsAmount = data.ShopsSlotsAmount;
            context.CharactersEquipmentSlotAmount = data.CharactersEquipmentSlotAmount;
            context.CharactersWeaponSetsAmount = data.CharactersWeaponSetsAmount;
            context.CharactersClothEquipment = data.ClothSlots;

            context.Player = new PlayerModel(data.Player);

            for (int i = 0; i < data.Characters.Length; i++)
            {
                context.Characters.Add(new CharacterModel
                    (data.Characters[i], data.CharactersEquipmentSlotAmount, data.ClothSlots, data.CharactersWeaponSetsAmount));
            }

            for (int i = 0; i < data.Cities.Length; i++)
            {
                context.Cities.Add(new CityModel(data.Cities[i]));
            }

            for (int i = 0; i < data.Locations.Length; i++)
            {
                context.Locations.Add(new LocationModel(data.Locations[i]));
            }

            context.QuestsData = data.Quests;
        }
    }
}
