using Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [Header("Hub map")]
        [SerializeField] private bool _mapOnStartEnabled;
        [SerializeField] private string _mainPanelName;
        [SerializeField] private string _infoPanelName;

        [Header("City panel")]
        [SerializeField] private GameObject _cityPanelPrefab;
        [SerializeField] private string _cityNamePanelName;
        [SerializeField] private string _cityDescriptionPanelName;
        [SerializeField] private string _citizenPanelName;

        [Header("Citizen panel")]
        [SerializeField] private GameObject _citizenPrefab;
        [SerializeField] private string _citizenNamePanelName;
        [SerializeField] private string _exclamationImgName;

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

        public void CityButton_OnClick(string cityId, HubMapModel model)
        {
            Destroy(model.CurrentInfoObject);

            model.CurrentInfoObject = GameObject.Instantiate(_cityPanelPrefab);
            model.CurrentInfoObject.transform.SetParent(model.InfoPanel.transform, false);
            model.CurrentInfoObject.transform.localScale = new Vector3(3, 1, 1);

            FillCityPrefab(cityId, model);

            model.InfoPanel.SetActive(true);
        }

        public void CloseInfoButton_OnClick(GameObject infoPanel, GameObject currentInfoObject)
        {
            Debug.Log(this+"CloseInfoButton_OnClick");
            infoPanel.SetActive(false);
            Destroy(currentInfoObject);
        }

        private void FillCityPrefab(string cityId, HubMapModel model)
        {
            model.CurrentInfoObject.transform.FindDeep(_cityNamePanelName).gameObject.GetComponent<Text>().text = model.TempData.CitiesDic[cityId].Name;
            model.CurrentInfoObject.transform.FindDeep(_cityDescriptionPanelName).gameObject.GetComponent<Text>().text = model.TempData.CitiesDic[cityId].Description;

            Transform citizenPanelTransform = model.CurrentInfoObject.transform.FindDeep(_citizenPanelName);
            for (int i = 0; i < model.TempData.CitiesDic[cityId].QuestGivers.Count; i++)
            {
                Transform citizen = GameObject.Instantiate(_citizenPrefab).transform;
                citizen.SetParent(citizenPanelTransform, false);
                citizen.localScale = new Vector3(1, 1, 1);
                citizen.FindDeep(_citizenNamePanelName).GetComponent<Text>().text = model.TempData.CitiesDic[cityId].QuestGivers[i].Name;
                if (model.TempData.CitiesDic[cityId].QuestGivers[i].IsHaveQuest)
                {
                    citizen.FindDeep(_exclamationImgName).gameObject.SetActive(true);
                    citizen.gameObject.GetComponent<Button>().onClick.AddListener(CitizenButton_onClick);
                }
            }
        }

        private void CitizenButton_onClick()
        {
            Debug.Log("Open dialog window");
        }

        #endregion
    }
}
