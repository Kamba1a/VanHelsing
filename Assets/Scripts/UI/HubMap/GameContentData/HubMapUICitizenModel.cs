using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUICitizenModel
    {

        public int DataInstanceId { get; private set; }
        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }
        public int CurrentDialogId { get; set; }
        //public HubMapUIQuestMarkerType QuestMarkerType { get; set; }
        public string CurrentSpeechText { get; set; }
        public List<HubMapUIDialogAnswer> CurrentExpectedResponses { get; private set; }


        public HubMapUICitizenModel(HubMapUICitizenData data)
        {
            DataInstanceId = data.GetInstanceID();
            Name = data.Name;
            Portrait = data.Portrait;
            CurrentDialogId = data.FirstDialogId;
            //QuestMarkerType = HubMapUIQuestMarkerType.None;
            CurrentExpectedResponses = new List<HubMapUIDialogAnswer>();
        }
    }
}
