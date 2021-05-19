using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct WorkRoomStruct
    {
        [SerializeField] private BaseWorkRoomStruct<WorkRoomProgress> _baseWorkRoomStruct;


        public BaseWorkRoomStruct<WorkRoomProgress> BaseWorkRoomStruct => _baseWorkRoomStruct;
    }
}
