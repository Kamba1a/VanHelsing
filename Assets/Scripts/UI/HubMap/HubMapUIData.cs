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
    }
}
