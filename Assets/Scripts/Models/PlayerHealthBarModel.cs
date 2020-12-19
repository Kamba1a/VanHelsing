using UnityEngine;
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
            int sectionsAmount = data.HealthSectionsPercentThresholds.Length;
            _healthSections = new PlayerSectionHealthBar[sectionsAmount];

            float previousHealthThreshold = 0;
            for (int i = 0; i < sectionsAmount; i++)
            {
                Transform newSection = GameObject.Instantiate(Data.PlayerHealthBarData.PlayerHealthSectionPrefab).transform;
                newSection.parent = prefab.transform.Find("Panel");

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

        public void Update(float currentHealthPercent)
        {
            for (int i = 0; i < _healthSections.Length; i++)
            {
                if (currentHealthPercent < _healthSections[i].UpperHealthTreshold && currentHealthPercent > _healthSections[i].LowerHealthTreshold)
                {
                    float healthPercentForSection = currentHealthPercent - _healthSections[i].LowerHealthTreshold;
                    _healthSections[i].UpdatingImage.fillAmount = healthPercentForSection / _healthSections[i].PercentSectionSize;
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
