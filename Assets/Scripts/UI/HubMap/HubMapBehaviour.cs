using Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;

        [Header("City info panel")]
        [SerializeField] private GameObject _cityInfoPanel;
        [SerializeField] private GameObject _cityName;
        [SerializeField] private GameObject _cityDescription;
        [SerializeField] private GameObject _citizenPanel;

        #endregion


        #region Fields

        private List<GameObject> _clearInfoPanelList;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _clearInfoPanelList = new List<GameObject>();
            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
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
            ClearInfoPanel();
            FillCityInfo(Data.HubMapData.Cities[cityId]);
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
        }

        public void OnClick_CloseInfoButton()
        {
            _cityInfoPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }

        private void FillCityInfo(ICityInfo city)
        {
            _cityName.GetComponent<Text>().text = city.Name;
            _cityDescription.GetComponent<Text>().text = city.Description;

            for (int i = 0; i < city.Citizens.Count; i++)
            {
                GameObject citizen = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab);
                _clearInfoPanelList.Add(citizen);
                citizen.transform.SetParent(_citizenPanel.transform, false);
                citizen.transform.localScale = new Vector3(1, 1, 1);
                citizen.GetComponentInChildren<CitizenInfoBehaviour>().Initialize(city.Citizens[i]);
            }
        }

        private void ClearInfoPanel()
        {
            for (int i=0; i< _clearInfoPanelList.Count; i++)
            {
                Destroy(_clearInfoPanelList[i]);
            }
            _clearInfoPanelList.Clear();
        }

        #endregion
    }
}
