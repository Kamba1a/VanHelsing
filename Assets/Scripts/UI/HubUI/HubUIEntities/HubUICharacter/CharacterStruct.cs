using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct CharacterStruct
    {
        public string Name;
        public Sprite Portrait;
        public bool IsFemale;
        public int Rank;
        public Material DefaultMaterial;
        public BaseItemData[] BackpackItems;
        public ClothesItemData[] ClothesEquipmentItems;
        public PocketItemData[] PocketItems;
        public WeaponItemData[] WeaponEquipmentItems;
        public CharacterClothesModuleParts[] DefaultModuleParts;
        public CharacterHeadPart[] DefaultHeadParts;
        public CharacterSkillLevel[] Skills;
    }
}
