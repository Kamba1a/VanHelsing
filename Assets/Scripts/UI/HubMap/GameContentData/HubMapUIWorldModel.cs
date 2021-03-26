using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIWorldModel
    {
        public HubMapUIPlayerModel Player { get; private set; }
        public List<HubMapUICityModel> Cities { get; private set; }
        public List<HubMapUILocation> Locations { get; private set; }


        public Action<HubMapUICityModel> OnChangePlayerReputationHandler { get; set; }
 
        public HubMapUIWorldModel(HubMapUIWorldData data)
        {
            Player = new HubMapUIPlayerModel(data.Player);

            Cities = new List<HubMapUICityModel>();
            for (int i = 0; i< data.Cities.Length; i++)
            {
                HubMapUICityModel newCity = new HubMapUICityModel(data.Cities[i]);
                newCity.OnChangePlayerReputationHandler += (rep) => OnChangePlayerReputationHandler?.Invoke(rep);

                Cities.Add(newCity);
            }

            Locations = new List<HubMapUILocation>();
            for (int i = 0; i < data.Locations.Length; i++)
            {
                Locations.Add(data.Locations[i]);
            }
        }


        public HubMapUICityModel GetCity(HubMapUICityData cityData)
        {
            return Cities.Find(city => city.DataInstanceID == cityData.GetInstanceID());
        }
    }
}
