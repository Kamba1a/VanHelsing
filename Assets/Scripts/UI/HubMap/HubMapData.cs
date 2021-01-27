using System;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private GameObject _cityPanelPrefab;
        [SerializeField] private GameObject _citizenPrefab;
        [SerializeField] private bool _mapOnStartEnabled;
        [SerializeField] private string _mainPanelName;
        [SerializeField] private string _infoPanelName;

        #endregion


        #region Properties

        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public string MainPanelName => _mainPanelName;
        public string InfoPanelName => _infoPanelName;

        #endregion


        #region Methods

        public void Updating()
        {
            
        }

        public void HubButton_OnClick(GameObject mainPanel)
        {
            mainPanel.SetActive(false);
        }

        public void MapButton_OnClick(GameObject mainPanel)
        {
            mainPanel.SetActive(true);
        }

        public void CityButton_OnClick(GameObject infoPanel, ref GameObject currentInfoObject)
        {
            Destroy(currentInfoObject);
            currentInfoObject = GameObject.Instantiate(_cityPanelPrefab);
            currentInfoObject.transform.SetParent(infoPanel.transform, false);
            currentInfoObject.transform.localScale = new Vector3(3, 1, 1);
            infoPanel.SetActive(true);
        }

        public void CloseInfoButton_OnClick(GameObject infoPanel, GameObject currentInfoObject)
        {
            Debug.Log(this+"CloseInfoButton_OnClick");
            infoPanel.SetActive(false);
            Destroy(currentInfoObject);
        }

        #endregion
    }
}
