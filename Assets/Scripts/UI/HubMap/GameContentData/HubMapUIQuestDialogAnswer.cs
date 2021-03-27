using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIQuestDialogAnswer : HubMapUIDialogAnswer
    {
        #region Fields

        [SerializeField] private bool _isProgressQuest;
        [SerializeField] private HubMapUIQuestData _progressQuest;

        #endregion


        #region Properties

        public bool IsProgressQuest => _isProgressQuest;
        public HubMapUIQuestData ProgressQuest => _progressQuest;

        #endregion
    }
}
