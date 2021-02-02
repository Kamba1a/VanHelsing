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

        [Header("Hub map")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;

        [Header("City info panel")]
        [SerializeField] private GameObject _cityInfoPanel;
        [SerializeField] private GameObject _cityFraction;
        [SerializeField] private GameObject _cityName;
        [SerializeField] private GameObject _cityDescription;
        [SerializeField] private GameObject _citizenPanel;
        [SerializeField] private GameObject _cityReputation;
        [SerializeField] private GameObject _citySellingPanel;

        [Header("Quest panel")]
        [SerializeField] private GameObject _questPanel;
        [SerializeField] private GameObject _questGiverPortrait;
        [SerializeField] private GameObject _questGiverName;
        [SerializeField] private GameObject _questDescription;
        [SerializeField] private GameObject _questApplyButton;
        [SerializeField] private GameObject _questDeclineButton;

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
            _questPanel.SetActive(false);
        }

        #endregion


        #region TriggerEvents

        public void OnClick_HubButton() => HideUI();
        public void OnClick_MapButton() => ShowUI();
        public void OnClick_CityButton(int cityId) => ShowCityInfoPanel(cityId);
        public void OnClick_CloseInfoButton() => HideInfoPanel();

        #endregion


        #region Methods

        private void ShowUI()
        {
            _mainPanel.SetActive(true);
        }

        private void HideUI()
        {
            _mainPanel.SetActive(false);
        }

        private void ShowCityInfoPanel(int cityId)
        {
            ClearInfoPanel();
            FillCityInfo(Data.HubMapData.Cities[cityId]);
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
        }

        private void HideInfoPanel()
        {
            _cityInfoPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }

        private void FillCityInfo(ICityInfo city)
        {
            _cityFraction.GetComponent<Image>().sprite = city.Fraction;
            _cityName.GetComponent<Text>().text = city.Name;
            _cityDescription.GetComponent<Text>().text = city.Description;
            _cityReputation.GetComponent<Text>().text = city.Reputation.ToString();

            for (int i = 0; i < city.Citizens.Count; i++)
            {
                GameObject citizen = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab);
                _clearInfoPanelList.Add(citizen);
                citizen.transform.SetParent(_citizenPanel.transform, false);
                citizen.transform.localScale = new Vector3(1, 1, 1);
                citizen.GetComponentInChildren<CitizenUIBehaviour>().Initialize(city.Citizens[i]);
                citizen.GetComponentInChildren<CitizenUIBehaviour>().OnClick_CitizenButtonHandler = ShowQuestPanel;
            }

            for (int i = 0; i < city.SellingItems.Count; i++)
            {
                GameObject item = GameObject.Instantiate(Data.HubMapData.SellingItemUIPrefab);
                _clearInfoPanelList.Add(item);
                item.transform.SetParent(_citySellingPanel.transform, false);
                item.transform.localScale = new Vector3(1, 1, 1);
                item.GetComponentInChildren<SellingItemUIBehaviour>().Initialize(city.SellingItems[i]);
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

        private void ShowQuestPanel(int citizenId)
        {
            FillQuestPanel(citizenId);
            _questPanel.SetActive(true);
        }

        private void FillQuestPanel(int citizenId)
        {
            //fill window
        }

        #endregion
    }
}
