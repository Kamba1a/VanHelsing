using System;
using UnityEngine;


namespace BeastHunter
{
    [Serializable]
    public class HubMapUICharacterHeadPart
    {
        #region Fields

        [SerializeField] HubMapUICharacterHeadParts _type;
        [SerializeField] string _name;
        [SerializeField] bool _isActivateByDefault;

        #endregion


        #region Properties

        public HubMapUICharacterHeadParts Type => _type;
        public string Name => _name;
        public bool IsActivateByDefault => _isActivateByDefault;

        #endregion


        #region ClassLifeCycle

        public HubMapUICharacterHeadPart(HubMapUICharacterHeadParts type, string name)
        {
            _type = type;
            _name = name;
            _isActivateByDefault = true;
        }

        #endregion
    }
}
