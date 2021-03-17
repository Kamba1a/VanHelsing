using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUIQuestTask
    {
        [SerializeField] private int _id;
        [SerializeField] private HubMapUICitizen _targetCitizen;
        [SerializeField] private bool _isNpcInitiatesDialogue;
        [SerializeField] private int _initiatedDialogueId;
        [SerializeField] private HubMapUIQuestTaskAnswers[] _taskAnswers;
        [SerializeField] private int _nextQuestTaskId;

        public int Id => _id;
        public int NextQuestTaskId => _nextQuestTaskId;
        public HubMapUICitizen TargetCitizen => _targetCitizen;
        public bool IsNpcInitiatesDialogue => _isNpcInitiatesDialogue;
        public int InitiatedDialogueId => _initiatedDialogueId;
        public HubMapUIQuestTaskAnswers[] TaskAnswers => _taskAnswers;
    }
}
