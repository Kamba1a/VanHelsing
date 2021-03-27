using UnityEngine;
using UnityEngine.UI;


namespace BeastHunter
{
    class HubMapUIShopSlotBehaviour : HubMapUISlotBehaviour
    {
        [SerializeField] private Image _noReputationImage;

        public void SetAvailability(bool isEnoughReputation)
        {
            _noReputationImage.enabled = !isEnoughReputation;
        }
    }
}
