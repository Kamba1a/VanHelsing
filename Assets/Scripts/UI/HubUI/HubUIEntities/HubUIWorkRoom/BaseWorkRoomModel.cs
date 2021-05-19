using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseWorkRoomModel<T> where T : BaseWorkRoomProgress
    {
        public WorkRoomType RoomType { get; private set; }
        public int Level { get; private set; }
        public CharacterLimitedStorage ChiefWorkplace { get; private set; }
        public CharacterLimitedStorage AssistantWorkplaces { get; private set; }
        public abstract Dictionary<int,T> ProgressScheme { get; protected set; }


        public BaseWorkRoomModel(BaseWorkRoomStruct<T> roomStruct)
        {
            RoomType = roomStruct.RoomType;
            Level = roomStruct.Level;

            ChiefWorkplace = new CharacterLimitedStorage(1, CharacterStorageType.ChiefWorkplace);
            if (roomStruct.ChiefWorker != null)
            {
                ChiefWorkplace.PutElementToFirstEmptySlot(roomStruct.ChiefWorker);
            }

            ProgressScheme = new Dictionary<int, T>();
            for (int i = 0; i < roomStruct.ProgressScheme.Count; i++)
            {
                if (!ProgressScheme.ContainsKey(roomStruct.ProgressScheme[i].Level))
                {
                    ProgressScheme.Add(roomStruct.ProgressScheme[i].Level, roomStruct.ProgressScheme[i].Progress);
                }
                else
                {
                    Debug.LogError("Incorrect input parameter: progress scheme list contain two identical values of level number");
                }
            }

            AssistantWorkplaces = new CharacterLimitedStorage(ProgressScheme[Level].AssistansAmount, CharacterStorageType.AssistantWorkplaces);

            for (int i = 0; i < roomStruct.Assistants.Count; i++)
            {
                if (!AssistantWorkplaces.PutElementToFirstEmptySlot(roomStruct.Assistants[i]))
                {
                    Debug.LogError("Incorrect input parameter: assistans amount more than the number of slots");
                    break;
                }
            }
        }
    }
}
