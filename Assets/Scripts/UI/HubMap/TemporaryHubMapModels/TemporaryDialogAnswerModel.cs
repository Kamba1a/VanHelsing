using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryDialogAnswerModel : IDialogAnswer
    {
        #region Fields

        [SerializeField] private string _text;
        [SerializeField] private bool _isDialogEnd;
        [SerializeField] private int _nextDialogId;

        #endregion


        #region Properties

        public string Text => _text;
        public bool IsDialogEnd => _isDialogEnd;
        public int NextDialogNodeId => _nextDialogId;

        #endregion
    }
}
