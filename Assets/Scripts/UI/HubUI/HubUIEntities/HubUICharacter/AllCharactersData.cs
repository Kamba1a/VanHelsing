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

        #endregion


        #region Properties

        public int BackpuckSlotAmount => _backpuckSlotAmount;
        public int WeaponSetsAmount => _weaponSetsAmount;
        public ClothesType[] ClothesSlots => (ClothesType[])_clothesSlots.Clone();
        public GameObject View3DModelPrefab => _view3DModelPrefab;
        public RuntimeAnimatorController View3DModelAnimatorController => _view3DModelAnimatorController;
        public string ModularCharactersChildGOForModulesName => _modularCharactersChildGOForModulesName;

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

        #endregion
    }
}
