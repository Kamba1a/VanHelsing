using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIEquipmentData", menuName = "CreateData/HubMapUIData/HubMapUIEquipmentData", order = 0)]
    public class HubMapUIEquipmentData : ScriptableObject
    {
        [SerializeField] private List<HubMapUIEquipmentType> _equipmentSlots; //cloth and accessories only
        //[SerializeField] private List<HubMapUIEquipmentType> _weaponSlots; //todo
        [Header("Slot type sprites")]
        [SerializeField] Sprite WeaponSlotIcon;
        [SerializeField] Sprite ArmLowerSlotIcon;
        [SerializeField] Sprite ArmUpperSlotIcon;
        [SerializeField] Sprite BackSlotIcon;
        [SerializeField] Sprite ElbowSlotIcon;
        [SerializeField] Sprite HandSlotIcon;
        [SerializeField] Sprite HeadSlotIcon;
        [SerializeField] Sprite HipsSlotIcon;
        [SerializeField] Sprite KneeSlotIcon;
        [SerializeField] Sprite LegSlotIcon;
        [SerializeField] Sprite ShoulderSlotIcon;
        [SerializeField] Sprite TorsoSlotIcon;
        [SerializeField] Sprite RingSlotIcon;
        [SerializeField] Sprite AmuletSlotIcon;
        [SerializeField] Sprite BeltSlotIcon;
        [SerializeField] Sprite PocketItemSlotIcon;


        public List<HubMapUIEquipmentType> EquipmentSlots => _equipmentSlots;


        public Sprite GetEquipmentSlotSpriteByType(HubMapUIEquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case HubMapUIEquipmentType.None: return null;
                case HubMapUIEquipmentType.Weapon: return WeaponSlotIcon;
                case HubMapUIEquipmentType.ArmLower: return ArmLowerSlotIcon;
                case HubMapUIEquipmentType.ArmUpper: return ArmUpperSlotIcon;
                case HubMapUIEquipmentType.Back: return BackSlotIcon;
                case HubMapUIEquipmentType.Elbow: return ElbowSlotIcon;
                case HubMapUIEquipmentType.Hand: return HandSlotIcon;
                case HubMapUIEquipmentType.Head: return HeadSlotIcon;
                case HubMapUIEquipmentType.Hips: return HipsSlotIcon;
                case HubMapUIEquipmentType.Knee: return KneeSlotIcon;
                case HubMapUIEquipmentType.Leg: return LegSlotIcon;
                case HubMapUIEquipmentType.Shoulder: return ShoulderSlotIcon;
                case HubMapUIEquipmentType.Torso: return TorsoSlotIcon;
                case HubMapUIEquipmentType.Ring: return RingSlotIcon;
                case HubMapUIEquipmentType.Amulet: return AmuletSlotIcon;
                case HubMapUIEquipmentType.Belt: return BeltSlotIcon;
                case HubMapUIEquipmentType.PocketItem: return PocketItemSlotIcon;
                default:
                    Debug.LogError(this + ": incorrect equipment type");
                    return null;
            }
        }
    }
}
