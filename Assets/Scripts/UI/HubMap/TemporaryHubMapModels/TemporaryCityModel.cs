using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCityInfoModel : ICityInfo
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Image _fraction;
        [SerializeField] private List<TemporaryCitizenInfoModel> _questGivers;
        [SerializeField] private float _reputation;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Image Fraction => _fraction;
        public List<ICitizenInfo> Citizens => _questGivers.ToList<ICitizenInfo>();
        public float Reputation => _reputation;

        #endregion
    }
}
