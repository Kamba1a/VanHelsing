using UnityEngine;

namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUIQuestTaskAnswers
    {
        [SerializeField] private int[] _dialogNodesForThisAnswer;
        [SerializeField] private HubMapUIDialogAnswer _answer;


        public int[] DialogNodesForThisAnswer => _dialogNodesForThisAnswer;
        public HubMapUIDialogAnswer Answer => _answer;
    }
}
