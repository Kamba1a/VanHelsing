using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    [Serializable]
    public struct RandomCharacterSettingsStruct
    {
        [SerializeField] string[] _names;
        [SerializeField] private GameObject _defaultView3DModelPrefab;
        [SerializeField] private RuntimeAnimatorController _view3DModelAnimatorController;
    }
}
