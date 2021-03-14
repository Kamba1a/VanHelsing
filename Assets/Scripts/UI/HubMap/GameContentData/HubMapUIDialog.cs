using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIDialogNode
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField][TextArea(3, 10)] private string _text;
        [SerializeField] private bool _isQuest;
        [SerializeField] private HubMapUIDialogAnswer[] _answers;

        #endregion


        #region Properties

        public int Id => _id;
        public string Text => _text;
        public bool IsQuest => _isQuest;
        public HubMapUIDialogAnswer[] Answers => _answers;

        #endregion
    }
}
