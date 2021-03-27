using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUIQuestTaskData
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private HubMapUIQuestCitizenDialogSettings[] _citizenDialogSettings;
        [SerializeField] private int _nextQuestTaskId;

        #endregion


        #region Properties

        public int Id => _id;
        public int NextQuestTaskId => _nextQuestTaskId;
        public HubMapUIQuestCitizenDialogSettings[] CitizenDialogSettings => (HubMapUIQuestCitizenDialogSettings[])_citizenDialogSettings.Clone();

        #endregion
    }
}
