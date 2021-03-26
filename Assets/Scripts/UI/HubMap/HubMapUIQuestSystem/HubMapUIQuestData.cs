using System;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIQuestData : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private HubMapUIQuestTaskData[] _tasks;
        [SerializeField] private int _firstTaskId;
        [SerializeField] private int _emptyEndTaskId;
        [SerializeField] private HubMapUICityReputation _requiredReputation;
        [SerializeField] private HubMapUIQuestData _requiredQuest;


        public string Title => _title;
        public int FirstTaskId => _firstTaskId;
        public int EmptyEndTaskId => _emptyEndTaskId;
        public HubMapUICityReputation RequiredReputation => _requiredReputation;
        public HubMapUIQuestData RequiredQuest => _requiredQuest;
        public HubMapUIQuestTaskData[] Tasks => (HubMapUIQuestTaskData[])_tasks.Clone();


        public HubMapUIQuestTaskData FirstTask { get; private set; }


        private void OnEnable()
        {
            FirstTask = Array.Find(_tasks, task=> task.Id == _firstTaskId);
        }
    }
}
