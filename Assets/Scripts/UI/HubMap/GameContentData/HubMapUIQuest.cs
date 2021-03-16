using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIQuest", menuName = "CreateData/HubMapUIData/HubMapUIQuest", order = 0)]
    public class HubMapUIQuest : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private HubMapUIQuestTask[] _tasks;
        [SerializeField] private int _firstTaskId;
        [SerializeField] private int _endTaskId;

        public string Name => _name;
        public HubMapUIQuestTask FirstTask { get; private set; }

        private void OnEnable()
        {
            FirstTask = Array.Find(_tasks, task=> task.Id == _firstTaskId);
        }
    }

    [System.Serializable]
    public class HubMapUIQuestTask
    {
        [SerializeField] private int _id;
        //[SerializeField] private TaskType _taskType;
        [SerializeField] private HubMapUICitizen _targetCitizen;
        [SerializeField] private int _startAnswerId;
        [SerializeField] private int _endAnswerId;
        [SerializeField] private int _nextQuestTaskId;


        public int Id => _id;
        public int NextQuestTaskId => _nextQuestTaskId;
        public HubMapUICitizen TargetCitizen => _targetCitizen;
    }

    public class HubMapUIQuestController
    {
        private Dictionary<HubMapUIQuest, HubMapUIQuestTask> _currentQuestTask;
        private List<HubMapUIQuest> _completedQuests;


        public HubMapUIQuestController()
        {
            _completedQuests = new List<HubMapUIQuest>();
            _currentQuestTask = new Dictionary<HubMapUIQuest, HubMapUIQuestTask>();
            //add quests from quest storage?
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
