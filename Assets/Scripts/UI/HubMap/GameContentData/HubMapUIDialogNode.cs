using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUIDialogNode
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField][TextArea(3, 10)] private string _text;
        [SerializeField] private List<HubMapUIDialogAnswer> _answers;

        #endregion


        #region Properties

        public int Id => _id;
        public string Text => _text;
        public List<HubMapUIDialogAnswer> Answers => _answers;

        #endregion


        public void SetId(int id)   //todo: auto id increment in citizens
        {
            _id = id;
        }
    }
}
