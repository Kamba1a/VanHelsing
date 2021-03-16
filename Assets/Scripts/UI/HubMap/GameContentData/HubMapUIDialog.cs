using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIDialogNode
    {
        #region Fields

        [SerializeField][ReadOnlyInUnityInspector] private int _id;
        [SerializeField][TextArea(3, 10)] private string _text;
        [SerializeField] private HubMapUIDialogAnswer[] _answers;

        #endregion


        #region Properties

        public int Id => _id;
        public string Text => _text;
        public HubMapUIDialogAnswer[] Answers => _answers;

        #endregion

        public void SetId(int id)
        {
            _id = id;
        }
    }
}
