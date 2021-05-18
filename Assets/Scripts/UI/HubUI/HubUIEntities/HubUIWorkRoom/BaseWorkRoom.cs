namespace BeastHunterHubUI
{
    public abstract class BaseWorkRoom
    {
        private const int ASSISTENTS_AMOUNT = 3;

        public WorkRoomType RoomType { get; private set; }
        public CharacterLimitedStorage ChiefWorkplace { get; private set; }
        public CharacterLimitedStorage AssistantWorkplaces { get; private set; }


        public BaseWorkRoom(WorkRoomType roomType)
        {
            RoomType = roomType;
            ChiefWorkplace = new CharacterLimitedStorage(1, CharacterStorageType.ChiefWorkplace);
            AssistantWorkplaces = new CharacterLimitedStorage(ASSISTENTS_AMOUNT, CharacterStorageType.AssistantWorkplaces);
        }
    }
}
