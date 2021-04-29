using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BeastHunterHubUI
{
    public class CharacterRandomizeService
    {
        private const float MALE_GENDER_CHANCE = 0.5f;

        //public CharacterModel GetRandomCharacter()
        //{
        //    return new CharacterModel(new CharacterData(), new CharactersSettingsStruct()); //temporary
        //}

        private bool IsMale()
        {
            return UnityEngine.Random.Range(0, 101) <= MALE_GENDER_CHANCE * 100;
        }

        /* Data:
        [SerializeField] private Sprite _portrait; //RUNTIME
        [SerializeField] private BaseItemData[] _startBackpuckItems; //RUNTIME from pool
        [SerializeField] private ClothesItemData[] _startClothesEquipmentItems; //RUNTIME from pool
        [SerializeField] private WeaponItemData[] _startWeaponEquipmentItems; //RUNTIME from pool
        [SerializeField] private Material _defaultMaterial; //RUNTIME from pool
        [SerializeField] private CharacterClothesModuleParts[] _defaultModuleParts; //RUNTIME
        [SerializeField] private CharacterHeadPart[] _defaultHeadParts; //RUNTIME
        */
    }
}
