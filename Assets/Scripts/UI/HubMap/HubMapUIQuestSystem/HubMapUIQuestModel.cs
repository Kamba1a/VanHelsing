namespace BeastHunter
{
    public class HubMapUIQuestModel
    {
        public HubMapUIQuestData Data { get; set; }
        public HubMapUIQuestStatus Status { get; set; }
        public HubMapUIQuestTask CurrentTask { get; set; }


        public HubMapUIQuestModel(HubMapUIQuestData data, HubMapUIQuestStatus status)
        {
            Data = data;
            Status = status;
            CurrentTask = data.FirstTask;
        }
    }
}
