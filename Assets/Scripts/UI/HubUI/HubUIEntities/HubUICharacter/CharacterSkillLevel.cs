using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public class CharacterSkillLevel
    {
        [SerializeField] SkillType _skillType;
        [SerializeField] int _skillLevel;


        public SkillType SkillType => _skillType;
        public int SkillLevel => _skillLevel;
    }
}
