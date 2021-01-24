using System.Collections.Generic;

namespace BeastHunter
{
    public class QuestGivers
    {
        public List<QuestGiverModel> QuestGiversList;

        public QuestGivers()
        {
            QuestGiversList = new List<QuestGiverModel>();

            QuestGiversList.Add(new QuestGiverModel()
            {
                Id =1,
                Name = "AAA",
                IsHaveQuest = true,
                QuestDescription = "AAA"
            });

            QuestGiversList.Add(new QuestGiverModel()
            {
                Id = 2,
                Name = "BBB",
                IsHaveQuest = false,
                QuestDescription = "BBB"
            });
        }
    }
}
