using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryDialogAnswerModel : IHubMapUIAnswer
    {
        #region Fields

        [SerializeField] [TextArea(3, 10)] private string _text;
        [SerializeField] private bool _isDialogEnd;
        [SerializeField] private int _nextDialogNumber;

        #endregion


        #region Properties

        public string Text => _text;
        public bool IsDialogEnd => _isDialogEnd;
        public int NextDialogNumber => _nextDialogNumber;

        #endregion
    }
}
