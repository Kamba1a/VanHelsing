using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCitizenInfoModel : ICitizenInfo
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int _dialogId;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public Sprite Portrait => _portrait;
        public int DialogId => _dialogId;

        #endregion
    }
}
