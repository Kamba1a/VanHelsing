using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUICharacterClothesModuleParts
    {
        #region Fields

        [SerializeField] HubMapUIClothesType _type;
        [SerializeField] List<string> _names;

        #endregion


        #region Properties

        public HubMapUIClothesType Type => _type;
        public List<string> Names => _names;

        #endregion


        #region ClassLifeCycle

        public HubMapUICharacterClothesModuleParts(HubMapUIClothesType type, List<string> names)
        {
            _type = type;
            _names = names;
        }

        #endregion
    }
}
