using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuestsController
    {
        private Dictionary<HubMapUIQuest, HubMapUIQuestInfo> _questsInfo;


        public event Action OnQuestIsActiveHandler;
        public event Action OnNextTaskHandler;


        public HubMapUIQuestsController(IEnumerable<HubMapUIQuest> quests)
        {
            _questsInfo = new Dictionary<HubMapUIQuest, HubMapUIQuestInfo>();

            if (quests != null)
            {
                foreach (HubMapUIQuest quest in quests)
                {
                    if (!_questsInfo.ContainsKey(quest))
                    {
                        HubMapUIQuestInfo questInfo = new HubMapUIQuestInfo(HubMapUIQuestStatus.NotStarted, quest.FirstTask);
                        _questsInfo.Add(quest, questInfo);
                    }
                }
            }

            DefineAllQuestStatuses();
            Data.HubMapData.ReputationController.OnChangeReputationHandler += OnChangeCityReputation;
        }


        public void DefineAllQuestStatuses()
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in _questsInfo)
            {
                DefineQuestStatus(kvp.Key);
            }
        }

        private void DefineQuestStatus(HubMapUIQuest quest)
        {
            switch (_questsInfo[quest].QuestStatus)
            {
                case HubMapUIQuestStatus.NotStarted:

                    if (CheckQuestForRequiredConditions(quest))
                    {
                        _questsInfo[quest].SetQuestStatus(HubMapUIQuestStatus.Active);
                        OnQuestActive(quest);
                        Debug.Log("Change quest status on active");
                    }

                    break;
                case HubMapUIQuestStatus.Active:

                    if(_questsInfo[quest].CurrentTask.Id == quest.EmptyEndTaskId)
                    {
                        _questsInfo[quest].SetQuestStatus(HubMapUIQuestStatus.Completed);
                        OnQuestComplete();
                        Debug.Log("Change quest status on completed");
                    }
                    break;
                default:
                    break;
            }
        }

        private void SetQuestDialogToCitizen(HubMapUIQuest quest)
        {
            if (_questsInfo[quest].CurrentTask.IsNpcInitiatesDialogue)
            {
                Data.HubMapData.DialogsController.SetNewDialogId(
                    _questsInfo[quest].CurrentTask.TargetCitizen,
                    _questsInfo[quest].CurrentTask.InitiatedDialogueId);
            }
        }

        private bool CheckQuestForRequiredConditions(HubMapUIQuest quest)
        {
            return CheckQuestForReputationRequirement(quest) && CheckQuestForQuestRequirement(quest);
        }

        private bool CheckQuestForReputationRequirement(HubMapUIQuest quest)
        {
            return quest.RequiredReputation.Reputation <= Data.HubMapData.ReputationController.GetReputation(quest.RequiredReputation.City);
        }

        private bool CheckQuestForQuestRequirement(HubMapUIQuest quest)
        {
            return quest.RequiredQuest == null
                    || _questsInfo[quest.RequiredQuest].QuestStatus == HubMapUIQuestStatus.Completed;
        }

        public Dictionary<HubMapUIQuest, HubMapUIQuestInfo> GetActiveQuests()
        {
            Dictionary<HubMapUIQuest, HubMapUIQuestInfo> dic = new Dictionary<HubMapUIQuest, HubMapUIQuestInfo>();
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in _questsInfo)
            { 
                if (kvp.Value.QuestStatus == HubMapUIQuestStatus.Active)
                {
                    dic.Add(kvp.Key, kvp.Value);
                }
            }
            return dic;
        }

        public Dictionary<HubMapUIQuest, HubMapUIQuestInfo> GetNotStartedQuests()
        {
            Dictionary<HubMapUIQuest, HubMapUIQuestInfo> dic = new Dictionary<HubMapUIQuest, HubMapUIQuestInfo>();
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in _questsInfo)
            {
                if (kvp.Value.QuestStatus == HubMapUIQuestStatus.NotStarted)
                {
                    dic.Add(kvp.Key, kvp.Value);
                }
            }
            return dic;
        }

        public Dictionary<HubMapUIQuest, HubMapUIQuestInfo> GetCompletedQuests()
        {
            Dictionary<HubMapUIQuest, HubMapUIQuestInfo> dic = new Dictionary<HubMapUIQuest, HubMapUIQuestInfo>();
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in _questsInfo)
            {
                if (kvp.Value.QuestStatus == HubMapUIQuestStatus.Completed)
                {
                    dic.Add(kvp.Key, kvp.Value);
                }
            }
            return dic;
        }

        public HubMapUIQuestMarkerType GetQuestMarker(HubMapUICitizen citizen)
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in GetActiveQuests())
            {
                if (kvp.Value.CurrentTask.TargetCitizen == citizen)
                {
                    if (kvp.Key.FirstTaskId == kvp.Value.CurrentTask.Id)
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

        public void SetNextTask(HubMapUIQuest quest)
        {
            if (_questsInfo.ContainsKey(quest))
            {
                HubMapUIQuestTask currentTask = _questsInfo[quest].CurrentTask;
                HubMapUIQuestTask nextTask = Array.Find(quest.GetTasks(), task => task.Id == currentTask.NextQuestTaskId);
                _questsInfo[quest].SetCurrentTask(nextTask);
                DefineQuestStatus(quest);
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested quest");
            }
        }

        private void OnChangeCityReputation(HubMapUICityReputation cityReputation)
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in GetNotStartedQuests())
            {
                DefineQuestStatus(kvp.Key);
            }
        }

        private void OnQuestComplete()
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in GetNotStartedQuests())
            {
                DefineQuestStatus(kvp.Key);
            }
        }

        private void OnQuestActive(HubMapUIQuest quest)
        {
            OnQuestIsActiveHandler?.Invoke();
            SetQuestDialogToCitizen(quest);
        }

        public HubMapUIDialogAnswer GetAdditionalQuestAnswer(int currentDialogId)
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestInfo> kvp in GetActiveQuests())
            {
                HubMapUIQuestTaskAnswers[] additionalAnswers = kvp.Value.CurrentTask.TaskAnswers;
                for (int i = 0; i < additionalAnswers.Length; i++)
                {
                    int[] dialogNodesForAdditionalAnswer = additionalAnswers[i].DialogNodesForThisAnswer;
                    for (int j = 0; j < dialogNodesForAdditionalAnswer.Length; j++)
                    {
                        if (dialogNodesForAdditionalAnswer[j] == currentDialogId)
                        {
                            return additionalAnswers[i].Answer;
                        }
                    }
                }
            }
            return null;
        }
    }
}
