using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "AllCharacters", menuName = "CreateData/HubUIData/AllCharacters", order = 0)]
    public class AllCharactersData : ScriptableObject
    {
        #region Constants

        private const int CHECK_NAME_ON_FREE_ATTEMPTS_NUMBER = 500;

        #endregion


        #region Fields

        [SerializeField] private ClothesType[] _clothesSlots;
        [SerializeField] private int _backpuckSlotAmount;
        [SerializeField] private int _weaponSetsAmount;
        [SerializeField] private GameObject _modularCharactersPrefab;
        [SerializeField] private GameObject _view3DModelPrefab;
        [SerializeField] private RuntimeAnimatorController _view3DModelAnimatorController;
        [SerializeField] string _modularCharactersChildGOForModulesName;
        [SerializeField] private GameObject _characters3DViewRenderingPrefab;
        [SerializeField] private Vector3 _characters3DViewRenderingObjectPosition;

        [Header("Character randomizer settings")]
        [SerializeField, Range(0, 1)] private float _femaleGenderChance;
        [SerializeField] private string[] _femaleNamesPool;
        [SerializeField] private string[] _maleNamesPool;
        [SerializeField] private Material[] _fantasyHeroMaterialsPool;
        [Tooltip("cannot be greater than clothes slots count")]
        [SerializeField] private int _minClothesEquipItemsAmount;
        [SerializeField, Range(0, 1)] private float _hairlessChance;
        [SerializeField, Range(0, 1)] private float _maleFacialHairChance;

        private List<string> _allHairModulesNames;
        private List<string> _femaleHeadModulesNames;
        private List<string> _femaleEyebrowModulesNames;
        private List<string> _maleHeadModulesNames;
        private List<string> _maleEyebrowModulesNames;
        private List<string> _maleFacialHairModulesNames;

        private List<CharacterClothesModuleParts> _femaleDefaultBodyParts = new List<CharacterClothesModuleParts>()
        {
            new CharacterClothesModuleParts(ClothesType.Torso, new List<string>(){ "Chr_Torso_Female_00" }),
            new CharacterClothesModuleParts(ClothesType.Hips, new List<string>(){ "Chr_Hips_Female_00" }),

            new CharacterClothesModuleParts(ClothesType.Shoulders, new List<string>()
            {
                "Chr_ArmUpperRight_Female_00",
                "Chr_ArmUpperLeft_Female_00",
            }),
            new CharacterClothesModuleParts(ClothesType.Arms, new List<string>()
            {
                "Chr_ArmLowerRight_Female_00",
                "Chr_ArmLowerLeft_Female_00",
            }),
            new CharacterClothesModuleParts(ClothesType.Hands, new List<string>()
            {
                "Chr_HandRight_Female_00",
                "Chr_HandLeft_Female_00",
            }),
            new CharacterClothesModuleParts(ClothesType.Legs, new List<string>()
            {
                "Chr_LegRight_Female_00",
                "Chr_LegLeft_Female_00",
            }),
        };

        private List<CharacterClothesModuleParts> _maleDefaultBodyParts = new List<CharacterClothesModuleParts>()
        {
            new CharacterClothesModuleParts(ClothesType.Torso, new List<string>(){ "Chr_Torso_Male_00" }),
            new CharacterClothesModuleParts(ClothesType.Hips, new List<string>(){ "Chr_Hips_Male_00" }),

            new CharacterClothesModuleParts(ClothesType.Shoulders, new List<string>()
            {
                "Chr_ArmUpperRight_Male_00",
                "Chr_ArmUpperLeft_Male_00",
            }),
            new CharacterClothesModuleParts(ClothesType.Arms, new List<string>()
            {
                "Chr_ArmLowerRight_Male_00",
                "Chr_ArmLowerLeft_Male_00",
            }),
            new CharacterClothesModuleParts(ClothesType.Hands, new List<string>()
            {
                "Chr_HandRight_Male_00",
                "Chr_HandLeft_Male_00",
            }),
            new CharacterClothesModuleParts(ClothesType.Legs, new List<string>()
            {
                "Chr_LegRight_Male_00",
                "Chr_LegLeft_Male_00",
            }),
        };

        #endregion


        #region Properties

        public int BackpackSlotAmount => _backpuckSlotAmount;
        public int WeaponSetsAmount => _weaponSetsAmount;
        public ClothesType[] ClothesSlots => (ClothesType[])_clothesSlots.Clone();
        public GameObject View3DModelPrefab => _view3DModelPrefab;
        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
        public string ModularCharactersChildGOForModulesName => _modularCharactersChildGOForModulesName;
        public GameObject Character3DViewModelRendering { get; private set; }
        public Camera CharacterPortraitCamera { get; private set; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            if(_modularCharactersPrefab != null)
            {
                _allHairModulesNames = GetModulesNamesByGroupName("All_01_Hair");
                _femaleHeadModulesNames = GetModulesNamesByGroupName("Female_Head_All_Elements");
                _femaleEyebrowModulesNames = GetModulesNamesByGroupName("Female_01_Eyebrows");
                _maleHeadModulesNames = GetModulesNamesByGroupName("Male_Head_All_Elements");
                _maleEyebrowModulesNames = GetModulesNamesByGroupName("Male_01_Eyebrows");
                _maleFacialHairModulesNames = GetModulesNamesByGroupName("Male_02_FacialHair");
            }
            else
            {
                _allHairModulesNames = new List<string>();
                _femaleHeadModulesNames = new List<string>();
                _femaleEyebrowModulesNames = new List<string>();
                _maleHeadModulesNames = new List<string>();
                _maleEyebrowModulesNames = new List<string>();
                _maleFacialHairModulesNames = new List<string>();
            }
        }

        private void OnDisable()
        {
            _allHairModulesNames.Clear();
            _femaleHeadModulesNames.Clear();
            _femaleEyebrowModulesNames.Clear();
            _maleHeadModulesNames.Clear();
            _maleEyebrowModulesNames.Clear();
            _maleFacialHairModulesNames.Clear();

            _allHairModulesNames = null;
            _femaleHeadModulesNames = null;
            _femaleEyebrowModulesNames = null;
            _maleHeadModulesNames = null;
            _maleEyebrowModulesNames = null;
            _maleFacialHairModulesNames = null;
        }

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

        public void InitializeCharacter3DViewModelRendering()
        {
            Character3DViewModelRendering =
                Instantiate(_characters3DViewRenderingPrefab, _characters3DViewRenderingObjectPosition, Quaternion.identity);
            CharacterPortraitCamera = Character3DViewModelRendering.transform.Find("CameraForPortrait").GetComponent<Camera>();
            CharacterPortraitCamera.enabled = false;
        }

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
            if(skinnedMeshRenderer != null)
            {
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
            else
            {
                Debug.LogError($"{module.name} not contain SkinnedMeshRenderer component");
            }
        }

        public bool IsFemale()
        {
            return Random.Range(0, 101) <= _femaleGenderChance * 100;
        }

        public string GetRandonNameFromPool(bool isFemale)
        {
            string name = null;
            
            for (int i = 0; i < CHECK_NAME_ON_FREE_ATTEMPTS_NUMBER; i++)
            {
                name = isFemale ?
                _femaleNamesPool[Random.Range(0, _femaleNamesPool.Length)] :
                _maleNamesPool[Random.Range(0, _maleNamesPool.Length)];

                if (HubUIServices.SharedInstance.CharacterCheckNameService.IsNameFree(name))
                {
                    break;
                }

                if (i == CHECK_NAME_ON_FREE_ATTEMPTS_NUMBER-1)
                {
                    Debug.LogWarning("Need to increase the characters names pool");
                }
            }

            return name;
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

        public List<CharacterClothesModuleParts> GetDefaultBodyModules(bool isFemale)
        {
            if (isFemale)
            {
                return _femaleDefaultBodyParts;
            }
            else
            {
                return _maleDefaultBodyParts;
            }
        }

        public List<CharacterHeadPart> GetDefaultHeadModuleParts(bool isFemale)
        {
            List<CharacterHeadPart> headModuleParts = new List<CharacterHeadPart>();

            bool isFacialHair = !isFemale && Random.Range(0, 100) <= _maleFacialHairChance;
            bool isHairless = Random.Range(0, 101) <= _hairlessChance;

            string randomHeadModule;
            string randomEyebrowsModule;

            if (isFemale)
            {
                randomHeadModule = _femaleHeadModulesNames[Random.Range(0, _femaleHeadModulesNames.Count)];
                randomEyebrowsModule = _femaleEyebrowModulesNames[Random.Range(0, _femaleEyebrowModulesNames.Count)];
            }
            else
            {
                randomHeadModule = _maleHeadModulesNames[Random.Range(0, _maleHeadModulesNames.Count)];
                randomEyebrowsModule = _maleEyebrowModulesNames[Random.Range(0, _maleEyebrowModulesNames.Count)];
            }
            headModuleParts.Add(new CharacterHeadPart(CharacterHeadPartType.Head, randomHeadModule));
            headModuleParts.Add(new CharacterHeadPart(CharacterHeadPartType.Eyebrows, randomEyebrowsModule));

            if (!isHairless)
            {
                string randomHairModule = _allHairModulesNames[Random.Range(0, _allHairModulesNames.Count)];
                headModuleParts.Add(new CharacterHeadPart(CharacterHeadPartType.Hair, randomHairModule));
            }

            if (isFacialHair)
            {
                string randomFacialHairModule = _maleFacialHairModulesNames[Random.Range(0, _maleFacialHairModulesNames.Count)];
                headModuleParts.Add(new CharacterHeadPart(CharacterHeadPartType.FacialHair, randomFacialHairModule));
            }

            return headModuleParts;
        }

        private List<string> GetModulesNamesByGroupName(string groupName)
        {
            List<string> modulesNames = new List<string>();

            SkinnedMeshRenderer[] skinnedMeshRenderers =
                _modularCharactersPrefab.transform.FindDeep(groupName).gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                modulesNames.Add(skinnedMeshRenderers[i].gameObject.name);
            }

            return modulesNames;
        }

        #endregion
    }
}
