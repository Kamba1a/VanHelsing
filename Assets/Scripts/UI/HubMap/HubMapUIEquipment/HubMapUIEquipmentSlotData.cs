//using UnityEngine;


//namespace BeastHunter
//{
//    public class HubMapUIEquipmentSlotData : ScriptableObject
//    {
//        [Header("Slot type sprites")]
//        [SerializeField] Sprite ItemSlotIcon;
//        [SerializeField] Sprite WeaponSlotIcon;
//        [SerializeField] Sprite ArmLowerSlotIcon;
//        [SerializeField] Sprite ArmUpperSlotIcon;
//        [SerializeField] Sprite BackSlotIcon;
//        [SerializeField] Sprite ElbowSlotIcon;
//        [SerializeField] Sprite HandSlotIcon;
//        [SerializeField] Sprite HeadSlotIcon;
//        [SerializeField] Sprite HipsSlotIcon;
//        [SerializeField] Sprite KneeSlotIcon;
//        [SerializeField] Sprite LegSlotIcon;
//        [SerializeField] Sprite ShoulderSlotIcon;
//        [SerializeField] Sprite TorsoSlotIcon;

//        public Sprite GetEquipmentSlotSpriteByType(HubMapUIEquipmentType equipmentType)
//        {
//            switch (equipmentType)
//            {
//                case HubMapUIEquipmentType.None: return null;
//                case HubMapUIEquipmentType.Item: return ItemSlotIcon;
//                case HubMapUIEquipmentType.Weapon: return WeaponSlotIcon;
//                case HubMapUIEquipmentType.ArmLower: return ArmLowerSlotIcon;
//                case HubMapUIEquipmentType.ArmUpper: return ArmUpperSlotIcon;
//                case HubMapUIEquipmentType.Back: return BackSlotIcon;
//                case HubMapUIEquipmentType.Elbow: return ElbowSlotIcon;
//                case HubMapUIEquipmentType.Hand: return HandSlotIcon;
//                case HubMapUIEquipmentType.Head: return HeadSlotIcon;
//                case HubMapUIEquipmentType.Hips: return HipsSlotIcon;
//                case HubMapUIEquipmentType.Knee: return KneeSlotIcon;
//                case HubMapUIEquipmentType.Leg: return LegSlotIcon;
//                case HubMapUIEquipmentType.Shoulder: return ShoulderSlotIcon;
//                case HubMapUIEquipmentType.Torso: return TorsoSlotIcon;
//                default:
//                    Debug.LogError(this + ": incorrect equipment type");
//                    return null;
//            }
//        }
//    }
//}
