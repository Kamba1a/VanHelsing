using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUICitizen : HubMapUIBaseEntity, IHubMapUICitizen
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private HubMapUIDialog[] _dialogs;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public IHubMapUIDialog[] Dialogs => _dialogs;

        #endregion
    }
}
