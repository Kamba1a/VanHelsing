using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseWorkRoomData <T> : ScriptableObject where T : BaseWorkRoomProgress
    {
        [SerializeField] private BaseWorkRoomStruct<T> _baseWorkRoomStruct;


        public BaseWorkRoomStruct<T> BaseWorkRoomStruct => _baseWorkRoomStruct;
    }
}
