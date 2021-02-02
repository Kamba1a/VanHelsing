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

        [Header("TEMPORARY FOR DEBUG")] //TEMPORARY FIELDS FOR TESTING HUB MAP
        [SerializeField] public TemporarySellingItemModel[] Items;
        [SerializeField] public TemporaryDialogModel[] Dialogs;
        [SerializeField] public TemporaryCitizenInfoModel[] Citizens;
        [SerializeField] public TemporaryCityInfoModel[] Cities;

        #endregion


        #region Properties

        public GameObject HubMapUIPrefab => _hubMapUIPrefab;
        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;

        #endregion
    }
}
