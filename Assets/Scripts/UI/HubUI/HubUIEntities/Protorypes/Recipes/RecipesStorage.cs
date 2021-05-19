using System;


namespace BeastHunterHubUI
{
    public class RecipesStorage : BaseLimitedStorage<BaseItemRecipe, RecipesStorageType>
    {
        public RecipesStorage(int slotsAmount) : base(slotsAmount, RecipesStorageType.None) { }


        public override bool PutElement(int slotIndex, BaseItemRecipe element)
        {
            throw new NotImplementedException();
        }

        public override bool PutElementToFirstEmptySlot(BaseItemRecipe element)
        {
            throw new NotImplementedException();
        }
    }
}
