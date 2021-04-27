using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "CharacterBonesTransformsData", menuName = "CreateData/HubUIData/CharacterBonesTransformsData", order = 0)]
    class CharacterBonesTransformsData : ScriptableObject
    {
        [SerializeField] private GameObject _modularCharacter;

        private Dictionary<string, string[]> _bones;
    }
}
