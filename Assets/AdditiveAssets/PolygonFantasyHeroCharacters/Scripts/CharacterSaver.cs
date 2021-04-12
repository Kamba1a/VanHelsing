using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    class CharacterSaver : MonoBehaviour
    {
        [SerializeField] HubMapUICharacterData _characterData;

        private GameObject _modularCharacters;
        private Dictionary<HubMapUIClothesType, List<string>> _moduleLists;
        private Dictionary<HubMapUICharacterHeadParts, List<string>> _headPartsLists;


        private void Start()
        {
            _modularCharacters = GameObject.Find("ModularCharacters");
            InitializeModulePartsDictionary(ref _moduleLists);
        }

        [ContextMenu("SaveCharacterModulesInCharacterData")]
        public void SaveCharacterModulesInCharacterData()
        {

        }

        private void InitializeHeadPartsDictionary()
        {
            _headPartsLists = new Dictionary<HubMapUICharacterHeadParts, List<string>>();

            List<string> eyebrowsList = new List<string>()
            {
                "Male_01_Eyebrows",
                "Female_01_Eyebrows",
            };
            _headPartsLists.Add(HubMapUICharacterHeadParts.Eyebrows, eyebrowsList);

            List<string> facialHairList = new List<string>()
            {
                "Male_02_FacialHair",
            };
            _headPartsLists.Add(HubMapUICharacterHeadParts.FacialHair, facialHairList);

            List<string> hairList = new List<string>()
            {
                "All_01_Hair",
            };
            _headPartsLists.Add(HubMapUICharacterHeadParts.Hair, hairList);

            List<string> headList = new List<string>()
            {
                "Male_Head_All_Elements",
                "Female_Head_All_Elements"
            };
            _headPartsLists.Add(HubMapUICharacterHeadParts.Head, headList);
        }

        private void InitializeModulePartsDictionary(ref Dictionary<HubMapUIClothesType, List<string>> dictionary)
        {
            dictionary = new Dictionary<HubMapUIClothesType, List<string>>();

            List<string> backList = new List<string>()
            {
                "All_04_Back_Attachment",

            };
            dictionary.Add(HubMapUIClothesType.Back, backList);

            List<string> headList = new List<string>()
            {
                "Helmet",
                "Female_Head_No_Elements",
                "Male_Head_No_Elements",
                "HeadCoverings_No_Hair",
                "HeadCoverings_No_FacialHair",
                "HeadCoverings_Base_Hair",
            };
            dictionary.Add(HubMapUIClothesType.Head, headList);

            List<string> torsoList = new List<string>()
            {
                "Male_03_Torso",
                "Female_03_Torso",
            };
            dictionary.Add(HubMapUIClothesType.Torso, torsoList);

            List<string> hipsList = new List<string>()
            {
                "Male_10_Hips",
                "Female_10_Hips",
                "All_10_Knee_Attachement_Right",
                "All_11_Knee_Attachement_Left",
            };
            dictionary.Add(HubMapUIClothesType.Hips, hipsList);

            List<string> legsList = new List<string>()
            {
                "Male_11_Leg_Right",
                "Female_11_Leg_Right",
                "Male_12_Leg_Left",
                "Female_12_Leg_Left",
            };
            dictionary.Add(HubMapUIClothesType.Legs, legsList);

            List<string> shouldersList = new List<string>()
            {
                "All_05_Shoulder_Attachment_Right",
                "All_06_Shoulder_Attachment_Left",
                "Male_04_Arm_Upper_Right",
                "Female_04_Arm_Upper_Right",
                "Male_05_Arm_Upper_Left",
                "Female_05_Arm_Upper_Left",
            };
            dictionary.Add(HubMapUIClothesType.Shoulders, shouldersList);

            List<string> armsList = new List<string>()
            {
                "Male_06_Arm_Lower_Right",
                "Female_06_Arm_Lower_Right",
                "Male_07_Arm_Lower_Left",
                "Female_07_Arm_Lower_Left",
            };
            dictionary.Add(HubMapUIClothesType.Arms, armsList);

            List<string> handsList = new List<string>()
            {
                "Male_08_Hand_Right",
                "Female_08_Hand_Right",
                "Male_09_Hand_Left",
                "Female_09_Hand_Left",
            };
            dictionary.Add(HubMapUIClothesType.Hands, handsList);

            List<string> beltList = new List<string>()
            {
                "All_09_Hips_Attachment",
            };
            dictionary.Add(HubMapUIClothesType.Belt, beltList);
        }

        private void FillModuleListsDictionary()
        {

        }

        ////build out male lists
        //BuildList(male.headAllElements, "Male_Head_All_Elements");
        //BuildList(male.headNoElements, "Male_Head_No_Elements");
        //BuildList(male.eyebrow, "Male_01_Eyebrows");
        //BuildList(male.facialHair, "Male_02_FacialHair");
        //BuildList(male.torso, "Male_03_Torso");
        //BuildList(male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
        //BuildList(male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
        //BuildList(male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
        //BuildList(male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
        //BuildList(male.hand_Right, "Male_08_Hand_Right");
        //BuildList(male.hand_Left, "Male_09_Hand_Left");
        //BuildList(male.hips, "Male_10_Hips");
        //BuildList(male.leg_Right, "Male_11_Leg_Right");
        //BuildList(male.leg_Left, "Male_12_Leg_Left");

        ////build out female lists
        //BuildList(female.headAllElements, "Female_Head_All_Elements");
        //BuildList(female.headNoElements, "Female_Head_No_Elements");
        //BuildList(female.eyebrow, "Female_01_Eyebrows");
        //BuildList(female.facialHair, "Female_02_FacialHair");
        //BuildList(female.torso, "Female_03_Torso");
        //BuildList(female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
        //BuildList(female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
        //BuildList(female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
        //BuildList(female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
        //BuildList(female.hand_Right, "Female_08_Hand_Right");
        //BuildList(female.hand_Left, "Female_09_Hand_Left");
        //BuildList(female.hips, "Female_10_Hips");
        //BuildList(female.leg_Right, "Female_11_Leg_Right");
        //BuildList(female.leg_Left, "Female_12_Leg_Left");

        //// build out all gender lists
        //BuildList(allGender.all_Hair, "All_01_Hair");
        //BuildList(allGender.all_Head_Attachment, "All_02_Head_Attachment");
        //BuildList(allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
        //BuildList(allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
        //BuildList(allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
        //BuildList(allGender.chest_Attachment, "All_03_Chest_Attachment");
        //BuildList(allGender.back_Attachment, "All_04_Back_Attachment");
        //BuildList(allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
        //BuildList(allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
        //BuildList(allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
        //BuildList(allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
        //BuildList(allGender.hips_Attachment, "All_09_Hips_Attachment");
        //BuildList(allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
        //BuildList(allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
        //BuildList(allGender.elf_Ear, "Elf_Ear");


        ////WIP
        //Dictionary<string, Transform> _bonesMap;
        //GameObject newCharacter;
        //[ContextMenu("Create character copy on scene (WIP)")]
        //public void CreateCharacterCopyOnScene()
        //{
        //    newCharacter = GameObject.Instantiate(_modularCharacters, null, false);
        //    //Destroy(newCharacter.GetComponent<CharacterRandomizer>());

        //    SkinnedMeshRenderer[] moduleParts = newCharacter.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        //    _bonesMap = new Dictionary<string, Transform>();
        //    for (int i = 0; i < moduleParts.Length; i++)
        //    {
        //        for (int j = 0; j < moduleParts[i].bones.Length; j++)
        //        {
        //            if (!_bonesMap.ContainsKey(moduleParts[i].bones[j].name))
        //            {
        //                _bonesMap.Add(moduleParts[i].bones[j].name, moduleParts[i].bones[j]);
        //            }
        //        }
        //    }

        //    for (int i = 0; i < moduleParts.Length; i++)
        //    {
        //        if (!moduleParts[i].gameObject.activeSelf)
        //        {
        //            Destroy(moduleParts[i].gameObject);
        //        }
        //    }
        //}
    }
}
