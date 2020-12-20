using System;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "PlayerHealthBarData")]
    public class PlayerHealthBarData: ScriptableObject
    {
        #region SerializedFields

        [Header("Player health bar")]
        [SerializeField] private GameObject _healthBarPrefab;
        [SerializeField] private Vector2 _healthBarPosition;
        [SerializeField] private Vector2 _healthBarSize;
        [SerializeField] private string _healthSectionsPanelName;

        [Header("Player health bar sections")]
        [SerializeField] private GameObject _healthSectionPrefab;
        [SerializeField] private float _distanceBetweenSections;
        [Tooltip("Specify the number of sections in the health bar and set the maximum health threshold in percent for each section")]
        [Range(1, 100)]
        [SerializeField] private float[] _healthSectionsPercentThresholds;

        #endregion


        #region Properties

        public GameObject HealthBarPrefab => _healthBarPrefab;
        public Vector2 HealthBarPosition => _healthBarPosition;
        public Vector2 HealthBarSize => _healthBarSize;
        public string HealthSectionsPanelName => _healthSectionsPanelName;
        public GameObject HealthSectionPrefab => _healthSectionPrefab;
        public float DistanceBetweenSections => _distanceBetweenSections;
        public float[] HealthSectionsPercentThresholds { get; private set; }

        #endregion


        #region ClassLifeCycle

        public PlayerHealthBarData()
        {
            _healthBarPosition = new Vector2(30.0f, 30.0f);
            _healthBarSize = new Vector2(700.0f, 30.0f);
            _healthSectionsPanelName = "HealthSectionsPanel";

            _distanceBetweenSections = 30.0f;
            _healthSectionsPercentThresholds = new float[4];
            _healthSectionsPercentThresholds[0] = 25.0f;
            _healthSectionsPercentThresholds[1] = 50.0f;
            _healthSectionsPercentThresholds[2] = 75.0f;
            _healthSectionsPercentThresholds[3] = 100.0f;
        }

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
