using System;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System.Collections.Generic;

namespace BeastHunter
{
    public class HubMapModel
    {
        #region Fields

        private GameObject _objectOnScene;
        private HubMapData _hubMapData;
        private HubMapBehaviour _hubMapBehaviour;
        private GameObject _mainPanel;

        #endregion


        #region Properties

        public GameObject InfoPanel { get; set; }
        public GameObject CurrentInfoObject { get; set; }
        public TemporaryDataForHubMap TempData { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapModel(GameObject objectOnScene, HubMapData hubMapData)
        {
            _objectOnScene = objectOnScene;
            _hubMapData = hubMapData;

            _hubMapBehaviour = _objectOnScene.GetComponent<HubMapBehaviour>();
            _hubMapBehaviour.HubButton_OnClickHandler = HubButton_OnClick;
            _hubMapBehaviour.MapButton_OnClickHandler = MapButton_OnClick;
            _hubMapBehaviour.CityButton_OnClickHandler = CityButton_OnClick;
            _hubMapBehaviour.CloseInfoButton_OnClickHandler = CloseInfoButton_OnClick;

            _mainPanel = objectOnScene.transform.FindDeep(_hubMapData.MainPanelName).gameObject;
            _mainPanel.SetActive(_hubMapData.MapOnStartEnabled);

            InfoPanel = objectOnScene.transform.FindDeep(_hubMapData.InfoPanelName).gameObject;
            InfoPanel.SetActive(false);

            TempData = new TemporaryDataForHubMap();
        }

        #endregion


        #region Methods

        private void HubButton_OnClick() => _hubMapData.HubButton_OnClick(_mainPanel);
        private void MapButton_OnClick() => _hubMapData.MapButton_OnClick(_mainPanel);
        private void CityButton_OnClick(string cityId) => _hubMapData.CityButton_OnClick(cityId, this);
        private void CloseInfoButton_OnClick() => _hubMapData.CloseInfoButton_OnClick(InfoPanel, CurrentInfoObject);

        #endregion
    }
}
