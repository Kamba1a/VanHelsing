using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct MapObjectStruct
    {
        [SerializeField] private int _mapIndex;
        [SerializeField] private MapObjectType _mapObjectType;
        [SerializeField] private bool _isBlockedAtStart;
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 10)] private string _description;
    }
}
