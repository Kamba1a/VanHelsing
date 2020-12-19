using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "PlayerHealthBarData")]
    public class PlayerHealthBarData: ScriptableObject
    {
        [SerializeField] private GameObject _playerHealthBarPrefab;
        [SerializeField] private GameObject _playerHealthSectionPrefab;
        [Range(1, 100)]
        [SerializeField] private int _sectionAmount;
        [SerializeField] private PlayerSectionHealthBar[] _healthSections;


        public GameObject PlayerHealthBarPrefab => _playerHealthBarPrefab;
        public GameObject PlayerHealthSectionPrefab => _playerHealthSectionPrefab;
        public int SectionAmount => _sectionAmount;
        public PlayerSectionHealthBar[] HealthSections => _healthSections;

        private void OnEnable()
        {
            float previousHealthThreshold = 0;
            for (int i = 0; i < _healthSections.Length; i++)
            {
                _healthSections[i].PercentSectionSize = _healthSections[i].PercentHealthThreshold - previousHealthThreshold;
                _healthSections[i].Scale = _healthSections.Length / (100 / _healthSections[i].PercentSectionSize);
                previousHealthThreshold = _healthSections[i].PercentHealthThreshold;
            }
        }

        [Serializable]
        public class PlayerSectionHealthBar
        {
            [NonSerialized]
            public Image Image;
            [Range(1, 100)]
            public float PercentHealthThreshold;
            [NonSerialized]
            public float PercentSectionSize;
            [NonSerialized]
            public float Scale;
        }
    }
}
