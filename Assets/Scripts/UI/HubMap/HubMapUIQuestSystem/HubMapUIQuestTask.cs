using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUIQuestTask
    {
        [SerializeField] private int _id;
        [SerializeField] private HubMapUICitizen _targetCitizen;
        [SerializeField] private int _nextQuestTaskId;
        [SerializeField] private HubMapUIQuestTaskAnswers[] _taskAnswers;

        //[SerializeField] private int _startAnswerId;
        //[SerializeField] private int _endAnswerId;


        public int Id => _id;
        public int NextQuestTaskId => _nextQuestTaskId;
        public HubMapUICitizen TargetCitizen => _targetCitizen;
    }

    [System.Serializable]
    public class HubMapUIQuestTaskAnswers
    {
        [SerializeField] private HubMapUICitizen _citizen;
        [SerializeField] private HubMapUIDialogAnswer _answer;

        public HubMapUICitizen Citizen => _citizen;
        public HubMapUIDialogAnswer Answer => _answer;
    }
}
