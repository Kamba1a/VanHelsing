using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUIQuestTaskAnswers
    {
        [SerializeField] private HubMapUICitizen _citizen;
        [SerializeField] private HubMapUIDialogAnswer _answer;

        public HubMapUICitizen Citizen => _citizen;
        public HubMapUIDialogAnswer Answer => _answer;
    }
}
