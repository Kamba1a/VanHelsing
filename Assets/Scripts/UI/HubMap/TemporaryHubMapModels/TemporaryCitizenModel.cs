using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCitizenInfoModel : ICitizenInfo
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private bool _isHaveQuest;
        [SerializeField] private string _questDescription;
        [SerializeField] private string _applyQuestAnswer;
        [SerializeField] private string _declineQuestAnswer;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public Sprite Portrait => _portrait;
        public bool IsHaveQuest => _isHaveQuest;
        public string QuestDescription => _questDescription;
        public string ApplyQuestAnswer => _applyQuestAnswer;
        public string DeclineQuestAnswer => _declineQuestAnswer;

        #endregion
    }
}
