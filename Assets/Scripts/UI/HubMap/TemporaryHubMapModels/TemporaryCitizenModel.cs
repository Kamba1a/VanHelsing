using UnityEngine.UI;
using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCitizenInfoModel : ICitizenInfo
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private bool _isHaveQuest;
        [SerializeField] private string _questDescription;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public bool IsHaveQuest => _isHaveQuest;
        public string QuestDescription => _questDescription;

        #endregion
    }
}
