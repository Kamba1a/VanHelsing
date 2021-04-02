using System;

namespace BeastHunter
{
    public class HubMapUIMapObjectModel
    {
        #region Fields

        private bool _isBlocked;

        #endregion


        #region Properties

        public Action<HubMapUIMapObjectModel> OnChangeBlockedStatus { get; set; }

        public int DataInstanceID { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

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
    }
}
