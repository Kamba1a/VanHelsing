using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuestsController
    {
        private Dictionary<HubMapUIQuest, HubMapUIQuestTask> _currentQuestTask;
        private List<HubMapUIQuest> _completedQuests;


        public HubMapUIQuestsController()
        {
            _completedQuests = new List<HubMapUIQuest>();
            _currentQuestTask = new Dictionary<HubMapUIQuest, HubMapUIQuestTask>();
            //add quests from quest storage?
        }

        public HubMapUIQuestMarkerType GetQuestMarker(HubMapUICitizen citizen)
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestTask> kvp in _currentQuestTask)
            {
                if (kvp.Value.TargetCitizen == citizen)
                {
                    if (kvp.Key.FirstTaskId == kvp.Value.Id)
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

        public bool CheckTargetCitizen(HubMapUICitizen citizen)
        {
            foreach (KeyValuePair<HubMapUIQuest, HubMapUIQuestTask> kvp in _currentQuestTask)
            {
                return kvp.Value.TargetCitizen == citizen;
            }
            return false;
        }

        public void AddQuest(HubMapUIQuest quest)
        {
            if (!_currentQuestTask.ContainsKey(quest))
            {
                _currentQuestTask.Add(quest, quest.FirstTask);
            }
        }

        public HubMapUIQuestTask GetCurrentTaskId(HubMapUIQuest quest)
        {
            if (_currentQuestTask.ContainsKey(quest))
            {
                return _currentQuestTask[quest];
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested quest");
            }
        }

        public void SetNextTaskId(HubMapUIQuest quest, HubMapUIQuestTask task)
        {
            if (_currentQuestTask.ContainsKey(quest))
            {
                _currentQuestTask[quest] = task;
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested quest");
            }
        }

        private void QuestComplete(HubMapUIQuest quest)
        {
            _completedQuests.Add(quest);
            Debug.Log(quest.Name + "is completed");
            //reward, etc?
        }
    }
}
