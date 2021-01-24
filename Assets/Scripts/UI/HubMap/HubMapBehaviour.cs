using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] GameObject _mainPanel;
        [SerializeField] GameObject _mapButton;

        #endregion


        #region Fields

        //fields

        #endregion


        #region Properties

        public Action<GameObject, GameObject> OnHubButton_OnClickHandler { get; set; }

        #endregion


        #region UnityMethods

        void Start()
        {
            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            Text text = _mapButton.GetComponentInChildren<Text>();
            text.text = Data.HubMapData.MapOnStartEnabled ? "Хаб" : "Карта";
        }

        #endregion


        #region Methods

        public void HubButton_OnClick(GameObject buttonText) => OnHubButton_OnClickHandler?.Invoke(buttonText, _mainPanel);

        #endregion
    }
}
