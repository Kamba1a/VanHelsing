using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIWorldModel
    {
        #region Properties

        public HubMapUIPlayerModel Player { get; private set; }
        public List<HubMapUICityModel> Cities { get; private set; }
        public List<HubMapUILocation> Locations { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIWorldModel(HubMapUIWorldData data)
        {
            Player = new HubMapUIPlayerModel(data.Player);

            Cities = new List<HubMapUICityModel>();
            for (int i = 0; i< data.Cities.Length; i++)
            {
                HubMapUICityModel newCity = new HubMapUICityModel(data.Cities[i]);
                Cities.Add(newCity);
            }

            Locations = new List<HubMapUILocation>();
            for (int i = 0; i < data.Locations.Length; i++)
            {
                Locations.Add(data.Locations[i]);
            }
        }

        #endregion


        #region Methods

        public HubMapUICityModel GetCity(HubMapUICityData cityData)
        {
            return Cities.Find(city => city.DataInstanceID == cityData.GetInstanceID());
        }

        public HubMapUICitizenModel GetCitizen(HubMapUICitizenData citizenData)
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

        #endregion
    }
}
