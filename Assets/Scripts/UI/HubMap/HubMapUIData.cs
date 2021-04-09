using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapUIData : ScriptableObject
    {
        #region Fields

        [Header("UI prefabs")]
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _locationTextUIPrefab;
        [SerializeField] private GameObject _characterUIPrefab;
        [SerializeField] private GameObject _equipmentSlotUIPrefab;
        [SerializeField] private GameObject _inventorySlotUIPrefab;
        [SerializeField] private GameObject _shopSlotUIPrefab;
        [SerializeField] private GameObject _answerButtonUIPrefab;
        [SerializeField] private GameObject _characters3DViewRenderingPrefab;

        [Header("UI settings")]
        [SerializeField] private bool _mapOnStartEnabled;
        [SerializeField] private Vector3 _characters3DViewRenderingObjectPosition;

        [Header("Objects on map")]
        [SerializeField] private HubMapUIMapObjectData[] _mapObjects;

        [Header("Game content for UI")]
        [SerializeField] private HubMapUIContextData _contextData;

        [Header("Equipment slot type sprites")]
        [SerializeField] Sprite WeaponSlotIcon;
        [SerializeField] Sprite ArmSlotIcon;
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

        #endregion


        #region Properties

        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject ShopSlotUIPrefab => _shopSlotUIPrefab;
        public GameObject LocationTextUIPrefab => _locationTextUIPrefab;
        public GameObject CharacterUIPrefab => _characterUIPrefab;
        public GameObject EquipmentSlotUIPrefab => _equipmentSlotUIPrefab;
        public GameObject InventorySlotUIPrefab => _inventorySlotUIPrefab;
        public GameObject AnswerButtonUIPrefab => _answerButtonUIPrefab;
        public GameObject Characters3DViewRenderingPrefab => _characters3DViewRenderingPrefab;
        public Vector3 Characters3DViewRenderingObjectPosition => _characters3DViewRenderingObjectPosition;

        public bool MapOnStartEnabled => _mapOnStartEnabled;

        public HubMapUIMapObjectData[] MapObjects => (HubMapUIMapObjectData[])_mapObjects.Clone();

        public HubMapUIContextData ContextData => _contextData;

        #endregion


        #region Methods

        public Sprite GetClothSlotSpriteByType(HubMapUIClothType clothType)
        {
            switch (clothType)
            {
                case HubMapUIClothType.Arm: return ArmSlotIcon;
                case HubMapUIClothType.Back: return BackSlotIcon;
                case HubMapUIClothType.Hand: return HandSlotIcon;
                case HubMapUIClothType.Head: return HeadSlotIcon;
                case HubMapUIClothType.Hips: return HipsSlotIcon;
                case HubMapUIClothType.Leg: return LegSlotIcon;
                case HubMapUIClothType.Shoulder: return ShoulderSlotIcon;
                case HubMapUIClothType.Torso: return TorsoSlotIcon;
                case HubMapUIClothType.Ring: return RingSlotIcon;
                case HubMapUIClothType.Amulet: return AmuletSlotIcon;
                case HubMapUIClothType.Belt: return BeltSlotIcon;
                default:
                    Debug.LogError(this + ": incorrect cloth type");
                    return null;
            }
        }

        #endregion
    }
}
