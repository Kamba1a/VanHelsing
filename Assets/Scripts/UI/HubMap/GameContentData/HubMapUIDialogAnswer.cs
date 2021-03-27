﻿using System;
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

        #endregion


        #region Properties

        public Action<int> OnAnswerSelectByPlayerHandler { get; set; }

        public string Text => _text;
        public bool IsDialogEnd => _isDialogEnd;
        public int NextDialogNodeId => _nextDialogNodeId;

        #endregion


        #region Methods

        public void SelectedByPlayer()
        {
            OnAnswerSelectByPlayerHandler?.Invoke(_nextDialogNodeId);
        }

        #endregion
    }
}
