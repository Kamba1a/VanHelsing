using System.Collections.Generic;
using UnityEngine.UI;

namespace BeastHunter
{
    public class TemporaryCityInfoModel : ICityInfo
    {
        #region Fields

        private string _name;
        private string _description;
        private Image _fraction;
        private List<ICitizenInfo> _questGivers;
        private float _reputation;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public Image Fraction => _fraction;
        public List<ICitizenInfo> Citizens => _questGivers;
        public float Reputation => _reputation;

        #endregion


        #region ClassLifeCircle

        public TemporaryCityInfoModel(string name, string description, Image fraction, List<ICitizenInfo> questGivers, float reputation)
        {
            _name = name;
            _description = description;
            _fraction = fraction;
            _questGivers = questGivers;
            _reputation = reputation;
        }

        #endregion
    }
}
