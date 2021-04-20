using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIWeaponItemData", menuName = "CreateData/HubMapUIData/Items/Weapon", order = 0)]
    public class HubMapUIWeaponItemData : HubMapUIBaseItemData
    {
        #region Fields

        [SerializeField] private bool _isTwoHanded;

        #endregion


        #region Properties

        public bool IsTwoHanded => _isTwoHanded;
        public override HubMapUIItemType ItemType { get; protected set; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            ItemType = HubMapUIItemType.Weapon;
        }

        #endregion
    }
}
