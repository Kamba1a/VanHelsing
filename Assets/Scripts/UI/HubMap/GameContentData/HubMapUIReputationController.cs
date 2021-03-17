using System;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIReputationController
    {
        private List<HubMapUICityReputation> _citiesReputation;

        public event Action<HubMapUICityReputation> OnChangeReputationHandler;

        public HubMapUIReputationController(IEnumerable<HubMapUICityReputation> citiesReputation)
        {
            _citiesReputation = new List<HubMapUICityReputation>();

            if (citiesReputation != null)
            {
                foreach (HubMapUICityReputation cityReputation in citiesReputation)
                {
                    _citiesReputation.Add(new HubMapUICityReputation(cityReputation.City, cityReputation.Reputation));
                }
            }
        }

        public float GetReputation(HubMapUICity city)
        {
            return _citiesReputation.Find(e => e.City == city).Reputation;
        }

        public void AddReputation(HubMapUICity city, float number)
        {
            HubMapUICityReputation cityReputation = _citiesReputation.Find(e => e.City == city);
            cityReputation.Reputation += number;
            OnChangeReputationHandler?.Invoke(cityReputation);
        }
    }
}
