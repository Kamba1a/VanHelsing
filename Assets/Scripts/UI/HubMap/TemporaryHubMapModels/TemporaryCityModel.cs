using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCityInfoModel : ICityInfo
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _fraction;
        [SerializeField] private int[] _questGiversId;
        [SerializeField] private float _reputation;
        [SerializeField] private int[] _sellingItemsId;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Fraction => _fraction;
        public int[] CitizensId => _questGiversId;
        public float Reputation => _reputation;
        public int[] SellingItemsId => _sellingItemsId;

        #endregion
    }
}
