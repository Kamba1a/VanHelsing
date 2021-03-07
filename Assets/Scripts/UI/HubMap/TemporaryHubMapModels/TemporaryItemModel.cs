using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryItemModel : IItemInfo
    {
        #region Fields

        [SerializeField] [ReadOnlyInUnityInspector] private int _id;
        [SerializeField] private Sprite _image;
        [SerializeField] private float _requiredReputationForSale;

        #endregion


        #region Properties

        public int Id => _id;
        public Sprite Image => _image;
        public float RequiredReputationForSale => _requiredReputationForSale;

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        #endregion
    }
}
