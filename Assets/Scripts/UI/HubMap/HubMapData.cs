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
        private TemporaryCitizenInfoModel[] _citizensCopy;

        public TemporarySellingItemModel[] Items => _items;
        public TemporaryDialogModel[] Dialogs => _dialogs;
        public TemporaryCitizenInfoModel[] Citizens => _citizensCopy;
        public TemporaryCityInfoModel[] Cities => _cities;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _citizensCopy = new TemporaryCitizenInfoModel[_citizens.Length];
            for (int i = 0; i < _citizensCopy.Length; i++)
            {
                _citizensCopy[i] = new TemporaryCitizenInfoModel(_citizens[i]);
            }
        }

        #endregion


        #region Properties

        public GameObject HubMapUIPrefab => _hubMapUIPrefab;
        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;

        #endregion
    }
}
