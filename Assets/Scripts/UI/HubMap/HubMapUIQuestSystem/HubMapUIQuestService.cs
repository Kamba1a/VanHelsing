using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuestService
    {
        private enum RequirementCheckType
        {
            None = 0,
            CityReputation = 1,
            QuestComplete = 2,
        }

        #region Fields

        private HubMapUIWorldModel _world;
        private List<HubMapUIDialogNode> _dialogs;
        private List<HubMapUIQuestModel> _notStartedQuests;
        private List<HubMapUIQuestModel> _activeQuests;
        private List<HubMapUIQuestModel> _completedQuests;

        public Action<HubMapUIQuestModel> OnQuestStartHandler { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIQuestService(IEnumerable<HubMapUIQuestData> quests)
        {
            _world = Data.HubMapData.World;
            _dialogs = Data.HubMapData.Dialogs;

            _notStartedQuests = new List<HubMapUIQuestModel>();
            _activeQuests = new List<HubMapUIQuestModel>();
            _completedQuests = new List<HubMapUIQuestModel>();

            if (quests != null)
            {
                foreach (HubMapUIQuestData questData in quests)
                {
                    _notStartedQuests.Add(new HubMapUIQuestModel(questData, HubMapUIQuestStatus.NotStarted));
                }

                for (int i = 0; i < _notStartedQuests.Count; i++)
                {
                    if (CheckQuestForAllRequirements(_notStartedQuests[i], RequirementCheckType.QuestComplete))
                    {
                        QuestStart(_notStartedQuests[i]);
                    }
                }
            }

            _world.OnChangePlayerReputationHandler += OnChangePlayerReputation;
        }

        #endregion


        #region Methods

        public void SetNewDialog(HubMapUICitizenModel citizen, int newDialogId)
        {
            HubMapUIDialogNode dialog = _dialogs.Find(dialog => dialog.Id == newDialogId);

            citizen.CurrentDialogId = newDialogId;
            citizen.CurrentSpeechText = dialog.Text;
            citizen.CurrentExpectedResponses.Clear();
            for (int i = 0; i < dialog.Answers.Length; i++)
            {
                citizen.CurrentExpectedResponses.Add(dialog.Answers[i]);
            }

            HubMapUIDialogAnswer additionalQuestAnswer = GetAdditionalQuestAnswer(newDialogId);
            if (additionalQuestAnswer != null)
            {
                citizen.CurrentExpectedResponses.Add(additionalQuestAnswer);
            }
        }

        public void QuestProgressed(HubMapUIQuestData questData, HubMapUICitizenModel citizen)
        {
            HubMapUIQuestModel quest = _activeQuests.Find(q => q.Data == questData);
            if (quest != null)
            {
                if (quest.CurrentTask.TargetCitizen.GetInstanceID() == citizen.DataInstanceId)
                {
                    quest.NextTask();

                    if (quest.IsLastTaskComplete())
                    {
                        QuestComplete(quest);
                    }
                }
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested quest");
            }
        }

        public HubMapUIQuestMarkerType GetQuestMarker(HubMapUICitizenModel citizen)
        {
            for (int i = 0; i < _activeQuests.Count; i++)
            {
                if (_activeQuests[i].CurrentTask.TargetCitizen.GetInstanceID() == citizen.DataInstanceId)
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

        private void QuestStart(HubMapUIQuestModel quest)
        {
            if (quest.Status == HubMapUIQuestStatus.NotStarted)
            {
                    quest.Status = HubMapUIQuestStatus.Active;

                    _activeQuests.Add(quest);
                    _notStartedQuests.Remove(quest);

                    OnQuestStart(quest);
                    Debug.Log("Change quest status on active");
            }
        }

        private void QuestComplete(HubMapUIQuestModel quest)
        {
            if (quest.Status == HubMapUIQuestStatus.Active)
            {
                quest.Status = HubMapUIQuestStatus.Completed;

                _activeQuests.Remove(quest);
                _completedQuests.Add(quest);

                OnQuestComplete(quest);
                Debug.Log("Change quest status on completed");
            }
        }

        private bool CheckQuestForAllRequirements(HubMapUIQuestModel quest, RequirementCheckType excludeCheckType = RequirementCheckType.None)
        {
            if (excludeCheckType != RequirementCheckType.CityReputation)
            {
                HubMapUICityData cityData = quest.Data.RequiredReputation.City;
                bool checkReputationRequirement = quest.IsEnoughCityReputation(_world.GetCity(cityData));

                if (!checkReputationRequirement)
                {
                    return false;
                }
            }

            if (excludeCheckType != RequirementCheckType.QuestComplete)
            {
                HubMapUIQuestData questData = quest.Data.RequiredQuest;
                bool checkQuestRequirement = quest.IsRequirementQuestComleted(GetQuestModel(questData));

                if (!checkQuestRequirement)
                {
                    return false;
                }
            }

            return true;
        }

        private HubMapUIQuestModel GetQuestModel(HubMapUIQuestData questData)
        {
            HubMapUIQuestModel result;
            result = _activeQuests.Find(quest => quest.Data == questData);
            if (result != null)
            {
                return result;
            }

            result = _notStartedQuests.Find(quest => quest.Data == questData);
            if (result != null)
            {
                return result;
            }

            result = _completedQuests.Find(quest => quest.Data == questData);
            return result;
        }

        private void OnChangePlayerReputation(HubMapUICityModel city)
        {
            for (int i = 0; i < _notStartedQuests.Count; i++)
            {
                if (_notStartedQuests[i].IsEnoughCityReputation(city))
                {
                    if (CheckQuestForAllRequirements(_notStartedQuests[i], RequirementCheckType.CityReputation))
                    {
                        QuestStart(_notStartedQuests[i]);
                    }
                }
            }
        }

        private void OnQuestComplete(HubMapUIQuestModel quest)
        {
            for (int i = 0; i < _notStartedQuests.Count; i++)
            {
                if (_notStartedQuests[i].IsRequirementQuestComleted(quest))
                {
                    if (CheckQuestForAllRequirements(_notStartedQuests[i], RequirementCheckType.QuestComplete))
                    {
                        QuestStart(_notStartedQuests[i]);
                    }
                }
            }
        }

        private void OnQuestStart(HubMapUIQuestModel quest)
        {
            OnQuestStartHandler?.Invoke(quest);
            SetQuestDialogToCitizen(quest);
        }

        private void SetQuestDialogToCitizen(HubMapUIQuestModel quest)
        {
            if (quest.CurrentTask.IsNpcInitiatesDialogue)
            {
                SetNewDialog(_world.GetCitizen(quest.CurrentTask.TargetCitizen), quest.CurrentTask.InitiatedDialogueId);
            }
        }

        #endregion
    }
}
