using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "AllCharacters", menuName = "CreateData/HubUIData/AllCharacters", order = 0)]
    public class AllCharactersData : ScriptableObject
    {
        #region Fields

        [SerializeField] private ClothesType[] _clothesSlots;
        [SerializeField] private int _backpuckSlotAmount;
        [SerializeField] private int _weaponSetsAmount;
        [SerializeField] private GameObject _modularCharactersPrefab;
        [SerializeField] private GameObject _view3DModelPrefab;
        [SerializeField] private RuntimeAnimatorController _view3DModelAnimatorController;
        [SerializeField] string _modularCharactersChildGOForModulesName;

        [Header("Character randomizer settings")]
        [SerializeField, Range(0,1)] private float _femaleGenderChance;
        [SerializeField] private string[] _femaleNamesPool;
        [SerializeField] private string[] _maleNamesPool;
        [SerializeField] private Material[] _fantasyHeroMaterialsPool;
        [Tooltip("cannot be greater than clothes slots count")]
        [SerializeField] private int _minClothesEquipItemsAmount;

        #endregion


        #region Properties

        public int BackpuckSlotAmount => _backpuckSlotAmount;
        public int WeaponSetsAmount => _weaponSetsAmount;
        public ClothesType[] ClothesSlots => (ClothesType[])_clothesSlots.Clone();
        public GameObject View3DModelPrefab => _view3DModelPrefab;
        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
        public string ModularCharactersChildGOForModulesName => _modularCharactersChildGOForModulesName;

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            if (_minClothesEquipItemsAmount > _clothesSlots.Length)
            {
                Debug.LogError("MinClothesEquipItemsAmount value cannot be greater than clothes slots count!");
                _minClothesEquipItemsAmount = _clothesSlots.Length;
            }
        }

        #endregion


        #region Methods

        public List<GameObject> GetModulePartsByNames(IEnumerable<string> modulePartsNames)
        {
            List<GameObject> moduleParts = new List<GameObject>();
            if (modulePartsNames != null)
            {
                foreach (string modulePartName in modulePartsNames)
                {
                    GameObject modulePart = GetModulePartByName(modulePartName);
                    if(modulePart != null)
                    {
                        moduleParts.Add(modulePart);
                    }
                }
            }
            return moduleParts;
        }

        public GameObject GetModulePartByName(string modulePartName)
        {
            Transform transform = _modularCharactersPrefab.transform.FindDeep(modulePartName);
            if(transform != null)
            {
                return transform.gameObject;
            }
            else
            {
                Debug.LogError($"Module {modulePartName} is not found in {_modularCharactersPrefab.name}");
                return null;
            }
        }

        public void BindModuleToCharacter(GameObject module, GameObject characterModel)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = module.GetComponent<SkinnedMeshRenderer>();
            Transform[] bonesFromOriginal = skinnedMeshRenderer.bones;
            Transform rootBoneFromOriginal = skinnedMeshRenderer.rootBone;

            Transform newRootBone = characterModel.transform.FindDeep(rootBoneFromOriginal.name);
            Transform[] allBonesInNewRootBone = newRootBone.GetComponentsInChildren<Transform>();

            Transform[] newBones = new Transform[bonesFromOriginal.Length];

            for (int i = 0; i < bonesFromOriginal.Length; i++)
            {
                for (int j = 0; j < allBonesInNewRootBone.Length; j++)
                {
                    if (bonesFromOriginal[i].name == allBonesInNewRootBone[j].name)
                    {
                        newBones[i] = allBonesInNewRootBone[j];
                    }
                }
            }

            skinnedMeshRenderer.bones = newBones;
            skinnedMeshRenderer.rootBone = newRootBone;
        }

        public bool IsFemale()
        {
            return Random.Range(0, 101) <= _femaleGenderChance * 100;
        }

        public string GetRandonNameFromPool(bool isFemale)
        {
            //todo: checking for the use of the name among existing characters
            if (isFemale)
            {
                return _femaleNamesPool[Random.Range(0, _femaleNamesPool.Length)];
            }
            else
            {
                return _maleNamesPool[Random.Range(0, _maleNamesPool.Length)];
            }
        }

        public Material GetRandomMaterialFromPool()
        {
            return _fantasyHeroMaterialsPool[Random.Range(0, _fantasyHeroMaterialsPool.Length)];
        }

        public List<ClothesItemModel> GetRandomStartingClothes(int rank)
        {
            List<ClothesItemModel> clothes = new List<ClothesItemModel>();

            int clothesItemsAmount = Random.Range(_minClothesEquipItemsAmount, _clothesSlots.Length);

            List<ClothesType> clothesTypePool = new List<ClothesType>();
            for (int i = 0; i < _clothesSlots.Length; i++)
            {
                clothesTypePool.Add(_clothesSlots[i]);
            }

            for (int i = 0; i < clothesItemsAmount; i++)
            {
                int randomIndex = Random.Range(0, clothesTypePool.Count);
                ClothesType randomClothesType = clothesTypePool[randomIndex];
                clothesTypePool.RemoveAt(randomIndex);

                ClothesItemData randomClothesData = BeastHunter.Data.HubUIData.ItemDataPools.GetRandomClothesDataByRankAndType(rank, randomClothesType);
                clothes.Add(new ClothesItemModel(randomClothesData)) ;
            }

            return clothes;
        }

        public List<WeaponItemModel> GetRandomStartingWeapon(int rank)
        {
            List<WeaponItemModel> weapons = new List<WeaponItemModel>();

            WeaponItemData randomWeaponData = BeastHunter.Data.HubUIData.ItemDataPools.GetRandomWeaponDataByRank(rank);
            weapons.Add(new WeaponItemModel(randomWeaponData));

            return weapons;
        }

        #endregion
    }
}
