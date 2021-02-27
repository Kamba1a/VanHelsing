using Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapBehaviour : MonoBehaviour
    {
        #region SerializeFields

        [Header("Hub map")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;

        [Header("City info panel")]
        [SerializeField] private GameObject _cityInfoPanel;
        [SerializeField] private GameObject _cityFraction;
        [SerializeField] private GameObject _cityName;
        [SerializeField] private GameObject _cityDescription;
        [SerializeField] private GameObject _citizenPanel;
        [SerializeField] private GameObject _cityReputation;
        [SerializeField] private GameObject _citySellingPanel;

        [Header("Dialog panel")]
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private GameObject _citizenPortrait;
        [SerializeField] private GameObject _citizenName;
        [SerializeField] private GameObject _dialogText;
        [SerializeField] private GameObject _acceptButton;
        [SerializeField] private GameObject _declineButton;

        [Header("Location info panel")]
        [SerializeField] private GameObject _locationInfoPanel;
        [SerializeField] private GameObject _locationName;
        [SerializeField] private GameObject _locationDescription;
        [SerializeField] private GameObject _dwellersPanel;
        [SerializeField] private GameObject _ingredientsPanel;
        [SerializeField] private GameObject _locationScreen;
        [SerializeField] private GameObject _hikeButton;

        [Header("Hike panel")]
        [SerializeField] private GameObject _hikePanel;
        [SerializeField] private GameObject _hikePreparePanel;
        [SerializeField] private Scrollbar _charactersPanelScrollbar;


        #endregion


        #region Fields

        private List<GameObject> _clearInfoPanelList;
        private List<GameObject> _currentCitizensList;
        private int _currentLocationId;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _currentCitizensList = new List<GameObject>();
            _clearInfoPanelList = new List<GameObject>();
            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
            _hikePreparePanel.SetActive(true);
        }

        #endregion


        #region TriggerEvents

        public void OnClick_HubButton() => HideUI();
        public void OnClick_MapButton() => ShowUI();
        public void OnClick_CityButton(int cityId) => ShowCityInfoPanel(cityId);
        public void OnClick_CloseInfoButton() => HideAllInfoPanels();
        public void OnClick_LocationButton(int locationId) => ShowLocationInfoPanel(locationId);
        public void OnClick_HikeButton() => ShowHikePanel();
        public void OnClick_HikeAcceptButton() => LocationLoad();
        public void OnClick_CloseHikeButton() => CloseHikePanel();
        public void OnClick_CharactersPanelButton(float step) => MoveCharactersPanelScrollbar(step);

        #endregion


        #region Methods

        private void ShowUI()
        {
            _mainPanel.SetActive(true);
        }

        private void HideUI()
        {
            _mainPanel.SetActive(false);
        }

        private void ShowHikePanel()
        {
            _hikePanel.SetActive(true);
        }

        private void CloseHikePanel()
        {
            _hikePanel.SetActive(false);
        }

        private void ShowCityInfoPanel(int cityId)
        {
            HideAllInfoPanels();
            ClearInfoPanel();
            FillCityInfo(Data.HubMapData.Cities[cityId]);
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
        }

        private void ShowLocationInfoPanel(int locationId)
        {
            _currentLocationId = locationId;
            HideAllInfoPanels();
            ClearInfoPanel();
            FillLocationInfo(Data.HubMapData.Locations[locationId]);
            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _locationInfoPanel.SetActive(true);
        }

        private void HideAllInfoPanels()
        {
            _cityInfoPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }

        private void FillCityInfo(ICityInfo city)
        {
            _cityFraction.GetComponent<Image>().sprite = city.Fraction;
            _cityName.GetComponent<Text>().text = city.Name;
            _cityDescription.GetComponent<Text>().text = city.Description;
            _cityReputation.GetComponent<Text>().text = city.Reputation.ToString();

            for (int i = 0; i < city.CitizensId.Length; i++)
            {
                GameObject citizen = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab);
                _clearInfoPanelList.Add(citizen);
                _currentCitizensList.Add(citizen);
                citizen.transform.SetParent(_citizenPanel.transform, false);
                citizen.transform.localScale = new Vector3(1, 1, 1);
                citizen.GetComponentInChildren<CitizenUIBehaviour>().Initialize(Data.HubMapData.Citizens[city.CitizensId[i]]);
                citizen.GetComponentInChildren<CitizenUIBehaviour>().OnClick_CitizenButtonHandler = ShowDialogPanel;
            }

            for (int i = 0; i < city.SellingItemsId.Length; i++)
            {
                GameObject item = GameObject.Instantiate(Data.HubMapData.SellingItemUIPrefab);
                _clearInfoPanelList.Add(item);
                item.transform.SetParent(_citySellingPanel.transform, false);
                item.transform.localScale = new Vector3(1, 1, 1);
                item.GetComponentInChildren<SellingItemUIBehaviour>().Initialize(Data.HubMapData.Items[city.SellingItemsId[i]]);
            }
        }

        private void FillLocationInfo(ILocationInfo location)
        {
            _locationScreen.GetComponent<Image>().sprite = location.Screenshot;
            _locationName.GetComponent<Text>().text = location.Name;
            _locationDescription.GetComponent<Text>().text = location.Description;

            for (int i = 0; i < location.DwellersId.Length; i++)
            {
                GameObject dweller = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _clearInfoPanelList.Add(dweller);
                dweller.transform.SetParent(_dwellersPanel.transform, false);
                dweller.transform.localScale = new Vector3(1, 1, 1);
                //dweller.GetComponentInChildren<DwellerUIBehaviour>().Initialize(Data.HubMapData.Dwellers[location.DwellersId[i]]);
            }

            for (int i = 0; i < location.IngredientsId.Length; i++)
            {
                GameObject ingredient = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _clearInfoPanelList.Add(ingredient);
                ingredient.transform.SetParent(_ingredientsPanel.transform, false);
                ingredient.transform.localScale = new Vector3(1, 1, 1);
                //ingredient.GetComponentInChildren<IngredientUIBehaviour>().Initialize(Data.HubMapData.Ingredients[location.IngredientsId[i]]);
            }
        }

        private void ClearInfoPanel()
        {
            for (int i=0; i< _clearInfoPanelList.Count; i++)
            {
                Destroy(_clearInfoPanelList[i]);
            }
            _clearInfoPanelList.Clear();
            _currentCitizensList.Clear();
        }

        private void ShowDialogPanel(int citizenId)
        {
            FillDialogPanel(Data.HubMapData.Citizens[citizenId]);
            _dialogPanel.SetActive(true);
        }

        private void HideDialogPanel()
        {
            _dialogPanel.SetActive(false);
        }

        private void OnClick_DialogButton(ICitizenInfo citizen, IDialogAnswer dialogAnswer)
        {
            _acceptButton.GetComponent<Button>().onClick.RemoveAllListeners();
            _declineButton.GetComponent<Button>().onClick.RemoveAllListeners();

            citizen.NextDialog(dialogAnswer.NextDialogNodeId);

            if (dialogAnswer.IsDialogEnd)
            {
                HideDialogPanel();
                UpdateCitizenInfo(citizen.Id);
            }
            else
            {
                FillDialogPanel(citizen);
            }
        }

        private void UpdateCitizenInfo(int citizenId)
        {
            for (int i = 0; i < _currentCitizensList.Count; i++)
            {
                if (_currentCitizensList[i].GetComponentInChildren<CitizenUIBehaviour>().Id == citizenId)
                {
                    _currentCitizensList[i].GetComponentInChildren<CitizenUIBehaviour>().UpdateInfo(Data.HubMapData.Citizens[citizenId]);
                }
            }
        }

        private void FillDialogPanel(ICitizenInfo citizen)
        {
            IDialog currentDialog = Data.HubMapData.Dialogs[citizen.CurrentDialogId];
            _citizenName.GetComponent<Text>().text = citizen.Name;
            _citizenPortrait.GetComponent<Image>().sprite = citizen.Portrait;
            _dialogText.GetComponent<Text>().text = currentDialog.Text;
            _declineButton.GetComponentInChildren<Text>().text = currentDialog.NegativeAnswer.Text;
            _declineButton.GetComponent<Button>().onClick.AddListener(() => OnClick_DialogButton(citizen, currentDialog.NegativeAnswer));

            if (currentDialog.PositiveAnswer.Text != "")
            {
                _acceptButton.GetComponentInChildren<Text>().text = currentDialog.PositiveAnswer.Text;
                _acceptButton.GetComponent<Button>().onClick.AddListener(() => OnClick_DialogButton(citizen, currentDialog.PositiveAnswer));
                _acceptButton.SetActive(true);
            }
            else
            {
                _acceptButton.SetActive(false);
            }
        }

        private void MoveCharactersPanelScrollbar(float step)
        {
            _charactersPanelScrollbar.value += step;
        }

        private void LocationLoad()
        {
            Debug.Log("Load location. Location id: " + _currentLocationId);
        }

        #endregion
    }
}
