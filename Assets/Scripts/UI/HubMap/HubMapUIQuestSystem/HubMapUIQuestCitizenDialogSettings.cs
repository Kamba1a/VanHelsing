using System;
using UnityEngine;


namespace BeastHunter
{
    [Serializable]
    public class HubMapUIQuestCitizenDialogSettings
    {
        #region Fields

        [SerializeField] private HubMapUICitizenData _citizen;
        [SerializeField] private bool _isTarget;
        [SerializeField] private bool _isNpcInitiateDialog;
        [SerializeField] private int _initiatedDialogId;
        [SerializeField] private int _questAnswerId;

        #endregion


        #region Properties

        public HubMapUICitizenData Citizen => _citizen;
        public bool IsTarget => _isTarget;
        public bool IsNpcInitiateDialog => _isNpcInitiateDialog;
        public int InitiatedDialogId => _initiatedDialogId;
        public int QuestAnswerId => _questAnswerId;

        #endregion
    }
}
