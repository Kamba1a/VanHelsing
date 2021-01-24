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
        HubMapBehaviour _hubMapBehaviour;
        bool _mainPanelSwitcher;

        #endregion


        #region ClassLifeCycle

        public HubMapModel(GameObject objectOnScene, HubMapData hubMapData)
        {
            _objectOnScene = objectOnScene;
            _hubMapData = hubMapData;

            _hubMapBehaviour = _objectOnScene.GetComponent<HubMapBehaviour>();
            _hubMapBehaviour.OnHubButton_OnClickHandler = OnHubButton_OnClick;

            _mainPanelSwitcher = _hubMapData.MapOnStartEnabled;
        }

        #endregion


        #region Methods

        private void OnHubButton_OnClick(GameObject buttonText, GameObject mainPanel)
        {
            if (_mainPanelSwitcher)
            {
                mainPanel.SetActive(false);
                buttonText.GetComponent<Text>().text = "Карта";
            }
            else
            {
                mainPanel.SetActive(true);
                buttonText.GetComponent<Text>().text = "Хаб";
            }
            _mainPanelSwitcher = !_mainPanelSwitcher;
        }

        #endregion
    }
}
