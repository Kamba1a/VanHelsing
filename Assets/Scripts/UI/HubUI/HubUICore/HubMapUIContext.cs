using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIContext
    {
        #region Properties

        public int ShopsSlotsAmount { get; set; }
        public int CharactersEquipmentSlotAmount { get; set; }
        public int CharactersWeaponSetsAmount { get; set; }
        public HubMapUIClothesType[] CharactersClothEquipment { get; set; }
        public HubMapUIPlayerModel Player { get; set; }
        public List<HubMapUICharacterModel> Characters { get; set; }
        public List<HubMapUICityModel> Cities { get; set; }
        public List<HubMapUILocationModel> Locations { get; set; }
        public HubMapUIQuestData[] QuestsData { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIContext()
        {
            Characters = new List<HubMapUICharacterModel>();
            Cities = new List<HubMapUICityModel>();
            Locations = new List<HubMapUILocationModel>();
        }

        #endregion


        #region Methods

        public HubMapUIMapObjectModel GetMapObjectModel(HubMapUIMapObjectData mapObjectData)
        {
            switch (mapObjectData.GetMapObjectType())
            {
                case HubMapUIMapObjectType.Location:
                    return GetLocation(mapObjectData as HubMapUILocationData);

                case HubMapUIMapObjectType.City:
                    return GetCity(mapObjectData as HubMapUICityData);

                default:
                    return null;
            }
        }

        public HubMapUICityModel GetCity(HubMapUICityData cityData)
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

        public HubMapUILocationModel GetLocation(HubMapUILocationData locationData)
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

        public HubMapUICitizenModel GetCitizen(HubMapUICitizenData citizenData)
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
