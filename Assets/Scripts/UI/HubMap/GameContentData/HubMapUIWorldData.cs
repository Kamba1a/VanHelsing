using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIWorldData", menuName = "CreateData/HubMapUIData/HubMapUIWorldData", order = 0)]
    public class HubMapUIWorldData : ScriptableObject
    {
        #region Fields

        [SerializeField] private HubMapUIPlayerData _player;
        [SerializeField] private HubMapUICityData[] _cities;
        [SerializeField] private HubMapUILocation[] _locations;

        #endregion


        #region Properties

        public HubMapUIPlayerData Player => _player;
        public HubMapUICityData[] Cities => (HubMapUICityData[])_cities.Clone();
        public HubMapUILocation[] Locations => (HubMapUILocation[])_locations.Clone();

        #endregion
    }
}
