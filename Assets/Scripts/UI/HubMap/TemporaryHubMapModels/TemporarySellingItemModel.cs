using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    class TemporarySellingItemModel : ISellingItem
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _image;
        [SerializeField] private float _cost;
        [SerializeField] private float _requiredReputation;
        [SerializeField] private bool _isEnoughReputation;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Sprite Image => _image;
        public float Cost => _cost;
        public float RequiredReputation => _requiredReputation;
        public bool IsEnoughReputation => _isEnoughReputation;

        #endregion
    }
}
