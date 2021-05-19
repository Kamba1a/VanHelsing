using System.Collections.Generic;


namespace BeastHunterHubUI
{
    public class WorkRoomModel : BaseWorkRoomModel<WorkRoomProgress>
    {
        public RecipesStorage RecipesSlots { get; private set; }
        public ItemLimitedStorage FinishedItems { get; private set; }
        public override Dictionary<int, WorkRoomProgress> ProgressScheme { get; protected set; }


        public WorkRoomModel(WorkRoomStruct roomStruct) : base(roomStruct.BaseWorkRoomStruct)
        {
            FinishedItems = new ItemLimitedStorage(ProgressScheme[Level].RecipesSlots, ItemStorageType.WorkRoomFinishedItems);
            RecipesSlots = new RecipesStorage(ProgressScheme[Level].RecipesSlots);
        }
    }
}
