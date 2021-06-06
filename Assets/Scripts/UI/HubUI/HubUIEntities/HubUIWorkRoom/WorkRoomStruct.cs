using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct WorkRoomStruct
    {
        [SerializeField] private BaseWorkRoomStruct<WorkRoomProgress> _baseWorkRoomStruct;
        [SerializeField] private ItemOrderStruct[] _orders;


        public BaseWorkRoomStruct<WorkRoomProgress> BaseWorkRoomStruct => _baseWorkRoomStruct;
        public ItemOrderStruct[] Orders => (ItemOrderStruct[])_orders?.Clone();
    }
}
