﻿using UnityEngine;


namespace BeastHunter
{
    [System.Serializable]
    public class HubMapUIQuestAnswer
    {
        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private int[] _dialogNodesForThisAnswer;
        [SerializeField] private HubMapUIDialogAnswer _answer;

        #endregion


        #region Properties

        public int Id => _id;
        public int[] DialogNodesForThisAnswer => (int[])_dialogNodesForThisAnswer.Clone();
        public HubMapUIDialogAnswer Answer => _answer;
        public bool IsActive { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIQuestAnswer(HubMapUIQuestAnswer hubMapUIQuestAnswers)
        {
            _id = hubMapUIQuestAnswers.Id;
            _dialogNodesForThisAnswer = hubMapUIQuestAnswers.DialogNodesForThisAnswer;
            _answer = hubMapUIQuestAnswers.Answer;
            IsActive = false;
        }

        #endregion


        #region Methods

        public bool IsBelongToDialogNode(int dialogNodeId)
        {
            for (int i = 0; i < DialogNodesForThisAnswer.Length; i++)
            {
                if (DialogNodesForThisAnswer[i] == dialogNodeId)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
