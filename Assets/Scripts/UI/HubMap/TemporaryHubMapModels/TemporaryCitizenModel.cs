using UnityEngine.UI;

namespace BeastHunter
{
    public class TemporaryCitizenInfoModel : ICitizenInfo
    {
        #region Fields

        private string _name;
        private Image _portrait;
        private bool _isHaveQuest;
        private string _questDescription;

        #endregion


        #region Properties

        public string Name => _name;
        public Image Portrait => _portrait;
        public bool IsHaveQuest => _isHaveQuest;
        public string QuestDescription => _questDescription;

        #endregion


        #region ClassLifeCircle

        public TemporaryCitizenInfoModel(string name, Image portrait, bool isHaveQuest, string questDescription)
        {
            _name = name;
            _portrait = portrait;
            _isHaveQuest = isHaveQuest;
            _questDescription = questDescription;
        }

        #endregion
    }
}
