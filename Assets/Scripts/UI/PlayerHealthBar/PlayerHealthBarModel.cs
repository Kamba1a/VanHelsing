using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class PlayerHealthBarModel
    {
        #region Fields

        private HealthSection[] _healthSections;

        #endregion


        #region Properties

        public float CurrentHealthThreshold { get; private set; }

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
            _healthSections = new HealthSection[sectionsAmount];

            float previousHealthThreshold = 0;
            for (int i = 0; i < sectionsAmount; i++)
            {
                Transform newSection = GameObject.Instantiate(Data.PlayerHealthBarData.HealthSectionPrefab).transform;
                newSection.parent = healthBar;

                Image updatableImage = newSection.GetChild(0).GetComponent<Image>();
                float upperTresholdInPercent = data.HealthSectionsPercentThresholds[i];
                float sectionSizeInPercent = upperTresholdInPercent - previousHealthThreshold;
                _healthSections[i] = new HealthSection(updatableImage, upperTresholdInPercent, sectionSizeInPercent);

                float sectionScale = sectionsAmount / (100 / _healthSections[i].SectionSizeInPercent);
                newSection.localScale = new Vector3(sectionScale, 1, 1);

                previousHealthThreshold = upperTresholdInPercent;
            }

            CurrentHealthThreshold = 100;
        }

        #endregion


        #region Methods

        /// <summary>Updates health bar sections filling</summary>
        /// <param name="currentHealthPercent">Current health percentage</param>
        /// <returns>Current maximum health threshold as a percentage</returns>
        public void HealthFillUpdate(float currentHealthPercent)
        {
            float currentHealthPercentRound = (float)Math.Round(currentHealthPercent, 2);
            for (int i = 0; i < _healthSections.Length; i++)
            {
                if (currentHealthPercentRound <= _healthSections[i].UpperThresholdInPercent && currentHealthPercentRound > _healthSections[i].LowerThresholdInPercent)
                {
                    float healthPercentForSection = currentHealthPercentRound - _healthSections[i].LowerThresholdInPercent;
                    _healthSections[i].UpdatableImage.fillAmount = healthPercentForSection / _healthSections[i].SectionSizeInPercent;
                    CurrentHealthThreshold = _healthSections[i].UpperThresholdInPercent;
                }
                else if (currentHealthPercentRound <= _healthSections[i].LowerThresholdInPercent)
                {
                    _healthSections[i].UpdatableImage.fillAmount = 0;
                }
                else if (currentHealthPercentRound > _healthSections[i].UpperThresholdInPercent)
                {
                    _healthSections[i].UpdatableImage.fillAmount = 1;
                }
            }
        }

        #endregion


        #region PrivateData

        private class HealthSection
        {
            #region Properties

            public Image UpdatableImage { get; private set; }
            public float UpperThresholdInPercent { get; private set; }
            public float LowerThresholdInPercent { get; private set; }
            public float SectionSizeInPercent { get; private set; }

            #endregion


            #region ClassLifeCycle

            public HealthSection(Image updatableImage, float upperTresholdInPercent, float sectionSizeInPercent)
            {
                UpdatableImage = updatableImage;
                UpperThresholdInPercent = upperTresholdInPercent;
                SectionSizeInPercent = sectionSizeInPercent;
                LowerThresholdInPercent = upperTresholdInPercent - sectionSizeInPercent;
            }

            #endregion
        }

        #endregion
    }
}