using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryDialogModel : IHubMapUIDialog
    {
        #region Fields

        [SerializeField] private int _number;
        [SerializeField][TextArea(3, 10)] private string _text;
        [SerializeField] private bool _isQuest;
        [SerializeField] private TemporaryDialogAnswerModel _positiveAnswer;
        [SerializeField] private TemporaryDialogAnswerModel _negativeAnswer;

        #endregion


        #region Properties

        public int Number => _number;
        public string Text => _text;
        public bool IsQuest => _isQuest;
        public IHubMapUIAnswer PositiveAnswer => _positiveAnswer;
        public IHubMapUIAnswer NegativeAnswer => _negativeAnswer;

        #endregion
    }
}
