namespace BeastHunter
{
    public class HubMapUIQuestModel
    {
        public HubMapUIQuestData Data { get; set; }
        public HubMapUIQuestStatus Status { get; set; }
        public HubMapUIQuestTaskData CurrentTask { get; set; }


        public HubMapUIQuestModel(HubMapUIQuestData data, HubMapUIQuestStatus status)
        {
            Data = data;
            Status = status;
            CurrentTask = data.FirstTask;
        }

        public bool HasQuestCompleteRequirement(HubMapUIQuestData questData)
        {
            if (Data.RequiredQuest != null)
            {
                return Data.RequiredQuest == questData;
            }
            return false;
        }

        public bool IsEnoughCityReputation(HubMapUICityModel city)
        {
            if (HasCityRequirement(city.DataInstanceID))
            {
                return Data.RequiredReputation.Reputation <= city.PlayerReputation;
            }
            return true;
        }

        public bool HasCityRequirement(int cityDataInstanceID)
        {
            if (Data.RequiredReputation.City != null)
            {
                return Data.RequiredReputation.City.GetInstanceID() == cityDataInstanceID;
            }
            return false;
        }
    }
}
