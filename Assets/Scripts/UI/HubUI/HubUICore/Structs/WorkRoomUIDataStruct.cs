using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct WorkRoomUIDataStruct
    {
        [SerializeField] private GameObject _workRoomButtonPrefab;


        public GameObject WorkRoomButtonPrefab => _workRoomButtonPrefab;
    }
}
