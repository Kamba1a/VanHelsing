using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastHunter
{
    /// <summary>TEMPORARY CLASS. It is stub class for example of map filling</summary>
    public class TemporaryDataForHubMap
    {
        public Dictionary<string, TemporaryCityModel> CitiesDic { get; private set; }
        public Dictionary<string, TemporaryCitizenModel> QuestGiversDic { get; private set; }

        public TemporaryDataForHubMap()
        {
            CreateQuestGiversData();
            CreateCitiesData();
        }

        private void CreateQuestGiversData()
        {
            QuestGiversDic = new Dictionary<string, TemporaryCitizenModel>();

            QuestGiversDic.Add("1", new TemporaryCitizenModel()
            {
                Name = "Трисс Меригольд",
                IsHaveQuest = false,
                QuestDescription = "AAA"
            });

            QuestGiversDic.Add("2", new TemporaryCitizenModel()
            {
                Name = "Голан Вивальди",
                IsHaveQuest = true,
                QuestDescription = "описание квеста описание квеста описание квеста описание квеста описание квеста описание квеста описание квеста"
            });

            QuestGiversDic.Add("3", new TemporaryCitizenModel()
            {
                Name = "AAA",
                IsHaveQuest = false,
                QuestDescription = "AAA"
            });

            QuestGiversDic.Add("4", new TemporaryCitizenModel()
            {
                Name = "Шани",
                IsHaveQuest = false,
                QuestDescription = "AAA"
            });
        }

        private void CreateCitiesData()
        {
            CitiesDic = new Dictionary<string, TemporaryCityModel>();

            CitiesDic.Add("Wyzima", new TemporaryCityModel()
            {
                Name = "Вызима",
                Description = "Город богат и очень важен для королевства. За свою долгую историю поселение было свидетелем многих событий, как радостных, так и трагических. Вызима прекрасное место для развития науки. Город стоит на пересечении множества торговых путей и является основной экономической областью Темерии. Здесь также располагаются крупные формирования темерской армии. В столице рассматриваются важные государственные вопросы. ",
                QuestGivers = new List<TemporaryCitizenModel>()
                {
                    QuestGiversDic["1"],
                    QuestGiversDic["2"],
                    QuestGiversDic["4"]
                }
            });
        }
    }
}
