using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region Properties

        public Action<GameObject> HubButton_OnClickHandler { get; set; }

        #endregion


        //#region UnityMethods

        //private void Start()
        //{
        //    _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
        //    _mapButtonText = _mapButton.GetComponentInChildren<Text>();
        //    _mapButtonText.text = Data.HubMapData.MapOnStartEnabled ? "Хаб" : "Карта";
        //}

        //#endregion


        #region Methods

        public void HubButton_OnClick(GameObject button)
        {
            HubButton_OnClickHandler?.Invoke(button);
        }

        #endregion
    }
}
