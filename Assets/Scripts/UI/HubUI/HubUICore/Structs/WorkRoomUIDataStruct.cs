using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct WorkRoomUIDataStruct
    {
        [SerializeField] private GameObject _workRoomButtonPrefab;
        [SerializeField] private GameObject _workerSlotPrefab;
        [SerializeField] private GameObject _orderSlotPrefab;
        [SerializeField] private GameObject _makedItemSlotPrefab;


        public GameObject WorkRoomButtonPrefab => _workRoomButtonPrefab;
        public GameObject WorkerSlotPrefab => _workerSlotPrefab;
        public GameObject OrderSlotPrefab => _orderSlotPrefab;
        public GameObject MakedItemSlotPrefab => _makedItemSlotPrefab;
    }
}
