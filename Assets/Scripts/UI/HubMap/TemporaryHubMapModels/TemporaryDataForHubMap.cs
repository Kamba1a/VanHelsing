using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    /// <summary>TEMPORARY CLASS. It is stub class for example of map filling</summary>
    public class TemporaryDataForHubMap
    {
        public Dictionary<string, ICityInfo> CitiesDic { get; private set; }
        public Dictionary<string, ICitizenInfo> QuestGiversDic { get; private set; }

        public TemporaryDataForHubMap()
        {
            CreateQuestGiversData();
            CreateCitiesData();
        }

        private void CreateQuestGiversData()
        {
            QuestGiversDic = new Dictionary<string, ICitizenInfo>();

            QuestGiversDic.Add("1", new TemporaryCitizenInfoModel(
                "Трисс Меригольд",
                null,
                false,
                "AAA"
                ));

            QuestGiversDic.Add("2", new TemporaryCitizenInfoModel(
                "Голан Вивальди",
                null,
                true,
                "описание квеста описание квеста описание квеста описание квеста описание квеста описание квеста описание квеста"
                ));

            QuestGiversDic.Add("3", new TemporaryCitizenInfoModel(
                "AAA",
                null,
                false,
                "AAA"
                ));

            QuestGiversDic.Add("4", new TemporaryCitizenInfoModel(
                "Шани",
                null,
                false,
                "AAA"
                ));
        }

        private void CreateCitiesData()
        {
            CitiesDic = new Dictionary<string, ICityInfo>();

            CitiesDic.Add("Wyzima", new TemporaryCityInfoModel(
                "Вызима",
                "Город богат и очень важен для королевства. За свою долгую историю поселение было свидетелем многих событий, как радостных, так и трагических. Вызима прекрасное место для развития науки. Город стоит на пересечении множества торговых путей и является основной экономической областью Темерии. Здесь также располагаются крупные формирования темерской армии. В столице рассматриваются важные государственные вопросы. ",
                null,
                new List<ICitizenInfo>()
                {
                    QuestGiversDic["1"],
                    QuestGiversDic["2"],
                    QuestGiversDic["4"]
                },
                20
                ));
        }
    }
}
