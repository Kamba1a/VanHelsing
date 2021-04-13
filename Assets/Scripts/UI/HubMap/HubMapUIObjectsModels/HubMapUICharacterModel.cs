using Extensions;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUICharacterModel
    {
        #region Fields

        private GameObject _view3DModelPrefab;
        private RuntimeAnimatorController _view3DModelAnimatorController;

        private Dictionary<HubMapUICharacterHeadParts, (string name, bool isActiveByDefault)> _defaultHeadParts;
        private Dictionary<HubMapUIClothesType, List<string>> _defaultModuleParts;

        //todo?:
        //private Dictionary<HubMapUICharacterHeadParts, GameObject> _defaultHeadParts;
        //private Dictionary<HubMapUIClothesType, List<GameObject>> _defaultModuleParts;
        //private Dictionary<HubMapUIClothesType, List<GameObject>> _clothesModuleParts;

        #endregion


        #region Properties

        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }
        public bool IsFemale { get; private set; }
        public GameObject View3DModelObjectOnScene { get; private set; }
        public HubMapUIItemStorage Backpack { get; private set; }
        public HubMapUIClothesEquipmentStorage ClothEquipment { get; private set; }

        //todo: Pockets and Weapon storages
        //public HubMapUIEquipmentModel Pockets { get; private set; }
        //public HubMapUIEquipmentModel Weapon { get; private set; }

        public HubMapUICharacterBehaviour Behaviour { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUICharacterModel(HubMapUICharacterData data, int backpackSize, HubMapUIClothesType[] clothEquipment)
        {
            Name = data.Name;
            Portrait = data.Portrait;
            IsFemale = data.IsFemale;
            _view3DModelPrefab = data.View3DModelPrefab;
            _view3DModelAnimatorController = data.View3DModelAnimatorController;

            Backpack = new HubMapUIItemStorage(backpackSize, HubMapUIItemStorageType.CharacterInventory);
            for (int i = 0; i < data.StartBackpuckItems.Length; i++)
            {
                HubMapUIBaseItemModel itemModel = HubMapUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartBackpuckItems[i]);
                Backpack.PutItem(i, itemModel);
            }

            ClothEquipment = new HubMapUIClothesEquipmentStorage(clothEquipment, HubMapUIItemStorageType.CharacterClothEquipment);
            for (int i = 0; i < data.StartEquipmentItems.Length; i++)
            {
                HubMapUIBaseItemModel clothes = HubMapUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartEquipmentItems[i]);
                ClothEquipment.PutItemToFirstEmptySlot(clothes);
            }
            ClothEquipment.OnTakeItemFromSlotHandler += OnTakeClothesEquipmentItem;
            ClothEquipment.OnPutItemToSlotHandler += OnPutClothesEquipmentItem;

            InitializeDefaultHeadPartsDictionary(data.DefaultHeadParts);
            InitializeDefaultModulePartsDictionary(data.DefaultModuleParts);
        }

        #endregion


        #region Methods

        public void InitializeView3DModel(Transform parent)
        {
            View3DModelObjectOnScene = GameObject.Instantiate(_view3DModelPrefab, parent);
            View3DModelObjectOnScene.GetComponent<Animator>().runtimeAnimatorController = _view3DModelAnimatorController;

            foreach (KeyValuePair<HubMapUIClothesType, List<string>> kvp in _defaultModuleParts)
            {
                SetActiveDefaultClothesByType(true, kvp.Key);
            }

            foreach (KeyValuePair<HubMapUICharacterHeadParts, (string name, bool isActiveByDefault)> kvp in _defaultHeadParts)
            {
                SetActiveModulePart(kvp.Value.isActiveByDefault, kvp.Value.name);
            }

            for (int i = 0; i < ClothEquipment.GetSlotsCount(); i++)
            {
                if (ClothEquipment.GetAll()[i] != null)
                {
                    PutOnClothes(ClothEquipment.GetAll()[i] as HubMapUIClothesItemModel);
                }
            }

            View3DModelObjectOnScene.SetActive(false);
        }

        private void OnTakeClothesEquipmentItem(HubMapUIItemStorageType storageType, int slotIndex, HubMapUIBaseItemModel item)
        {
            if (View3DModelObjectOnScene != null)
            {
                TakeOffClothes(item as HubMapUIClothesItemModel);
            }
        }

        private void OnPutClothesEquipmentItem(HubMapUIItemStorageType storageType, int slotIndex, HubMapUIBaseItemModel item)
        {
            if (View3DModelObjectOnScene != null)
            {
                PutOnClothes(item as HubMapUIClothesItemModel);
            }
        }

        private void InitializeDefaultHeadPartsDictionary(HubMapUICharacterHeadPart[] characterHeadParts)
        {
            _defaultHeadParts = new Dictionary<HubMapUICharacterHeadParts, (string, bool)>();
            
            for (int i = 0; i < characterHeadParts.Length; i++)
            {
                HubMapUICharacterHeadPart headPart = characterHeadParts[i];
                if (!_defaultHeadParts.ContainsKey(headPart.Type))
                {
                    _defaultHeadParts.Add(headPart.Type, (headPart.Name, headPart.IsActivateByDefault));
                }
            }
        }

        private void InitializeDefaultModulePartsDictionary(HubMapUICharacterClothesModuleParts[] clothesModuleParts)
        {
            _defaultModuleParts = new Dictionary<HubMapUIClothesType, List<string>>();

            for (int i = 0; i < clothesModuleParts.Length; i++)
            {
                HubMapUICharacterClothesModuleParts clothesParts = clothesModuleParts[i];

                if (!_defaultModuleParts.ContainsKey(clothesParts.Type))
                {
                    List<string> clothesNames = new List<string>();
                    for (int j = 0; j < clothesParts.Names.Count; j++)
                    {
                        clothesNames.Add(clothesParts.Names[j]);
                    }

                    _defaultModuleParts.Add(clothesParts.Type, clothesNames);
                }
            }
        }

        private void TakeOffClothes(HubMapUIClothesItemModel clothes)
        {
            HubMapUIClothesType clothesType = clothes.Type;

            SetActiveDefaultClothesByType(true, clothesType);

            if (clothesType == HubMapUIClothesType.Head)
            {
                foreach (KeyValuePair<HubMapUICharacterHeadParts, (string name, bool isActiveByDefault)> kvp in _defaultHeadParts)
                {
                    SetActiveModulePart(kvp.Value.isActiveByDefault, kvp.Value.name);
                }
            }

            SetActiveClothesModuleParts(false, clothes);
        }

        private void PutOnClothes(HubMapUIClothesItemModel clothes)
        {
            HubMapUIClothesType clothesType = clothes.Type;

            SetActiveClothesModuleParts(true, clothes);

            if (clothesType == HubMapUIClothesType.Head)
            {
                foreach (KeyValuePair<HubMapUICharacterHeadParts, (string name, bool)> kvp in _defaultHeadParts)
                {
                    SetActiveModulePart(true, kvp.Value.name);
                }

                for (int i = 0; i < clothes.DisabledHeadParts.Length; i++)
                {
                    SetActiveModulePart(false, _defaultHeadParts[clothes.DisabledHeadParts[i]].name);
                }
            }

            SetActiveDefaultClothesByType(false, clothesType);
        }

        private void SetActiveClothesModuleParts(bool flag, HubMapUIClothesItemModel clothes)
        {
            for (int i = 0; i < clothes.PartsNamesAllGender.Length; i++)
            {
                SetActiveModulePart(flag, clothes.PartsNamesAllGender[i]);
            }

            if (IsFemale)
            {
                for (int i = 0; i < clothes.PartsNamesFemale.Length; i++)
                {
                    SetActiveModulePart(flag, clothes.PartsNamesFemale[i]);
                }
            }
            else
            {
                for (int i = 0; i < clothes.PartsNamesMale.Length; i++)
                {
                    SetActiveModulePart(flag, clothes.PartsNamesMale[i]);
                }
            }
        }

        private void SetActiveDefaultClothesByType(bool flag, HubMapUIClothesType clothesType)
        {
            for (int i = 0; i < _defaultModuleParts[clothesType].Count; i++)
            {
                SetActiveModulePart(flag, _defaultModuleParts[clothesType][i]);
            }

        }

        private void SetActiveModulePart(bool flag, string modulePartName)
        {
            View3DModelObjectOnScene.transform.FindDeep(modulePartName).gameObject.SetActive(flag);
        }

        private void ActivatedModulePartAndAssignMaterial(bool flag, string modulePartName, Material fantasyHeroMaterial)
        {
            GameObject modulePart = View3DModelObjectOnScene.transform.FindDeep(modulePartName).gameObject;
            SetMaterial(modulePart, fantasyHeroMaterial);
            modulePart.SetActive(true);
        }

        private void SetMaterial(GameObject gameObject, Material fantasyHeroMaterial)
        {
            gameObject.GetComponent<SkinnedMeshRenderer>().material = fantasyHeroMaterial;
        }

        #endregion
    }
}
