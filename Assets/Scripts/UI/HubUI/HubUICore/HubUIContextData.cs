using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "HubUIGameContent", menuName = "CreateData/HubUIData/HubUIGameContent", order = 0)]
    public class HubUIContextData : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _shopsSlotsAmount;
        [SerializeField] private PlayerData _player;
        [SerializeField] private int _charactersInventorySlotAmount;
        [SerializeField] private int _charactersWeaponSetsAmount;
        [SerializeField] private ClothesType[] _charactersClothSlots;
        [SerializeField] private CharacterData[] _characters;
        [SerializeField] private CityData[] _cities;
        [SerializeField] private LocationData[] _locations;
        [SerializeField] private QuestData[] _quests;

        #endregion


        #region Properties

        public int ShopsSlotsAmount => _shopsSlotsAmount;
        public PlayerData Player => _player;
        public int CharactersEquipmentSlotAmount => _charactersInventorySlotAmount;
        public int CharactersWeaponSetsAmount => _charactersWeaponSetsAmount;
        public ClothesType[] ClothSlots => (ClothesType[])_charactersClothSlots.Clone();
        public CharacterData[] Characters => (CharacterData[])_characters.Clone();
        public CityData[] Cities => (CityData[])_cities.Clone();
        public LocationData[] Locations => (LocationData[])_locations.Clone();
        public QuestData[] Quests => (QuestData[])_quests.Clone();

        #endregion
    }
}
