using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCitizenInfoModel : IHubMapUICitizen
    {
        #region Fields

        [SerializeField] [ReadOnlyInUnityInspector] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private TemporaryDialogModel[] _dialogs;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public Sprite Portrait => _portrait;
        public IHubMapUIDialog[] Dialogs => _dialogs;

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        #endregion
    }
}
