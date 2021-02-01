using Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;

        #endregion


        #region Fields

        private GameObject _currentInfoObject;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
        }

        #endregion


        #region Methods

        public void OnClick_HubButton()
        {
            _mainPanel.SetActive(false);
        }

        public void OnClick_MapButton()
        {
            _mainPanel.SetActive(true);
        }

        public void OnClick_CityButton(int cityId)
        {
            Destroy(_currentInfoObject);

            _currentInfoObject = GameObject.Instantiate(Data.HubMapData.CityInfoUIPrefab);
            _currentInfoObject.transform.SetParent(_infoPanel.transform.FindDeep("Viewport"), false);
            _currentInfoObject.transform.localScale = new Vector3(3, 1, 1);
            _infoPanel.GetComponentInChildren<ScrollRect>().content = _currentInfoObject.GetComponent<RectTransform>();

            _currentInfoObject.GetComponentInChildren<CityInfoPanelBehaviour>().Initialize(Data.HubMapData.Cities[cityId]);

            _infoPanel.SetActive(true);
        }

        public void OnClick_CloseInfoButton()
        {
            _infoPanel.SetActive(false);
            Destroy(_currentInfoObject);
        }

        #endregion
    }
}
