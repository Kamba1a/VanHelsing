using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIItem : HubMapUIBaseEntity, IHubMapUIItem
    {
        #region Fields

        [SerializeField] private Sprite _image;
        [SerializeField] private float _requiredReputationForSale;

        #endregion


        #region Properties

        public Sprite Image => _image;
        public float RequiredReputationForSale => _requiredReputationForSale;

        #endregion
    }
}
