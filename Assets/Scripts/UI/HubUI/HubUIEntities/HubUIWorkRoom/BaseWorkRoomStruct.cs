using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct BaseWorkRoomStruct<T> where T : BaseWorkRoomProgress
    {
        #region Fields

        [SerializeField] private WorkRoomType _roomType;
        [SerializeField] private int _level;
        [SerializeField] private CharacterModel _chiefWorker;
        [SerializeField] private List<CharacterModel> _assistants;
        [SerializeField] private List<WorkRoomProgressLevel<T>> _progressScheme;

        #endregion


        #region Properties

        public WorkRoomType RoomType => _roomType;
        public int Level => _level;
        public CharacterModel ChiefWorker => _chiefWorker;
        public List<CharacterModel> Assistants => _assistants;
        public List<WorkRoomProgressLevel<T>> ProgressScheme => _progressScheme;

        #endregion
    }
}
