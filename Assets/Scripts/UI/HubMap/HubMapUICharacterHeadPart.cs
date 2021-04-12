﻿using System;
using UnityEngine;


namespace BeastHunter
{
    [Serializable]
    public class HubMapUICharacterHeadPart
    {
        [SerializeField] HubMapUICharacterHeadParts _type;
        [SerializeField] string _name;
        [SerializeField] bool _isActivateByDefault;


        public HubMapUICharacterHeadParts Type => _type;
        public string Name => _name;
        public bool IsActivateByDefault => _isActivateByDefault;
    }
}
