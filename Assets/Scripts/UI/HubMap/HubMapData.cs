using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        [SerializeField] bool _mapOnStartEnabled;
        public bool MapOnStartEnabled => _mapOnStartEnabled;

        public void Updating()
        {
            
        }
    }
}
