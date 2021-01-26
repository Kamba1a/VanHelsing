using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region Properties

        public Action HubButton_OnClickHandler { get; set; }
        public Action MapButton_OnClickHandler { get; set; }
        public Action CityButton_OnClickHandler { get; set; }
        public Action CloseInfoButton_OnClickHandler { get; set; }

        #endregion


        #region Methods

        public void HubButton_OnClick() => HubButton_OnClickHandler?.Invoke();
        public void MapButton_OnClick() => MapButton_OnClickHandler?.Invoke();
        public void CityButton_OnClick() => CityButton_OnClickHandler?.Invoke();
        public void CloseInfoButton_OnClick() => CloseInfoButton_OnClickHandler?.Invoke();

        #endregion
    }
}
