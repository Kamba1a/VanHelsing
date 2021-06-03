using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct WorkRoomStruct
    {
        [SerializeField] private BaseWorkRoomStruct<WorkRoomProgress> _baseWorkRoomStruct;
        [SerializeField] private ItemOrderModel[] orders;


        public BaseWorkRoomStruct<WorkRoomProgress> BaseWorkRoomStruct => _baseWorkRoomStruct;
    }
}
