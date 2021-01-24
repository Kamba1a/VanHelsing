using System;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

namespace BeastHunter
{
    public class HubMapModel
    {
        #region Fields

        private GameObject _objectOnScene;
        private HubMapData _hubMapData;
        private HubMapBehaviour _hubMapBehaviour;
        private GameObject _mainPanel;
        private GameObject _infoPanel;

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

            _mainPanel = objectOnScene.transform.FindDeep(_hubMapData.MainPanelName).gameObject;
            _mainPanel.SetActive(_hubMapData.MapOnStartEnabled);

            _infoPanel = objectOnScene.transform.FindDeep(_hubMapData.InfoPanelName).gameObject;
            _infoPanel.SetActive(false);
        }

        #endregion


        #region Methods

        private void HubButton_OnClick() => _hubMapData.HubButton_OnClick(_mainPanel);
        private void MapButton_OnClick() => _hubMapData.MapButton_OnClick(_mainPanel);
        private void CityButton_OnClick() => _hubMapData.CityButton_OnClick(_infoPanel);

        #endregion
    }
}
