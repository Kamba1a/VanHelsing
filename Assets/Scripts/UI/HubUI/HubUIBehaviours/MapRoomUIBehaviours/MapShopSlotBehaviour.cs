using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class MapShopSlotBehaviour : MapItemStorageSlotBehaviour
    {
        #region Fields

        [SerializeField] private Image _noReputationImage;

        #endregion


        #region Methods

        public void SetAvailability(bool isAvailability)
        {
            _noReputationImage.enabled = !isAvailability;
        }

        #endregion
    }
}
