using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUICity : IHubMapUICity
    {
        #region Fields

        [SerializeField] [ReadOnlyInUnityInspector] private int _id;
        [SerializeField] private string _name;
        [SerializeField][TextArea(3,10)] private string _description;
        [SerializeField] private Sprite _fraction;
        [SerializeField] private int[] _citizensId;
        [SerializeField] private float _playerReputation;
        [SerializeField] private int[] _sellingItemsId;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Fraction => _fraction;
        public int[] CitizensId => _citizensId;
        public float CurrentPlayerReputation => _playerReputation;
        public int[] SellingItemsId => _sellingItemsId;

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        #endregion
    }
}
