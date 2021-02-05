using System;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private GameObject _hubMapUIPrefab;
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _sellingItemUIPrefab;
        [SerializeField] private bool _mapOnStartEnabled;

        #endregion


        # region TEMPORARY DATA FOR TESTING HUB MAP

        [Header("TEMPORARY FOR DEBUG")]
        [SerializeField] private TemporarySellingItemModel[] _items;
        [SerializeField] private TemporaryDialogModel[] _dialogs;
        [SerializeField] private TemporaryCitizenInfoModel[] _citizens;
        [SerializeField] private TemporaryCityInfoModel[] _cities;
        public TemporarySellingItemModel[] Items => _items;
        public TemporaryDialogModel[] Dialogs => _dialogs;
        public TemporaryCitizenInfoModel[] Citizens => _citizens;
        public TemporaryCityInfoModel[] Cities => _cities;

        #endregion


        #region Properties

        public GameObject HubMapUIPrefab => _hubMapUIPrefab;
        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;

        #endregion
    }
}
