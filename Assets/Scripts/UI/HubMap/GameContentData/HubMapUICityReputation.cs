using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUICityReputation
    {
        [SerializeField] public HubMapUICity City;
        [SerializeField] public float Reputation;

        public HubMapUICityReputation(HubMapUICity city, float rep)
        {
            City = city;
            Reputation = rep;
        }
    }
}
