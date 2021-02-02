using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryDialogModel : IDialog
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private string _dialogText;
        [SerializeField] private string _positiveAnswer;
        [SerializeField] private string _negativeAnswer;
        [SerializeField] private bool _isQuest;

        #endregion


        #region Properties

        public int Id => _id;
        public string DialogText => _dialogText;
        public string PositiveAnswer => _positiveAnswer;
        public string NegativeAnswer => _negativeAnswer;
        public bool IsQuest => _isQuest;

        #endregion
    }
}
