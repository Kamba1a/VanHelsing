namespace BeastHunter
{
    class HubMapUIGameContentInitializeController
    {
        public HubMapUIGameContentInitializeController(HubMapUIContext context)
        {
            HubMapUIGameContentData data = Data.HubMapData.ContextData;

            context.ShopsSlotsAmount = data.ShopsSlotsAmount;
            context.CharactersEquipmentSlotAmount = data.CharactersEquipmentSlotAmount;
            context.CharactersWeaponSetsAmount = data.CharactersWeaponSetsAmount;
            context.CharactersClothEquipment = data.ClothSlots;

            context.Player = new HubMapUIPlayerModel(data.Player);

            for (int i = 0; i < data.Characters.Length; i++)
            {
                context.Characters.Add(new HubMapUICharacterModel
                    (data.Characters[i], data.CharactersEquipmentSlotAmount, data.ClothSlots, data.CharactersWeaponSetsAmount));
            }

            for (int i = 0; i < data.Cities.Length; i++)
            {
                context.Cities.Add(new HubMapUICityModel(data.Cities[i]));
            }

            for (int i = 0; i < data.Locations.Length; i++)
            {
                context.Locations.Add(new HubMapUILocationModel(data.Locations[i]));
            }

            context.QuestsData = data.Quests;
        }
    }
}
