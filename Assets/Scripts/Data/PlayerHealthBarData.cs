using System;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "PlayerHealthBarData")]
    public class PlayerHealthBarData: ScriptableObject
    {
        #region SerializedFields

        [SerializeField] private GameObject _playerHealthBarPrefab;
        [SerializeField] private GameObject _playerHealthSectionPrefab;
        [Tooltip("Specify the number of sections in the health bar and set the maximum health threshold in percent for each section")]
        [Range(1, 100)]
        [SerializeField] private float[] _healthSectionsPercentThresholds;

        #endregion


        #region Properties

        public GameObject PlayerHealthBarPrefab => _playerHealthBarPrefab;
        public GameObject PlayerHealthSectionPrefab => _playerHealthSectionPrefab;
        public float[] HealthSectionsPercentThresholds { get; private set; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            HealthSectionsPercentThresholds = (float[])_healthSectionsPercentThresholds.Clone();
            Array.Sort(HealthSectionsPercentThresholds);
            for (int i = 1; i < HealthSectionsPercentThresholds.Length; i++)
            {
                if (HealthSectionsPercentThresholds[i] == HealthSectionsPercentThresholds[i - 1])
                {
                    Debug.LogError(this + ": health sections thresholds do not have to match!");
                }
            }
        }

        #endregion
    }
}
