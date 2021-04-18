using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIWeaponItemData", menuName = "CreateData/HubMapUIData/Items/Weapon", order = 0)]
    public class HubMapUIWeaponItemData : HubMapUIBaseItemData
    {
        [SerializeField] private bool _isTwoHanded;


        public bool IsTwoHanded => _isTwoHanded;
        public override HubMapUIItemType ItemType { get; protected set; }


        private void OnEnable()
        {
            ItemType = HubMapUIItemType.Weapon;
        }
    }
}
