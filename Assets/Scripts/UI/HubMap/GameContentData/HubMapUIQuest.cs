using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIQuest", menuName = "CreateData/HubMapUIData/HubMapUIQuest", order = 0)]
    public class HubMapUIQuest : ScriptableObject
    {
        [SerializeField] private HubMapUIQuestTask[] _tasks;
        private int _firstTaskId;
        private int _endTaskId;
        private bool _isCompleted = false;

        public bool IsCompleted 
        {
            get { return _isCompleted; }
            private set { _isCompleted = value; }
        }

        public int FirstTaskId => _firstTaskId;

        private void QuestComplete()
        {
            _isCompleted = true;
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
        private bool _isCompleted = false;

        public bool IsCompleted
        {
            get { return _isCompleted; }
            private set
            { 
                if (value)
                {

                }
                _isCompleted = value;
            }
        }
    }

    public class HubMapUIQuestController
    {
        private Dictionary<HubMapUIQuest, int> _currentQuestTask;


        public HubMapUIQuestController()
        {
            _currentQuestTask = new Dictionary<HubMapUIQuest, int>();
        }

        public void AddQuest(HubMapUIQuest quest)
        {
            if (!_currentQuestTask.ContainsKey(quest))
            {
                _currentQuestTask.Add(quest, quest.FirstTaskId);
            }
        }

        public int GetCurrentTaskId(HubMapUIQuest quest)
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

        public void SetNewTaskId(HubMapUIQuest quest, int id)
        {
            if (_currentQuestTask.ContainsKey(quest))
            {
                _currentQuestTask[quest] = id;
            }
            else
            {
                throw new System.Exception(this + " does not contain the requested quest");
            }
        }
    }
}
