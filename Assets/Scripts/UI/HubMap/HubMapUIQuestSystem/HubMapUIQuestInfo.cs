using System;

namespace BeastHunter
{
    public class HubMapUIQuestInfo
    {
        private HubMapUIQuestStatus _questStatus;
        private HubMapUIQuestTask _currentTask;


        public HubMapUIQuestStatus QuestStatus => _questStatus;
        public HubMapUIQuestTask CurrentTask => _currentTask;


        public HubMapUIQuestInfo(HubMapUIQuestStatus questStatus, HubMapUIQuestTask firstTask)
        {
            _questStatus = questStatus;
            _currentTask = firstTask;
        }


        public void SetQuestStatus(HubMapUIQuestStatus newQuestStatus)
        {
            if (_questStatus != newQuestStatus)
            {
                _questStatus = newQuestStatus;
            }
        }

        public void SetCurrentTask(HubMapUIQuestTask nextTask)
        {
            if (_currentTask != nextTask)
            {
                _currentTask = nextTask;
            }
        }
    }
}
