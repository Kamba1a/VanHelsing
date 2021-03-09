using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIDialog : IHubMapUIDialog
    {
        #region Fields

        [SerializeField] private int _number;
        [SerializeField][TextArea(3, 10)] private string _text;
        [SerializeField] private bool _isQuest;
        [SerializeField] private HubMapUIDialogAnswer _positiveAnswer;
        [SerializeField] private HubMapUIDialogAnswer _negativeAnswer;

        #endregion


        #region Properties

        public int Number => _number;
        public string Text => _text;
        public bool IsQuest => _isQuest;
        public IHubMapUIDialogAnswer PositiveAnswer => _positiveAnswer;
        public IHubMapUIDialogAnswer NegativeAnswer => _negativeAnswer;

        #endregion
    }
}
