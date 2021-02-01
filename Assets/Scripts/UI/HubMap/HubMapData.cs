using Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private GameObject _hubMapUIPrefab;
        [SerializeField] private GameObject _cityInfoUIPrefab;
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private bool _mapOnStartEnabled;

        #endregion


        #region Properties

        public GameObject HubMapUIPrefab => _hubMapUIPrefab;
        public GameObject CityInfoUIPrefab => _cityInfoUIPrefab;
        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;

        #endregion
    }
}
