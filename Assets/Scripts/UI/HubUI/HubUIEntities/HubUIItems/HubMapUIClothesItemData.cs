using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIClothItemData", menuName = "CreateData/HubMapUIData/Items/Cloth", order = 0)]
    public class HubMapUIClothesItemData : HubMapUIBaseItemData
    {
        #region Fields

        [SerializeField] private HubMapUIClothesType _clothType;
        [SerializeField] private int _pocketsAmount;
        [Tooltip("Sure to use fantasy hero material shader (SyntyStudios/CustomCharacter)")]
        [SerializeField] private Material _fantasyHeroMaterial;
        [SerializeField] private string[] _clothesPartsNamesAllGender;
        [SerializeField] private string[] _clothesPartsNamesMale;
        [SerializeField] private string[] _clothesPartsNamesFemale;
        [SerializeField] private HubMapUICharacterHeadParts[] _disabledHeadParts;

        #endregion


        #region Properties

        public HubMapUIClothesType ClothesType => _clothType;
        public int PocketsAmount => _pocketsAmount;
        public Material FantasyHeroMaterial => _fantasyHeroMaterial;
        public string[] ClothesPartsNamesAllGender => (string[])_clothesPartsNamesAllGender?.Clone();
        public string[] ClothesPartsNamesMale => (string[])_clothesPartsNamesMale?.Clone();
        public string[] ClothesPartsNamesFemale => (string[])_clothesPartsNamesFemale?.Clone();
        public HubMapUICharacterHeadParts[] DisabledHeadParts => (HubMapUICharacterHeadParts[])_disabledHeadParts?.Clone();
        public override HubMapUIItemType ItemType { get; protected set; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            ItemType = HubMapUIItemType.Cloth;
        }

        #endregion
    }
}
