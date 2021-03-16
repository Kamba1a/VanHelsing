using System;
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
        public int FirstTaskId => _firstTaskId;
        public HubMapUIQuestTask FirstTask { get; private set; }

        private void OnEnable()
        {
            FirstTask = Array.Find(_tasks, task=> task.Id == _firstTaskId);
        }
    }
}
