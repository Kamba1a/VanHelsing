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
            _hubMapBehaviour.OnClick_HubButtonHandler = OnClick_HubButton;
            _hubMapBehaviour.OnClick_MapButtonHandler = OnClick_MapButton;
            _hubMapBehaviour.OnClick_CityButtonHandler = OnClick_CityButton;
            _hubMapBehaviour.OnClick_CloseInfoButtonHandler = OnClick_CloseInfoButton;

            _mainPanel = objectOnScene.transform.FindDeep(_hubMapData.MainPanelName).gameObject;
            _mainPanel.SetActive(_hubMapData.MapOnStartEnabled);

            InfoPanel = objectOnScene.transform.FindDeep(_hubMapData.InfoPanelName).gameObject;
            InfoPanel.SetActive(false);

            TempData = new TemporaryDataForHubMap();
        }

        #endregion


        #region Methods

        private void OnClick_HubButton() => _hubMapData.OnClick_HubButton(_mainPanel);
        private void OnClick_MapButton() => _hubMapData.OnClick_MapButton(_mainPanel);
        private void OnClick_CityButton(string cityId) => _hubMapData.OnClick_CityButton(cityId, this);
        private void OnClick_CloseInfoButton() => _hubMapData.OnClick_CloseInfoButton(InfoPanel, CurrentInfoObject);

        #endregion
    }
}
