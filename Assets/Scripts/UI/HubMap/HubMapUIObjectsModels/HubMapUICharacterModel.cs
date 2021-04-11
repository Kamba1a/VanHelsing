using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUICharacterModel
    {
        #region Fields

        private GameObject _view3DModelPrefab;
        private RuntimeAnimatorController _view3DModelAnimatorController;
        private Dictionary<HubMapUICharacterHeadParts, GameObject> _defaultHeadParts;
        private Dictionary<HubMapUIClothesType, List<GameObject>> _defaultModuleParts;
        private Dictionary<HubMapUIClothesType, List<GameObject>> _clothesModuleParts;


        #endregion


        #region Properties

        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }
        public bool IsFemale { get; private set; }
        public GameObject View3DModelObjectOnScene { get; private set; }
        public HubMapUIItemStorage Backpack { get; private set; }

        //todo: Pockets and Weapon storages
        public HubMapUIClothesEquipmentStorage ClothEquipment { get; private set; }
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

            InitializeHeadPartsDictionary();
            InitializeModulePartsDictionary(ref _defaultModuleParts);
            InitializeModulePartsDictionary(ref _clothesModuleParts);

            //TODO: fill default dictionaries
            //идея: в демо сцене чарактер рандомайзера сделать возможность загружать в нужный дата-файл список активированных частей (string)
            //таким образом, любого персонажа можно сохранить в его дату списком строк с именами активированных частей О_о

            ClothEquipment = new HubMapUIClothesEquipmentStorage(clothEquipment, HubMapUIItemStorageType.CharacterClothEquipment);
            for (int i = 0; i < data.StartEquipmentItems.Length; i++)
            {
                HubMapUIBaseItemModel clothes = HubMapUIServices.SharedInstance.ItemInitializeService.InitializeItemModel(data.StartEquipmentItems[i]);
                ClothEquipment.PutItemToFirstEmptySlot(clothes);
            }
            ClothEquipment.OnTakeItemFromSlotHandler += OnTakeClothesEquipmentItem;
            ClothEquipment.OnPutItemToSlotHandler += OnPutClothesEquipmentItem;
        }

        #endregion


        #region Methods

        public void InitializeView3DModel(Transform parent)
        {
            View3DModelObjectOnScene = GameObject.Instantiate(_view3DModelPrefab, parent);
            View3DModelObjectOnScene.GetComponent<Animator>().runtimeAnimatorController = _view3DModelAnimatorController;

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
            TakeOffClothes(item as HubMapUIClothesItemModel);
        }

        private void OnPutClothesEquipmentItem(HubMapUIItemStorageType storageType, int slotIndex, HubMapUIBaseItemModel item)
        {
            PutOnClothes(item as HubMapUIClothesItemModel);
        }

        private void InitializeHeadPartsDictionary()
        {
            _defaultHeadParts = new Dictionary<HubMapUICharacterHeadParts, GameObject>();
            _defaultHeadParts.Add(HubMapUICharacterHeadParts.Eyebrows, null);
            _defaultHeadParts.Add(HubMapUICharacterHeadParts.FacialHair, null);
            _defaultHeadParts.Add(HubMapUICharacterHeadParts.Hair, null);
            _defaultHeadParts.Add(HubMapUICharacterHeadParts.Head, null);
        }

        private void InitializeModulePartsDictionary(ref Dictionary<HubMapUIClothesType, List<GameObject>> dictionary)
        {
            dictionary = new Dictionary<HubMapUIClothesType, List<GameObject>>();
            dictionary.Add(HubMapUIClothesType.Back, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Head, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Torso, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Hips, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Legs, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Shoulders, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Arms, new List<GameObject>());
            dictionary.Add(HubMapUIClothesType.Hands, new List<GameObject>());
        }

        private void TakeOffClothes(HubMapUIClothesItemModel clothes)
        {
            HubMapUIClothesType clothesType = clothes.ClothesType;
            if (_clothesModuleParts.ContainsKey(clothesType))
            {
                SetActiveDefaultClothesType(clothesType, true);

                if (clothesType == HubMapUIClothesType.Head)
                {
                    foreach (KeyValuePair<HubMapUICharacterHeadParts, GameObject> kvp in _defaultHeadParts)
                    {
                        kvp.Value.SetActive(true);
                    }
                }

                for (int i = 0; i < _clothesModuleParts[clothesType].Count; i++)
                {
                    _clothesModuleParts[clothesType][i].SetActive(false);
                }
                _clothesModuleParts[clothesType].Clear();
            }
        }

        private void PutOnClothes(HubMapUIClothesItemModel clothes)
        {
            HubMapUIClothesType clothesType = clothes.ClothesType;
            if (_clothesModuleParts.ContainsKey(clothesType))
            {
                for (int i = 0; i < clothes.ClothesPartsNamesAllGender.Length; i++)
                {
                    ActivateModulePart(clothesType, clothes.ClothesPartsNamesAllGender[i]);
                }

                if (IsFemale)
                {
                    for (int i = 0; i < clothes.ClothesPartsNamesFemale.Length; i++)
                    {
                        ActivateModulePart(clothesType, clothes.ClothesPartsNamesFemale[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < clothes.ClothesPartsNamesMale.Length; i++)
                    {
                        ActivateModulePart(clothesType, clothes.ClothesPartsNamesMale[i]);
                    }
                }

                if (clothesType == HubMapUIClothesType.Head)
                {
                    for (int i = 0; i < clothes.DisabledHeadParts.Length; i++)
                    {
                        _defaultHeadParts[clothes.DisabledHeadParts[i]].SetActive(false);
                    }
                }

                SetActiveDefaultClothesType(clothesType, false);
            }
        }

        private void ActivateModulePart(HubMapUIClothesType clothesType, string modulePartName)
        {
            GameObject modulePart = View3DModelObjectOnScene.transform.Find(modulePartName).gameObject;
            modulePart.SetActive(true);
            _clothesModuleParts[clothesType].Add(modulePart);

        }

        private void SetActiveDefaultClothesType(HubMapUIClothesType clothesType, bool flag)
        {
            for (int i = 0; i < _defaultModuleParts[clothesType].Count; i++)
            {
                _defaultModuleParts[clothesType][i].SetActive(flag);
            }
        }

        #endregion
    }
}
