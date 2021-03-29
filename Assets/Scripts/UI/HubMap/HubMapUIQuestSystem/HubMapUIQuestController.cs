﻿using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuestController
    {
        #region PrivateData

        private enum RequirementCheckType
        {
            None = 0,
            CityReputation = 1,
            QuestComplete = 2,
        }

        #endregion


        #region Fields

        private HubMapUIContext _context;
        private List<HubMapUIQuestModel> _notActiveQuests;
        private List<HubMapUIQuestModel> _activeQuests;
        private List<HubMapUIQuestModel> _completedQuests;

        #endregion


        #region ClassLifeCycle

        public HubMapUIQuestController(HubMapUIContext context)
        {
            _context = context;

            _notActiveQuests = new List<HubMapUIQuestModel>();
            _activeQuests = new List<HubMapUIQuestModel>();
            _completedQuests = new List<HubMapUIQuestModel>();

            for (int i = 0; i < _context.QuestsData.Length; i++)
            {
                _notActiveQuests.Add(new HubMapUIQuestModel(_context.QuestsData[i], HubMapUIQuestStatus.NotActive));
            }

            for (int i = 0; i < _notActiveQuests.Count; i++)
            {
                if (CheckQuestForAllRequirements(_notActiveQuests[i], RequirementCheckType.QuestComplete))
                {
                    QuestActivate(_notActiveQuests[i]);
                }
            }

            for (int i = 0; i < _context.Cities.Count; i++)
            {
                _context.Cities[i].OnChangePlayerReputationHandler += OnChangePlayerReputation;
            }
        }

        #endregion


        #region Methods

        private void OnChangePlayerReputation(HubMapUICityModel city)
        {
            for (int i = 0; i < _notActiveQuests.Count; i++)
            {
                if (_notActiveQuests[i].IsEnoughCityReputation(city))
                {
                    if (CheckQuestForAllRequirements(_notActiveQuests[i], RequirementCheckType.CityReputation))
                    {
                        QuestActivate(_notActiveQuests[i]);
                    }
                }
            }
        }

        private HubMapUIQuestModel GetQuestModel(HubMapUIQuestData questData)
        {
            HubMapUIQuestModel result;
            result = _activeQuests.Find(quest => quest.Data == questData);
            if (result != null)
            {
                return result;
            }

            result = _notActiveQuests.Find(quest => quest.Data == questData);
            if (result != null)
            {
                return result;
            }

            result = _completedQuests.Find(quest => quest.Data == questData);
            return result;
        }

        private void QuestActivate(HubMapUIQuestModel quest)
        {
            if (quest.Status == HubMapUIQuestStatus.NotActive)
            {
                    quest.Status = HubMapUIQuestStatus.Active;

                    _activeQuests.Add(quest);
                    _notActiveQuests.Remove(quest);

                    OnQuestActivated(quest);
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

        private void QuestProgressing(HubMapUIQuestModel quest)
        {
            DeactivateQuestAnswers(quest);
            SetMarkerTypeToCitizen(quest, HubMapUIQuestMarkerType.None);
            quest.NextTask();

            if (quest.IsLastTaskComplete())
            {
                QuestComplete(quest);
            }
            else
            {
                SetQuestDialogToCitizen(quest);
                ActivateQuestAnswers(quest);
                SetMarkerTypeToCitizen(quest, HubMapUIQuestMarkerType.Question);
            }
        }

        private bool CheckQuestForAllRequirements(HubMapUIQuestModel quest, RequirementCheckType excludeCheckType = RequirementCheckType.None)
        {
            if (excludeCheckType != RequirementCheckType.CityReputation)
            {
                HubMapUICityData cityData = quest.Data.RequiredReputation.City;
                bool checkReputationRequirement = quest.IsEnoughCityReputation(_context.GetCity(cityData));

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

        private void OnQuestActivated(HubMapUIQuestModel quest)
        {
            SetQuestDialogToCitizen(quest);
            ActivateQuestAnswers(quest);
            SetMarkerTypeToCitizen(quest, HubMapUIQuestMarkerType.Exclamation);
        }

        private void OnQuestComplete(HubMapUIQuestModel quest)
        {
            for (int i = 0; i < _notActiveQuests.Count; i++)
            {
                if (_notActiveQuests[i].IsRequirementQuestComleted(quest))
                {
                    if (CheckQuestForAllRequirements(_notActiveQuests[i], RequirementCheckType.QuestComplete))
                    {
                        QuestActivate(_notActiveQuests[i]);
                    }
                }
            }
        }

        private void SetQuestDialogToCitizen(HubMapUIQuestModel quest)
        {
            if (quest.GetTargetCitizenSettings().IsNpcInitiateDialog)
            {
                _context.GetCitizen(quest.GetCurrentTargetCitizen()).
                    SetCurrentDialogNode(quest.GetTargetCitizenSettings().InitiatedDialogId);
            }
        }

        private void ActivateQuestAnswers(HubMapUIQuestModel quest)
        {
            if (quest.CurrentTask.CitizenDialogSettings.Length > 0)
            {
                for (int i = 0; i < quest.CurrentTask.CitizenDialogSettings.Length; i++)
                {
                    HubMapUIQuestAnswer questAnswer = _context.GetCitizen(quest.CurrentTask.CitizenDialogSettings[i].Citizen).
                        GetQuestAnswerById(quest.CurrentTask.CitizenDialogSettings[i].QuestAnswerId);

                    if (questAnswer != null)
                    {
                        questAnswer.IsActive = true;
                    }
                    else
                    {
                        Debug.LogError(quest.CurrentTask.CitizenDialogSettings[i].Citizen.Name + " does not have required quest answer id " + quest.CurrentTask.CitizenDialogSettings[i].QuestAnswerId);
                    }

                    if (quest.CurrentTask.CitizenDialogSettings[i].IsTarget)
                    {
                        questAnswer.Answer.OnAnswerSelectByPlayerHandler += (handlerValue) => QuestProgressing(quest);
                    }
                }
            }
        }

        private void DeactivateQuestAnswers(HubMapUIQuestModel quest)
        {
            if (quest.CurrentTask.CitizenDialogSettings.Length > 0)
            {
                for (int i = 0; i < quest.CurrentTask.CitizenDialogSettings.Length; i++)
                {
                    HubMapUIQuestAnswer questAnswer = _context.GetCitizen(quest.CurrentTask.CitizenDialogSettings[i].Citizen).
                        GetQuestAnswerById(quest.CurrentTask.CitizenDialogSettings[i].QuestAnswerId);

                    questAnswer.IsActive = false;

                    if (quest.CurrentTask.CitizenDialogSettings[i].IsTarget)
                    {
                        questAnswer.Answer.OnAnswerSelectByPlayerHandler -= (handlerValue) => QuestProgressing(quest);
                    }
                }
            }
        }

        private void SetMarkerTypeToCitizen(HubMapUIQuestModel quest, HubMapUIQuestMarkerType questMarker)
        {
            _context.GetCitizen(quest.GetCurrentTargetCitizen()).QuestMarkerType = questMarker;
        }

        #endregion
    }
}
