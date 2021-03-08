using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "HubMap/Item", order = 0)]
    public class TempItemData : ScriptableObject
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
