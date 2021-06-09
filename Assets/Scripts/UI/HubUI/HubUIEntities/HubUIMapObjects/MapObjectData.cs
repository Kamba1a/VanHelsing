using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public class MapObjectData
    {
        #region Fields

        [SerializeField] private int _instanceId;
        [SerializeField] private int _hubMapIndex;
        [SerializeField] private bool _isBlocked;
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 10)] private string _description;

        #endregion


        #region Properties

        public int InstanceId => _instanceId;
        public int HubMapIndex => _hubMapIndex;
        public bool IsBlocked => _isBlocked;
        public string Name => _name;
        public string Description => _description;

        #endregion
    }
}
