using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIDialogAnswer
    {
        #region Fields

        [SerializeField] [TextArea(3, 10)] private string _text;
        [SerializeField] private bool _isDialogEnd;
        [SerializeField] private int _nextDialogNodeId;
        [SerializeField] private bool _isProgressQuest;
        [SerializeField] private HubMapUIQuestData _progressQuest;

        #endregion


        #region Properties

        public string Text => _text;
        public bool IsDialogEnd => _isDialogEnd;
        public int NextDialogNodeId => _nextDialogNodeId;
        public bool IsProgressQuest => _isProgressQuest;
        public HubMapUIQuestData ProgressQuest => _progressQuest;

        #endregion
    }
}
