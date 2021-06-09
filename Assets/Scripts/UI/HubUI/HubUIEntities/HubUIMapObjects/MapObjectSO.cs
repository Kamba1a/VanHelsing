using UnityEngine;

namespace BeastHunterHubUI
{
    public abstract class MapObjectSO : ScriptableObject
    {
        #region Fields

        [SerializeField] private MapObjectStruct _mapObjectStruct;


        //TO REMOVE:
        [Header("Map object data")]
        [SerializeField] private bool _isBlockedAtStart;
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 10)] private string _description;

        #endregion


        #region Properties

        public MapObjectStruct MapObjectStruct => _mapObjectStruct;

        //to remove:
        public bool IsBlockedAtStart => _isBlockedAtStart;
        public string Name => _name;
        public string Description => _description;

        #endregion


        #region Methods

        public abstract MapObjectType GetMapObjectType(); //to remove

        #endregion
    }
}
