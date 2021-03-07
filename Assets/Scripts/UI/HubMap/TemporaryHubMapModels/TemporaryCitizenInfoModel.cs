using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCitizenInfoModel : ICitizenInfo
    {
        #region Fields

        [SerializeField] [ReadOnlyInUnityInspector] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int _currentDialogId;

        #endregion


        #region Properties

        public int Id => _id;
        public string Name => _name;
        public Sprite Portrait => _portrait;
        public int CurrentDialogId => _currentDialogId;

        #endregion


        #region ClassLifeCyrcle

        public TemporaryCitizenInfoModel(TemporaryCitizenInfoModel citizen)
        {
            _id = citizen.Id;
            _name = citizen.Name;
            _portrait = citizen.Portrait;
            _currentDialogId = citizen.CurrentDialogId;
        }

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        public void NextDialog(int nextDialogId)
        {
            _currentDialogId = nextDialogId;
        }

        #endregion
    }
}
