using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUICityReputation
    {
        [SerializeField] public HubMapUICityData City;
        [SerializeField] public int Reputation;

        public HubMapUICityReputation(HubMapUICityData city, int rep)
        {
            City = city;
            Reputation = rep;
        }
    }
}
