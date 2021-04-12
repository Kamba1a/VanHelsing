using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUICharacterClothesModuleParts
    {
        [SerializeField] HubMapUIClothesType _clothesType;
        [SerializeField] List<string> _modulePartsNames;


        public HubMapUIClothesType ClothesType => _clothesType;
        public List<string> ModulePartsNames => _modulePartsNames;
    }
}
