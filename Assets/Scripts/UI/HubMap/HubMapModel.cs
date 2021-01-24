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

        #endregion


        #region ClassLifeCycle

        public HubMapModel(GameObject objectOnScene, HubMapData hubMapData)
        {
            _objectOnScene = objectOnScene;
            _hubMapData = hubMapData;

            _hubMapBehaviour = _objectOnScene.GetComponent<HubMapBehaviour>();
            _hubMapBehaviour.HubButton_OnClickHandler = HubButton_OnClick;
            _hubMapBehaviour.MapButton_OnClickHandler = MapButton_OnClick;

            _mainPanel = objectOnScene.transform.FindDeep(_hubMapData.MainPanelName).gameObject;
            _mainPanel.SetActive(_hubMapData.MapOnStartEnabled);
        }

        #endregion


        #region Methods

        private void HubButton_OnClick() => _hubMapData.HubButton_OnClick(_mainPanel);
        private void MapButton_OnClick() => _hubMapData.MapButton_OnClick(_mainPanel);

        #endregion
    }
}
