namespace BeastHunter
{
    public class HubMapUIWeaponItemModel : HubMapUIBaseItemModel
    {
        public bool IsTwoHanded { get; private set; }

        public HubMapUIWeaponItemModel(HubMapUIWeaponItemData data) : base(data)
        {
            IsTwoHanded = data.IsTwoHanded;
        }
    }
}
