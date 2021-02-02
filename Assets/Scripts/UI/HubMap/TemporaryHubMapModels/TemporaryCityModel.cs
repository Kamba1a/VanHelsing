using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCityInfoModel : ICityInfo
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _fraction;
        [SerializeField] private List<TemporaryCitizenInfoModel> _questGivers;
        [SerializeField] private float _reputation;
        [SerializeField] private List<TemporarySellingItemModel> _sellingItems;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Sprite Fraction => _fraction;
        public List<ICitizenInfo> Citizens => _questGivers.ToList<ICitizenInfo>();
        public float Reputation => _reputation;
        public List<ISellingItem> SellingItems => _sellingItems.ToList<ISellingItem>();

        #endregion
    }
}
