using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "Location", menuName = "CreateData/HubUIData/Location", order = 0)]
    public class LocationSO : ScriptableObject
    {
        #region Fields

        [SerializeField] private LocationData _locationData;

        #endregion


        #region Properties

        public LocationData LocationData => _locationData;

        #endregion
    }
}
