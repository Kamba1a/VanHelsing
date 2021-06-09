using System;


namespace BeastHunterHubUI
{
    public abstract class MapObjectModel
    {
        #region Fields

        private bool _isBlocked;

        #endregion


        #region Properties

        public Action<MapObjectModel> OnChangeBlockedStatus { get; set; }

        public int InstanceID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public MapObjectBehaviour Behaviour { get; set; }

        public bool IsBlocked
        {
            get
            {
                return _isBlocked;
            }
            set
            {
                if (value != _isBlocked)
                {
                    _isBlocked = value;
                    OnChangeBlockedStatus?.Invoke(this);
                }
            }
        }

        #endregion


        #region ClassLifeCicle

        public MapObjectModel(MapObjectData mapObjectStruct)
        {
            InstanceID = mapObjectStruct.InstanceId;
            Name = mapObjectStruct.Name;
            Description = mapObjectStruct.Description;
            IsBlocked = mapObjectStruct.IsBlocked;
        }

        #endregion
    }
}
