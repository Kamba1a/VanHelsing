using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "HubUIData", menuName = "CreateData/HubUIData/MainData", order = 0)]
    public class HubUIData : ScriptableObject
    {
        #region Fields

        [Header("HUB UI SETTINGS")]
        [SerializeField] private HubUIContextData _contextData;

        [Header("MAP DATA SETTINGS")]
        public MapDataStruct MapDataStruct;

        #endregion


        #region Properties

        public HubUIContextData ContextData => _contextData;

        #endregion
    }
}
