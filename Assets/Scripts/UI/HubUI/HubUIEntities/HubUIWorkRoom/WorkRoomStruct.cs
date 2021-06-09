using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct WorkRoomStruct
    {
        [SerializeField] private BaseWorkRoomStruct<WorkRoomProgress> _baseWorkRoomStruct;
        [SerializeField] private ItemOrderData[] _orders;


        public BaseWorkRoomStruct<WorkRoomProgress> BaseWorkRoomStruct => _baseWorkRoomStruct;
        public ItemOrderData[] Orders => (ItemOrderData[])_orders?.Clone();
    }
}
