namespace BeastHunter
{
    public class HubMapUIWeaponItemModel : HubMapUIBaseItemModel
    {
        #region Properties

        public bool IsTwoHanded { get; private set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIWeaponItemModel(HubMapUIWeaponItemData data) : base(data)
        {
            IsTwoHanded = data.IsTwoHanded;
        }

        #endregion
    }
}
