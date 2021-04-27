//using System;
//using UnityEngine;


//namespace BeastHunterHubUI
//{
//    [Serializable]
//    public struct CharactersSettingsStruct
//    {
//        #region Fields

//        [SerializeField] private ClothesType[] _clothesSlots;
//        [SerializeField] private int _backpuckSlotAmount;
//        [SerializeField] private int _weaponSetsAmount;
//        [SerializeField] private GameObject _modularCharactersPrefab;
//        [SerializeField] private GameObject _view3DModelPrefab;
//        [SerializeField] private RuntimeAnimatorController _view3DModelAnimatorController;

//        #endregion


//        #region Properties

//        public int BackpuckSlotAmount => _backpuckSlotAmount;
//        public int WeaponSetsAmount => _weaponSetsAmount;
//        public ClothesType[] ClothesSlots => (ClothesType[])_clothesSlots.Clone();
//        public GameObject View3DModelPrefab => _view3DModelPrefab;
//        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
//        public GameObject ModularCharactersPrefab => _modularCharactersPrefab;

//        #endregion
//    }
//}
