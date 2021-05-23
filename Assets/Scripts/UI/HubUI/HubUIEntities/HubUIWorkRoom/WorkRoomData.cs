using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "WorkRoom", menuName = "CreateData/HubUIData/WorkRoom", order = 0)]
    public class WorkRoomData : ScriptableObject
    {
        [SerializeField] private WorkRoomStruct _workRoomStruct;


        public WorkRoomStruct WorkRoomStruct => _workRoomStruct;
    }
}
