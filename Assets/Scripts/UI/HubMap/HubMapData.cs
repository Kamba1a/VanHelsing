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

        [Header("TEMPORARY FOR DEBUG")]
        [SerializeField] public TemporaryCityInfoModel[] Cities;    //TEMPORARY FIELD FOR TESTING HUB MAP

        #endregion


        #region Properties

        public GameObject HubMapUIPrefab => _hubMapUIPrefab;
        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;

        #endregion
    }
}
