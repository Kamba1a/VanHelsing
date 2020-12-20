﻿using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class PlayerHealthBarModel
    {
        #region Fields

        private PlayerSectionHealthBar[] _healthSections;

        #endregion


        #region ClassLifeCycle

        public PlayerHealthBarModel(GameObject prefab, PlayerHealthBarData data)
        {
            Transform healthBar = prefab.transform.GetChild(0);
            healthBar.GetComponent<HorizontalLayoutGroup>().spacing = data.DistanceBetweenSections;

            RectTransform rectTransform = healthBar.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector3(data.HealthBarSize.x, data.HealthBarSize.y);
            rectTransform.position = new Vector3(data.HealthBarPosition.x, data.HealthBarPosition.y, 0);

            int sectionsAmount = data.HealthSectionsPercentThresholds.Length;
            _healthSections = new PlayerSectionHealthBar[sectionsAmount];

            float previousHealthThreshold = 0;
            for (int i = 0; i < sectionsAmount; i++)
            {
                Transform newSection = GameObject.Instantiate(Data.PlayerHealthBarData.HealthSectionPrefab).transform;
                newSection.parent = healthBar;

                _healthSections[i] = new PlayerSectionHealthBar();
                _healthSections[i].UpdatingImage = newSection.GetChild(0).GetComponent<Image>();
                _healthSections[i].UpperHealthTreshold = data.HealthSectionsPercentThresholds[i];
                _healthSections[i].PercentSectionSize = _healthSections[i].UpperHealthTreshold - previousHealthThreshold;
                _healthSections[i].LowerHealthTreshold = _healthSections[i].UpperHealthTreshold - _healthSections[i].PercentSectionSize;

                float sectionScale = sectionsAmount / (100 / _healthSections[i].PercentSectionSize);
                newSection.localScale = new Vector3(sectionScale, 1, 1);

                previousHealthThreshold = _healthSections[i].UpperHealthTreshold;
            }
        }

        #endregion


        #region Methods

        /// <summary>Updates health bar sections filling</summary>
        /// <param name="currentHealthPercent">Current health percentage</param>
        /// <returns>Current maximum health threshold as a percentage</returns>
        public float HealthFillUpdate(float currentHealthPercent)
        {
            float currentHealthThreshold = 0;
            for (int i = 0; i < _healthSections.Length; i++)
            {
                if (currentHealthPercent <= _healthSections[i].UpperHealthTreshold && currentHealthPercent > _healthSections[i].LowerHealthTreshold)
                {
                    float healthPercentForSection = currentHealthPercent - _healthSections[i].LowerHealthTreshold;
                    _healthSections[i].UpdatingImage.fillAmount = healthPercentForSection / _healthSections[i].PercentSectionSize;
                    currentHealthThreshold = _healthSections[i].UpperHealthTreshold;
                }
                else if (currentHealthPercent < _healthSections[i].LowerHealthTreshold)
                {
                    _healthSections[i].UpdatingImage.fillAmount = 0;
                }
                else if (currentHealthPercent > _healthSections[i].UpperHealthTreshold)
                {
                    _healthSections[i].UpdatingImage.fillAmount = 1;
                }
            }
            return currentHealthThreshold;
        }

        #region PrivateData

        private class PlayerSectionHealthBar
        {
            public Image UpdatingImage;
            public float UpperHealthTreshold;
            public float LowerHealthTreshold;
            public float PercentSectionSize;
        }

        #endregion
    }

    #endregion
}
