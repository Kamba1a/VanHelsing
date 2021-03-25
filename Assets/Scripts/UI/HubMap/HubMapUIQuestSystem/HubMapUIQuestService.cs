using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuestService
    {
        #region Fields

        private HubMapUIPlayerModel _player;
        private List<HubMapUIQuestModel> _notStartedQuests;
        private List<HubMapUIQuestModel> _activeQuests;
        private List<HubMapUIQuestModel> _completedQuests;

        public event Action OnQuestIsActiveHandler;

        #endregion


        #region ClassLifeCycle

        public HubMapUIQuestService(IEnumerable<HubMapUIQuestData> quests)
        {
            _player = Data.HubMapData.Player;

            _notStartedQuests = new List<HubMapUIQuestModel>();
            _activeQuests = new List<HubMapUIQuestModel>();
            _completedQuests = new List<HubMapUIQuestModel>();

            if (quests != null)
            {
                foreach (HubMapUIQuestData questData in quests)
                {
                    _notStartedQuests.Add(new HubMapUIQuestModel(questData, HubMapUIQuestStatus.NotStarted));
                }
            }

            for (int i = 0; i < _notStartedQuests.Count; i++)
            {
                if (CheckQuestForRequiredConditions(_notStartedQuests[i]))
                {
                    StartQuest(_notStartedQuests[i]);
                }
            }

            _player.OnChangeReputationHandler += OnChangeCityReputation;
        }

        #endregion


        #region Methods

        public void QuestProgressed(HubMapUIQuestData questData, HubMapUICitizen citizen)
        {
            HubMapUIQuestModel quest = _activeQuests.Find(q => q.Data == questData);
            if (quest != null)
            {
                if (quest.CurrentTask.TargetCitizen == citizen)
                {
                    HubMapUIQuestTask nextTask = Array.Find(quest.Data.Tasks, task => task.Id == quest.CurrentTask.NextQuestTaskId);
                    quest.CurrentTask = nextTask;

                    if (IsLastTaskComplete(quest))
                    {
                        CompleteQuest(quest);
                    }
                }
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested quest");
            }
        }

        public HubMapUIQuestMarkerType GetQuestMarker(HubMapUICitizen citizen)
        {
            for (int i = 0; i < _activeQuests.Count; i++)
            {
                if (_activeQuests[i].CurrentTask.TargetCitizen == citizen)
                {
                    if (_activeQuests[i].Data.FirstTaskId == _activeQuests[i].CurrentTask.Id)
                    {
                        return HubMapUIQuestMarkerType.Exclamation;
                    }
                    else
                    {
                        return HubMapUIQuestMarkerType.Question;
                    }
                }
            }
            return HubMapUIQuestMarkerType.None;
        }

        public HubMapUIDialogAnswer GetAdditionalQuestAnswer(int currentDialogId)
        {
            for (int questIndex = 0; questIndex < _activeQuests.Count; questIndex++)
            {
                HubMapUIQuestTaskAnswers[] additionalAnswers = _activeQuests[questIndex].CurrentTask.TaskAnswers;
                for (int answerIndex = 0; answerIndex < additionalAnswers.Length; answerIndex++)
                {
                    int[] dialogNodesForAdditionalAnswer = additionalAnswers[answerIndex].DialogNodesForThisAnswer;
                    for (int dialogIndex = 0; dialogIndex < dialogNodesForAdditionalAnswer.Length; dialogIndex++)
                    {
                        if (dialogNodesForAdditionalAnswer[dialogIndex] == currentDialogId)
                        {
                            return additionalAnswers[answerIndex].Answer;
                        }
                    }
                }
            }
            return null;
        }

        private void StartQuest(HubMapUIQuestModel quest)
        {
            if (quest.Status == HubMapUIQuestStatus.NotStarted)
            {
                    quest.Status = HubMapUIQuestStatus.Active;

                    _activeQuests.Add(quest);
                    _notStartedQuests.Remove(quest);

                    OnStartQuest(quest);
                    Debug.Log("Change quest status on active");
            }
        }

        private void CompleteQuest(HubMapUIQuestModel quest)
        {
            if (quest.Status == HubMapUIQuestStatus.Active)
            {
                quest.Status = HubMapUIQuestStatus.Completed;

                _activeQuests.Remove(quest);
                _completedQuests.Add(quest);

                OnQuestComplete();
                Debug.Log("Change quest status on completed");
            }
        }

        private void SetQuestDialogToCitizen(HubMapUIQuestModel quest)
        {
            if (quest.CurrentTask.IsNpcInitiatesDialogue)
            {
                Data.HubMapData.DialogsController.SetNewDialogId(
                    quest.CurrentTask.TargetCitizen,
                    quest.CurrentTask.InitiatedDialogueId);
            }
        }

        private bool IsLastTaskComplete(HubMapUIQuestModel quest)
        {
            return quest.CurrentTask.Id == quest.Data.EmptyEndTaskId;
        }

        private bool CheckQuestForRequiredConditions(HubMapUIQuestModel quest)
        {
            bool checkReputationRequirement = quest.Data.RequiredReputation.Reputation <= _player.GetCityReputation(quest.Data.RequiredReputation.City);
            bool checkQuestRequirement = quest.Data.RequiredQuest == null
                    || _completedQuests.Find(q => q.Data == q.Data.RequiredQuest) != null;

            return checkReputationRequirement && checkQuestRequirement;
        }

        private void OnChangeCityReputation(HubMapUICityReputation cityReputation)
        {
            for (int i = 0; i < _notStartedQuests.Count; i++)
            {
                if (CheckQuestForRequiredConditions(_notStartedQuests[i]))
                {
                    StartQuest(_notStartedQuests[i]);
                }
            }
        }

        private void OnQuestComplete()
        {
            for (int i = 0; i < _notStartedQuests.Count; i++)
            {
                if (CheckQuestForRequiredConditions(_notStartedQuests[i]))
                {
                    StartQuest(_notStartedQuests[i]);
                }
            }
        }

        private void OnStartQuest(HubMapUIQuestModel quest)
        {
            OnQuestIsActiveHandler?.Invoke();
            SetQuestDialogToCitizen(quest);
        }

        #endregion
    }
}
