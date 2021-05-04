using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class CharacterModel
    {
        #region Fields

        private AllCharactersData _allData;
        private Material _defaultCharacterMaterial;

        private Dictionary<CharacterHeadPartType, (string name, bool isActiveByDefault)> _defaultHeadPartsNames;
        private Dictionary<ClothesType, List<string>> _defaultModulePartsNames;
        private Dictionary<CharacterHeadPartType, (GameObject headPart, bool isActiveByDefault)> _defaultHeadParts;
        private Dictionary<ClothesType, List<GameObject>> _defaultModuleParts;
        private Dictionary<ClothesType, List<GameObject>> _clothesModuleParts;

        private Transform _characterModulesTransform;

        #endregion


        #region Properties

        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }
        public bool IsFemale { get; private set; }
        public GameObject View3DModelObjectOnScene { get; private set; }
        public ItemStorage Backpack { get; private set; }
        public EquippedClothesStorage ClothesEquipment { get; private set; }
        public PocketsStorage Pockets { get; private set; }
        public EquippedWeaponStorage WeaponEquipment { get; private set; }
        public bool IsHaveOrder { get; set; }
        public MapCharacterBehaviour Behaviour { get; set; }

        #endregion


        #region ClassLifeCycle

        public CharacterModel(CharacterData data)
        {
            _allData = BeastHunter.Data.HubUIData.AllCharactersData;
            Name = data.Name;
            Portrait = data.Portrait;
            IsFemale = data.IsFemale;
            _defaultCharacterMaterial = data.DefaultMaterial;

            Backpack = new ItemStorage(_allData.BackpuckSlotAmount, ItemStorageType.CharacterBackpuck);
            if (data.StartBackpuckItems != null)
            {
                for (int i = 0; i < data.StartBackpuckItems.Length; i++)
                {
                    BaseItemModel itemModel = HubUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartBackpuckItems[i]);
                    Backpack.PutItem(i, itemModel);
                }
            }

            Pockets = new PocketsStorage();

            ClothesEquipment = new EquippedClothesStorage(_allData.ClothesSlots);
            ClothesEquipment.IsEnoughEmptyPocketsFunc = Pockets.IsEnoughFreeSlots;

            ClothesEquipment.OnTakeItemFromSlotHandler += OnTakeClothesEquipmentItem;
            ClothesEquipment.OnPutItemToSlotHandler += OnPutClothesEquipmentItem;

            if (data.StartClothesEquipmentItems != null)
            {
                for (int i = 0; i < data.StartClothesEquipmentItems.Length; i++)
                {
                    BaseItemModel clothes = HubUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartClothesEquipmentItems[i]);
                    ClothesEquipment.PutItemToFirstEmptySlot(clothes);
                }
            }

            WeaponEquipment = new EquippedWeaponStorage(_allData.WeaponSetsAmount);

            if (data.StartWeaponEquipmentItems != null)
            {
                for (int i = 0; i < data.StartWeaponEquipmentItems.Length; i++)
                {
                    BaseItemModel weapon = HubUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartWeaponEquipmentItems[i]);
                    WeaponEquipment.PutItemToFirstEmptySlot(weapon);
                }
            }

            InitializeDefaultHeadPartsDictionary(data.DefaultHeadParts);
            InitializeDefaultModulePartsDictionary(data.DefaultModuleParts);
            InitializeView3DModel(_allData.Character3DViewModelRendering.transform);
        }

        public CharacterModel(int rank)
        {
            _allData = BeastHunter.Data.HubUIData.AllCharactersData; ;
            IsFemale = _allData.IsFemale() ? true : false;
            Name = _allData.GetRandonNameFromPool(IsFemale);
            //todo: Portrait = ?;
            _defaultCharacterMaterial = _allData.GetRandomMaterialFromPool();

            Backpack = new ItemStorage(_allData.BackpuckSlotAmount, ItemStorageType.CharacterBackpuck);
            Pockets = new PocketsStorage();

            WeaponEquipment = new EquippedWeaponStorage(_allData.WeaponSetsAmount);
            ClothesEquipment = new EquippedClothesStorage(_allData.ClothesSlots);
            ClothesEquipment.IsEnoughEmptyPocketsFunc = Pockets.IsEnoughFreeSlots;
            ClothesEquipment.OnTakeItemFromSlotHandler += OnTakeClothesEquipmentItem;
            ClothesEquipment.OnPutItemToSlotHandler += OnPutClothesEquipmentItem;

            List<ClothesItemModel> startingClothesItems = _allData.GetRandomStartingClothes(rank);
            for (int i = 0; i < startingClothesItems.Count; i++)
            {
                ClothesEquipment.PutItemToFirstEmptySlot(startingClothesItems[i]);
            }

            List<WeaponItemModel> startingWeapon = _allData.GetRandomStartingWeapon(rank);
            for (int i = 0; i < startingWeapon.Count; i++)
            {
                WeaponEquipment.PutItemToFirstEmptySlot(startingWeapon[i]);
            }

            InitializeDefaultHeadPartsDictionary(_allData.GetDefaultHeadModuleParts(IsFemale));
            InitializeDefaultModulePartsDictionary(_allData.GetDefaultBodyModules(IsFemale));
            InitializeView3DModel(_allData.Character3DViewModelRendering.transform);
        }

        #endregion


        #region Methods

        public bool EquipClothesItem(BaseItemStorage outStorage, int outStorageSlotIndex)
        {
            BaseItemModel item = outStorage.GetItemBySlot(outStorageSlotIndex);
            if (item != null && item.ItemType == ItemType.Clothes)
            {
                if (!ClothesEquipment.PutItemToFirstEmptySlot(item))
                {
                    int? firstSlot = ClothesEquipment.GetFirstSlotIndexForItem(item as ClothesItemModel);
                    ClothesEquipment.SwapItemsWithOtherStorage(firstSlot.Value, outStorage, outStorageSlotIndex);
                }
                else
                {
                    outStorage.RemoveItem(outStorageSlotIndex);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EquipWeaponItem(BaseItemStorage outStorage, int outStorageSlotIndex)
        {
            BaseItemModel item = outStorage.GetItemBySlot(outStorageSlotIndex);
            if (item != null && item.ItemType == ItemType.Weapon)
            {
                if (!WeaponEquipment.PutItemToFirstEmptySlot(item))
                {
                    HubUIServices.SharedInstance.GameMessages.Notice("There is no free slots for this weapon");
                    return false;
                }
                else
                {
                    outStorage.RemoveItem(outStorageSlotIndex);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

         public void InitializeView3DModel(Transform parent)
        {
            View3DModelObjectOnScene = GameObject.Instantiate(_allData.View3DModelPrefab, parent);
            View3DModelObjectOnScene.GetComponent<Animator>().runtimeAnimatorController = _allData.View3DModelAnimatorController;
            _characterModulesTransform = View3DModelObjectOnScene.transform.FindDeep(_allData.ModularCharactersChildGOForModulesName);

            InitializeDefaultModules();

            _clothesModuleParts = new Dictionary<ClothesType, List<GameObject>>();
            for (int i = 0; i < ClothesEquipment.GetSlotsCount(); i++)
            {
                if (ClothesEquipment.GetItemBySlot(i) != null)
                {
                    PutOnClothesOnModel(ClothesEquipment.GetItemBySlot(i) as ClothesItemModel);
                }
            }

            View3DModelObjectOnScene.SetActive(false);
        }

        private void OnTakeClothesEquipmentItem(ItemStorageType storageType, int slotIndex, BaseItemModel item)
        {
            ClothesItemModel clothes = item as ClothesItemModel;

            if (clothes != null)
            {
                Pockets.RemovePockets(clothes.PocketsAmount);

                if (View3DModelObjectOnScene != null)
                {
                    TakeOffClothesFromModel(clothes);
                }
            }
        }

        private void OnPutClothesEquipmentItem(ItemStorageType storageType, int slotIndex, BaseItemModel item)
        {
            ClothesItemModel clothes = item as ClothesItemModel;

            if (clothes != null)
            {
                Pockets.AddPockets(clothes.PocketsAmount);

                if (View3DModelObjectOnScene != null)
                {
                    PutOnClothesOnModel(clothes);
                }
            }
        }

        private void InitializeDefaultModules()
        {
            _defaultModuleParts = new Dictionary<ClothesType, List<GameObject>>();
            foreach (KeyValuePair<ClothesType, List<string>> kvp in _defaultModulePartsNames)
            {
                List<GameObject> defaultModules = AddModulePartsTo3DModel(_allData.GetModulePartsByNames(kvp.Value), _defaultCharacterMaterial);
                for (int i = 0; i < defaultModules.Count; i++)
                {
                    defaultModules[i].name += "(default)";
                }
                _defaultModuleParts.Add(kvp.Key, defaultModules);
            }

            _defaultHeadParts = new Dictionary<CharacterHeadPartType, (GameObject module, bool isActiveByDefault)>();
            foreach (KeyValuePair<CharacterHeadPartType, (string name, bool isActiveByDefault)> kvp in _defaultHeadPartsNames)
            {
                GameObject defaultHeadPart = AddModulePartTo3DModel(_allData.GetModulePartByName(kvp.Value.name), _defaultCharacterMaterial);
                defaultHeadPart.name += "(default)";
                _defaultHeadParts.Add(kvp.Key, (defaultHeadPart, kvp.Value.isActiveByDefault));
            }
            SetDefaultActiveStatusToHeadParts();
        }

        private void TakeOffClothesFromModel(ClothesItemModel clothes)
        {
            ClothesType clothesType = clothes.ClothesType;

            SetActiveStatusToDefaultClothesByType(true, clothesType);

            if (clothesType == ClothesType.Head)
            {
                SetDefaultActiveStatusToHeadParts();
            }

            RemoveClothesModulePartsFrom3DModel(clothes.ClothesType);
        }

        private void PutOnClothesOnModel(ClothesItemModel clothes)
        {
            ClothesType clothesType = clothes.ClothesType;

            AddClothesModulePartsTo3DModel(clothes);

            if (clothesType == ClothesType.Head)
            {
                foreach (KeyValuePair<CharacterHeadPartType, (GameObject headPart, bool)> kvp in _defaultHeadParts)
                {
                    kvp.Value.headPart.SetActive(true);
                }

                for (int i = 0; i < clothes.DisabledHeadParts.Length; i++)
                {
                    if (_defaultHeadParts.ContainsKey(clothes.DisabledHeadParts[i]))
                    {
                        _defaultHeadParts[clothes.DisabledHeadParts[i]].headPart.SetActive(false);
                    }
                }
            }

            SetActiveStatusToDefaultClothesByType(false, clothesType);
        }

        private void RemoveClothesModulePartsFrom3DModel(ClothesType clothesType)
        {
            for (int i = 0; i < _clothesModuleParts[clothesType].Count; i++)
            {
                _clothesModuleParts[clothesType][i].SetActive(false);
                GameObject.Destroy(_clothesModuleParts[clothesType][i]);
            }
            _clothesModuleParts[clothesType].Clear();
        }

        private void AddClothesModulePartsTo3DModel(ClothesItemModel clothes)
        {
            List<GameObject> moduleParts = new List<GameObject>();

            moduleParts.AddRange(_allData.GetModulePartsByNames(clothes.PartsNamesAllGender));

            if (IsFemale)
            {
                moduleParts.AddRange(_allData.GetModulePartsByNames(clothes.PartsNamesFemale));
            }
            else
            {
                moduleParts.AddRange(_allData.GetModulePartsByNames(clothes.PartsNamesMale));
            }

            if (moduleParts.Count > 0)
            {
                AddClothesModulesToDictionary(clothes.ClothesType, AddModulePartsTo3DModel(moduleParts, clothes.FantasyHeroMaterial));
            }
        }

        private void AddClothesModulesToDictionary(ClothesType clothesType, List<GameObject> clothesModules)
        {
            if (!_clothesModuleParts.ContainsKey(clothesType))
            {
                _clothesModuleParts.Add(clothesType, new List<GameObject>());
            }
            _clothesModuleParts[clothesType].AddRange(clothesModules);
        }

        private List<GameObject> AddModulePartsTo3DModel(List<GameObject> moduleParts, Material fantasyHeroMaterial)
        {
            List<GameObject> addedModules = new List<GameObject>();
            for (int i = 0; i < moduleParts.Count; i++)
            {
                addedModules.Add(AddModulePartTo3DModel(moduleParts[i], fantasyHeroMaterial));
            }
            return addedModules;
        }

        private GameObject AddModulePartTo3DModel(GameObject modulePart, Material fantasyHeroMaterial)
        {
            GameObject addedModule = GameObject.Instantiate(modulePart, _characterModulesTransform);
            _allData.BindModuleToCharacter(addedModule, View3DModelObjectOnScene);
            addedModule.GetComponent<SkinnedMeshRenderer>().material = fantasyHeroMaterial;
            addedModule.SetActive(true);
            return addedModule;
        }

        private void SetDefaultActiveStatusToHeadParts()
        {
            foreach (KeyValuePair<CharacterHeadPartType, (GameObject headPart, bool isActiveByDefault)> kvp in _defaultHeadParts)
            {
                kvp.Value.headPart.SetActive(kvp.Value.isActiveByDefault);
            }
        }

        private void SetActiveStatusToDefaultClothesByType(bool flag, ClothesType clothesType)
        {
            if (_defaultModuleParts.ContainsKey(clothesType))
            {
                for (int i = 0; i < _defaultModuleParts[clothesType].Count; i++)
                {
                    _defaultModuleParts[clothesType][i].SetActive(flag);
                }
            }
        }

        private void InitializeDefaultHeadPartsDictionary(IEnumerable<CharacterHeadPart> characterHeadParts)
        {
            _defaultHeadPartsNames = new Dictionary<CharacterHeadPartType, (string, bool)>();

            foreach (CharacterHeadPart headPart in characterHeadParts)
            {
                if (!_defaultHeadPartsNames.ContainsKey(headPart.Type))
                {
                    _defaultHeadPartsNames.Add(headPart.Type, (headPart.Name, headPart.IsActivateByDefault));
                }
            }
        }

        private void InitializeDefaultModulePartsDictionary(IEnumerable<CharacterClothesModuleParts> clothesModuleParts)
        {
            _defaultModulePartsNames = new Dictionary<ClothesType, List<string>>();

            foreach (CharacterClothesModuleParts clothesParts in clothesModuleParts)
            {
                if (!_defaultModulePartsNames.ContainsKey(clothesParts.Type))
                {
                    List<string> clothesNames = new List<string>();
                    for (int j = 0; j < clothesParts.Names.Count; j++)
                    {
                        clothesNames.Add(clothesParts.Names[j]);
                    }
                    _defaultModulePartsNames.Add(clothesParts.Type, clothesNames);
                }
            }
        }

        #endregion
    }
}
