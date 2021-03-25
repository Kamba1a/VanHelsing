using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUICityReputation
    {
        [SerializeField] public HubMapUICityData City;
        [SerializeField] public float Reputation;

        public HubMapUICityReputation(HubMapUICityData city, float rep)
        {
            City = city;
            Reputation = rep;
        }
    }
}
