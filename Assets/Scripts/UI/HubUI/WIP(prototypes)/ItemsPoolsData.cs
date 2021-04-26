using UnityEngine;


namespace BeastHunterHubUI
{
    public class ItemsPoolsData : ScriptableObject
    {
        [SerializeField] private ClothesItemData[] _clothesItemsPool;
        [SerializeField] private WeaponItemData[] _weaponItemsPool;
        [SerializeField] private PocketItemData[] _pocketItemsPool;


        public ClothesItemData[] ClothesItemsPool => _clothesItemsPool;
        public WeaponItemData[] WeaponItemsPool => _weaponItemsPool;
        public PocketItemData[] PocketItemsPool => _pocketItemsPool;
    }
}
