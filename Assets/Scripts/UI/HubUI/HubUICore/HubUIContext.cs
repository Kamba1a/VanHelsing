﻿using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class HubUIContext
    {
        #region Properties

        public int ShopsSlotsAmount { get; private set; }
        public int CharactersEquipmentSlotAmount { get; private set; }
        public int CharactersWeaponSetsAmount { get; private set; }
        public ClothesType[] CharactersClothEquipment { get; private set; }
        public PlayerModel Player { get; private set; }
        public List<CharacterModel> Characters { get; private set; }
        public List<CityModel> Cities { get; private set; }
        public List<LocationModel> Locations { get; private set; }
        public QuestData[] QuestsData { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubUIContext()
        {
            Characters = new List<CharacterModel>();
            Cities = new List<CityModel>();
            Locations = new List<LocationModel>();
        }

        #endregion


        #region Methods

        public void Initialize(HubUIContextData data)
        {
            ShopsSlotsAmount = data.ShopsSlotsAmount;
            CharactersEquipmentSlotAmount = data.CharactersEquipmentSlotAmount;
            CharactersWeaponSetsAmount = data.CharactersWeaponSetsAmount;
            CharactersClothEquipment = data.ClothSlots;

            Player = new PlayerModel(data.Player);

            for (int i = 0; i < data.Characters.Length; i++)
            {
                Characters.Add(new CharacterModel
                    (data.Characters[i], data.CharactersEquipmentSlotAmount, data.ClothSlots, data.CharactersWeaponSetsAmount));
            }

            for (int i = 0; i < data.Cities.Length; i++)
            {
                Cities.Add(new CityModel(data.Cities[i]));
            }

            for (int i = 0; i < data.Locations.Length; i++)
            {
                Locations.Add(new LocationModel(data.Locations[i]));
            }

            QuestsData = data.Quests;
        }

        public MapObjectModel GetMapObjectModel(MapObjectData mapObjectData)
        {
            switch (mapObjectData.GetMapObjectType())
            {
                case MapObjectType.Location:
                    return GetLocation(mapObjectData as LocationData);

                case MapObjectType.City:
                    return GetCity(mapObjectData as CityData);

                default:
                    return null;
            }
        }

        public CityModel GetCity(CityData cityData)
        {
            if (cityData != null)
            {
                return Cities.Find(city => city.DataInstanceID == cityData.GetInstanceID());
            }
            else
            {
                Debug.LogError(this + ": input parameter is null");
                return null;
            }
        }

        public LocationModel GetLocation(LocationData locationData)
        {
            if (locationData != null)
            {
                return Locations.Find(location => location.DataInstanceID == locationData.GetInstanceID());
            }
            else
            {
                Debug.LogError(this + ": input parameter is null");
                return null;
            }
        }

        public CitizenModel GetCitizen(CitizenData citizenData)
        {
            if (citizenData != null)
            {
                int citizenDataInstanceID = citizenData.GetInstanceID();
                for (int cityIndex = 0; cityIndex < Cities.Count; cityIndex++)
                {
                    for (int citizenIndex = 0; citizenIndex < Cities[cityIndex].Citizens.Count; citizenIndex++)
                    {
                        if (Cities[cityIndex].Citizens[citizenIndex].DataInstanceId == citizenDataInstanceID)
                        {
                            return Cities[cityIndex].Citizens[citizenIndex];
                        }
                    }
                }
                return null;
            }
            else
            {
                Debug.LogError(this + ": input parameter is null");
                return null;
            }
        }

        #endregion
    }
}
