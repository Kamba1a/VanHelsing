using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIGameContentData", menuName = "CreateData/HubMapUIData/HubMapUIGameContentData", order = 0)]
    public class HubMapUIGameContentData : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _shopsSlotsAmount;
        [SerializeField] private HubMapUIPlayerData _player;
        [SerializeField] private int _charactersInventorySlotAmount;
        [SerializeField] private int _charactersWeaponSetsAmount;
        [SerializeField] private HubMapUIClothesType[] _charactersClothSlots;
        [SerializeField] private HubMapUICharacterData[] _characters;
        [SerializeField] private HubMapUICityData[] _cities;
        [SerializeField] private HubMapUILocationData[] _locations;
        [SerializeField] private HubMapUIQuestData[] _quests;

        #endregion


        #region Properties

        public int ShopsSlotsAmount => _shopsSlotsAmount;
        public HubMapUIPlayerData Player => _player;
        public int CharactersEquipmentSlotAmount => _charactersInventorySlotAmount;
        public int CharactersWeaponSetsAmount => _charactersWeaponSetsAmount;
        public HubMapUIClothesType[] ClothSlots => (HubMapUIClothesType[])_charactersClothSlots.Clone();
        public HubMapUICharacterData[] Characters => (HubMapUICharacterData[])_characters.Clone();
        public HubMapUICityData[] Cities => (HubMapUICityData[])_cities.Clone();
        public HubMapUILocationData[] Locations => (HubMapUILocationData[])_locations.Clone();
        public HubMapUIQuestData[] Quests => (HubMapUIQuestData[])_quests.Clone();

        #endregion
    }
}
