using System;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuest : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private HubMapUIQuestTask[] _tasks;
        [SerializeField] private int _firstTaskId;
        [SerializeField] private int _emptyEndTaskId;
        [SerializeField] private HubMapUICityReputation _requiredReputation;
        [SerializeField] private HubMapUIQuest _requiredQuest;

        public string Title => _title;
        public int FirstTaskId => _firstTaskId;
        public int EmptyEndTaskId => _emptyEndTaskId;
        public HubMapUICityReputation RequiredReputation => _requiredReputation;
        public HubMapUIQuest RequiredQuest => _requiredQuest;
        public HubMapUIQuestTask FirstTask { get; private set; }

        private void OnEnable()
        {
            FirstTask = Array.Find(_tasks, task=> task.Id == _firstTaskId);
        }

        public HubMapUIQuestTask[] GetTasks()
        {
            return (HubMapUIQuestTask[])_tasks.Clone();
        }
    }
}
