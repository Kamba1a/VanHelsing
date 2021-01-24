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

        #endregion


        #region Properties

        public GameObject MainPanel { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapModel(GameObject objectOnScene, HubMapData hubMapData)
        {
            _objectOnScene = objectOnScene;
            _hubMapData = hubMapData;

            _hubMapBehaviour = _objectOnScene.GetComponent<HubMapBehaviour>();
            _hubMapBehaviour.HubButton_OnClickHandler = HubButton_OnClick;

            MainPanel = objectOnScene.transform.FindDeep(_hubMapData.MainPanelName).gameObject;
            MainPanel.SetActive(_hubMapData.MapOnStartEnabled);
        }

        #endregion


        #region Methods

        private void HubButton_OnClick(GameObject button) => _hubMapData.HubButton_OnClick(button, this);

        #endregion
    }
}
