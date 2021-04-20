using UnityEngine;
using UnityEngine.UI;


namespace BeastHunter
{
    class HubMapUIShopSlotBehaviour : HubMapUIStorageSlotBehaviour
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
