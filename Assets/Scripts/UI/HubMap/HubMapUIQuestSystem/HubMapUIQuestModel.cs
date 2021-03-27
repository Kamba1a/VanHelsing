using System;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIQuestModel
    {
        #region Properties

        public HubMapUIQuestData Data { get; private set; }
        public HubMapUIQuestStatus Status { get; set; }
        public HubMapUIQuestTaskData CurrentTask { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIQuestModel(HubMapUIQuestData data, HubMapUIQuestStatus status)
        {
            Data = data;
            Status = status;
            CurrentTask = Array.Find(data.Tasks, task => task.Id == data.FirstTaskId);
        }

        #endregion


        #region Methods

        public HubMapUICitizenData GetCurrentTargetCitizen()
        {
            return GetTargetCitizenSettings().Citizen;
        }

        public HubMapUIQuestCitizenDialogSettings GetTargetCitizenSettings()
        {
            for (int i = 0; i < CurrentTask.CitizenDialogSettings.Length; i++)
            {
                if (CurrentTask.CitizenDialogSettings[i].IsTarget)
                {
                    return CurrentTask.CitizenDialogSettings[i];
                }
            }
            Debug.LogError(this + ": no target citizen. Quest title: " + this.Data.Title + " Task id: " + CurrentTask.Id);
            return null;
        }

        public bool IsRequirementQuestComleted(HubMapUIQuestModel requirementQuest)
        {
            if (HasQuestCompleteRequirement(requirementQuest.Data))
            {
                return requirementQuest.Status == HubMapUIQuestStatus.Completed;
            }
            return true;
        }

        public bool IsEnoughCityReputation(HubMapUICityModel city)
        {
            if (HasCityRequirement(city.DataInstanceID))
            {
                return Data.RequiredReputation.Reputation <= city.PlayerReputation;
            }
            return true;
        }

        public void NextTask()
        {
            HubMapUIQuestTaskData nextTask = Array.Find(Data.Tasks, task => task.Id == CurrentTask.NextQuestTaskId);
            CurrentTask = nextTask;
        }

        public bool IsLastTaskComplete()
        {
            return CurrentTask.Id == Data.EmptyEndTaskId;
        }

        private bool HasQuestCompleteRequirement(HubMapUIQuestData questData)
        {
            if (Data.RequiredQuest != null)
            {
                return Data.RequiredQuest == questData;
            }
            return false;
        }

        private bool HasCityRequirement(int cityDataInstanceID)
        {
            if (Data.RequiredReputation.City != null)
            {
                return Data.RequiredReputation.City.GetInstanceID() == cityDataInstanceID;
            }
            return false;
        }

        #endregion
    }
}
