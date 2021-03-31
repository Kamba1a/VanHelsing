using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIContextData", menuName = "CreateData/HubMapUIData/HubMapUIContextData", order = 0)]
    public class HubMapUIContextData : ScriptableObject
    {
        #region Fields

        [SerializeField] private int _shopsSlotsAmount;
        [SerializeField] private int _charactersInventorySlotAmount;
        [SerializeField] private HubMapUIPlayerData _player;
        [SerializeField] private HubMapUICharacterData[] _characters;
        [SerializeField] private HubMapUICityData[] _cities;
        [SerializeField] private HubMapUILocationData[] _locations;
        [SerializeField] private HubMapUIQuestData[] _quests;

        #endregion


        #region Properties

        public int ShopsSlotsAmount => _shopsSlotsAmount;
        public int CharactersInventorySlotAmount => _charactersInventorySlotAmount;
        public HubMapUIPlayerData Player => _player;
        public HubMapUICharacterData[] Characters => (HubMapUICharacterData[])_characters.Clone();
        public HubMapUICityData[] Cities => (HubMapUICityData[])_cities.Clone();
        public HubMapUILocationData[] Locations => (HubMapUILocationData[])_locations.Clone();
        public HubMapUIQuestData[] Quests => (HubMapUIQuestData[])_quests.Clone();

        #endregion
    }
}
