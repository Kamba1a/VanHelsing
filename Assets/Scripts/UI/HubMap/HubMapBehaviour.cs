using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region Properties

        public Action OnClick_HubButtonHandler { get; set; }
        public Action OnClick_MapButtonHandler { get; set; }
        public Action<string> OnClick_CityButtonHandler { get; set; }
        public Action OnClick_CloseInfoButtonHandler { get; set; }

        #endregion


        #region Methods

        public void OnClick_HubButton() => OnClick_HubButtonHandler?.Invoke();
        public void OnClick_MapButton() => OnClick_MapButtonHandler?.Invoke();
        public void OnClick_CityButton(string cityId) => OnClick_CityButtonHandler?.Invoke(cityId);
        public void OnClick_CloseInfoButton() => OnClick_CloseInfoButtonHandler?.Invoke();

        #endregion
    }
}
