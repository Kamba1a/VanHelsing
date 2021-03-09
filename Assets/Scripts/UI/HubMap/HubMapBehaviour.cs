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
        [SerializeField] private GameObject _citizenPanel;
        [SerializeField] private GameObject _citySellingPanel;
        [SerializeField] private Image _cityFraction;
        [SerializeField] private Text _cityName;
        [SerializeField] private Text _cityDescription;
        [SerializeField] private Text _cityReputation;

        [Header("Dialog panel")]
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private Image _citizenPortrait;
        [SerializeField] private Text _citizenName;
        [SerializeField] private Text _dialogText;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _declineButton;

        [Header("Location info panel")]
        [SerializeField] private GameObject _locationInfoPanel;
        [SerializeField] private GameObject _dwellersPanel;
        [SerializeField] private GameObject _ingredientsPanel;
        [SerializeField] private Text _locationName;
        [SerializeField] private Text _locationDescription;
        [SerializeField] private Image _locationScreen;
        [SerializeField] private Button _hikeButton;

        [Header("Hike panel")]
        [SerializeField] private GameObject _hikePanel;
        [SerializeField] private GameObject _hikePreparePanel;
        [SerializeField] private GameObject _charactersPanel;
        [SerializeField] private GameObject _equipmentPanel;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _inventoryItemsPanel;
        [SerializeField] private Scrollbar _charactersPanelScrollbar;

        #endregion


        #region Fields

        private List<GameObject> _clearInfoPanelList;
        private List<GameObject> _currentCitizensList;
        private List<EquipmentItemHubMapUIBehaviour> _hikeEquipmentItemCells;
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
            _inventoryPanel.SetActive(false);
            FillInventoryPanel(Data.HubMapData.InventoryItemsId);

            for (int i = 0; i < Data.HubMapData.Characters.Length; i++)
            {
                GameObject character = GameObject.Instantiate(Data.HubMapData.CharacterUIPrefab);
                character.transform.SetParent(_charactersPanel.transform, false);
                character.transform.localScale = new Vector3(1, 1, 1);
                character.GetComponentInChildren<CharacterUIBehaviour>().Initialize(Data.HubMapData.Characters[i]);
                character.GetComponentInChildren<CharacterUIBehaviour>().OnClick_CharacterButtonHandler = FillEquipmentPanel;
            }

            _hikeEquipmentItemCells = new List<EquipmentItemHubMapUIBehaviour>();
            for (int i = 0; i < Data.HubMapData.HikeEquipmentPanelCellAmount; i++)
            {
                GameObject equipCell = GameObject.Instantiate(Data.HubMapData.EquipmentItemUIPrefab);
                equipCell.transform.SetParent(_equipmentPanel.transform, false);
                equipCell.transform.localScale = new Vector3(1, 1, 1);
                _hikeEquipmentItemCells.Add(equipCell.GetComponent<EquipmentItemHubMapUIBehaviour>());
                equipCell.GetComponent<Button>().onClick.AddListener(ShowInventoryPanel);
            }
        }

        #endregion


        #region TriggerEvents

        //TODO?: replace obscure event subscription to OnEnable()/OnDisable methods with explicit subscription
        public void OnClick_HubButton() => HideMap();
        public void OnClick_MapButton() => ShowMap();
        public void OnClick_CityButton(int cityId) => ShowCityInfoPanel(cityId);
        public void OnClick_CloseInfoButton() => HideRightInfoPanels();
        public void OnClick_LocationButton(int locationId) => ShowLocationInfoPanel(locationId);
        public void OnClick_HikeButton() => ShowHikePanel();
        public void OnClick_HikeAcceptButton() => LocationLoad();
        public void OnClick_CloseHikeButton() => CloseHikePanel();
        public void OnClick_CharactersPanelButton(float step) => MoveCharactersPanelScrollbar(step);
        public void OnClick_CloseInventoryButton() => CloseInventoryPanel();

        #endregion


        #region Methods

        private void ShowMap()
        {
            _mainPanel.SetActive(true);
        }

        private void HideMap()
        {
            _mainPanel.SetActive(false);
        }

        private void ShowInventoryPanel()
        {
            _inventoryPanel.SetActive(true);
        }

        private void CloseInventoryPanel()
        {
            _inventoryPanel.SetActive(false);
        }

        private void ShowHikePanel()
        {
            for (int i = 0; i < _hikeEquipmentItemCells.Count; i++)
            {
                _hikeEquipmentItemCells[i].ClearCell();
                _hikeEquipmentItemCells[i].SetInteractable(false);
            }
            _hikePanel.SetActive(true);
        }

        private void CloseHikePanel()
        {
            _inventoryPanel.SetActive(false);
            _hikePanel.SetActive(false);
        }

        private void ShowCityInfoPanel(int cityId)
        {
            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillCityInfo(Array.Find(Data.HubMapData.Cities, city => city.Id == cityId));
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
        }

        private void ShowLocationInfoPanel(int locationId)
        {
            _currentLocationId = locationId;
            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationInfo(Array.Find(Data.HubMapData.Locations, location => location.Id == locationId));
            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _locationInfoPanel.SetActive(true);
        }

        private void HideRightInfoPanels()
        {
            _cityInfoPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }

        private void ShowDialogPanel(int citizenId)
        {
            FillDialogPanel(Array.Find(Data.HubMapData.Citizens, citizen => citizen.Id == citizenId));
            _dialogPanel.SetActive(true);
        }

        private void HideDialogPanel()
        {
            _dialogPanel.SetActive(false);
        }

        private void FillInventoryPanel(List<int> itemsId)
        {
            for (int i = 0; i < itemsId.Count; i++)
            {
                GameObject itemUI = GameObject.Instantiate(Data.HubMapData.InventoryItemUIPrefab);
                itemUI.transform.SetParent(_inventoryItemsPanel.transform, false);
                itemUI.transform.localScale = new Vector3(1, 1, 1);

                itemUI.GetComponent<Image>().sprite = Array.Find(Data.HubMapData.Items, item => item.Id == itemsId[i]).Image;
                //itemUI.GetComponent<Button>().onClick.AddListener(TEMPmethod);
            }
        }

        //WIP
        private void TEMPmethod()
        {
            /*
            ICharacter currentCharacter = new TemporaryCharacterModel();

            int? equipItemIdInCell = 0; //currentCharacter.ItemsId
            int inventoryItemId = 0;    //Data.HubMapData.InventoryItemsId

            int currentEquipCellNumber = 0;
            int currentInventoryCellNumber = 0;

            if (currentCharacter.ItemsId[currentEquipCellNumber] != 0)
            {
                Data.HubMapData.InventoryItemsId[currentInventoryCellNumber] = equipItemIdInCell.Value;
            }
            else
            {
                Data.HubMapData.InventoryItemsId.Remove(inventoryItemId);
            }
            currentCharacter.ItemsId[currentEquipCellNumber] = inventoryItemId;

            //update inventory
            //update equipment
            */
        }

        private void FillEquipmentPanel(int[] itemsId)
        {
            for (int i = 0; i < _hikeEquipmentItemCells.Count; i++)
            {
                _hikeEquipmentItemCells[i].SetInteractable(true);
                if (itemsId[i] != 0)
                {
                    _hikeEquipmentItemCells[i].PutItemInCell(Array.Find(Data.HubMapData.Items, item => item.Id == itemsId[i]));
                }
                else
                {
                    _hikeEquipmentItemCells[i].ClearCell();
                }
            }
        }

        private void FillCityInfo(ICityInfo city)
        {
            _cityFraction.sprite = city.Fraction;
            _cityName.text = city.Name;
            _cityDescription.text = city.Description;
            _cityReputation.text = city.CurrentPlayerReputation.ToString();

            for (int i = 0; i < city.CitizensId.Length; i++)
            {
                GameObject citizenUI = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab);
                _clearInfoPanelList.Add(citizenUI);
                _currentCitizensList.Add(citizenUI);
                citizenUI.transform.SetParent(_citizenPanel.transform, false);
                citizenUI.transform.localScale = new Vector3(1, 1, 1);
                citizenUI.GetComponentInChildren<CitizenUIBehaviour>().Initialize(Array.Find(Data.HubMapData.Citizens, citizen => citizen.Id == city.CitizensId[i]));
                citizenUI.GetComponentInChildren<CitizenUIBehaviour>().OnClick_CitizenButtonHandler = ShowDialogPanel;
            }

            for (int i = 0; i < city.SellingItemsId.Length; i++)
            {
                GameObject itemUI = GameObject.Instantiate(Data.HubMapData.SellingItemUIPrefab);
                _clearInfoPanelList.Add(itemUI);
                itemUI.transform.SetParent(_citySellingPanel.transform, false);
                itemUI.transform.localScale = new Vector3(1, 1, 1);
                IItemInfo hubMapUIItem = Array.Find(Data.HubMapData.Items, item => item.Id == city.SellingItemsId[i]);
                itemUI.GetComponentInChildren<SellingItemUIBehaviour>().Initialize(hubMapUIItem, city.CurrentPlayerReputation > hubMapUIItem.RequiredReputationForSale);
            }
        }

        private void FillLocationInfo(ILocationInfo location)
        {
            _locationScreen.sprite = location.Screenshot;
            _locationName.text = location.Name;
            _locationDescription.text = location.Description;

            for (int i = 0; i < location.DwellersId.Length; i++)
            {
                GameObject dwellerUI = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _clearInfoPanelList.Add(dwellerUI);
                dwellerUI.transform.SetParent(_dwellersPanel.transform, false);
                dwellerUI.transform.localScale = new Vector3(1, 1, 1);
                //dwellerUI.GetComponentInChildren<DwellerUIBehaviour>().Initialize(Array.Find(Data.HubMapData.Dwellers, dweller => dweller.Id == location.DwellersId[i]));
            }

            for (int i = 0; i < location.IngredientsId.Length; i++)
            {
                GameObject ingredientUI = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _clearInfoPanelList.Add(ingredientUI);
                ingredientUI.transform.SetParent(_ingredientsPanel.transform, false);
                ingredientUI.transform.localScale = new Vector3(1, 1, 1);
                //ingredientUI.GetComponentInChildren<IngredientUIBehaviour>().Initialize(Array.Find(Data.HubMapData.Ingredients, ingredient => ingredient.Id == location.IngredientsId[i]));
            }
        }

        private void ClearRightInfoPanel()
        {
            for (int i=0; i< _clearInfoPanelList.Count; i++)
            {
                Destroy(_clearInfoPanelList[i]);
            }
            _clearInfoPanelList.Clear();
            _currentCitizensList.Clear();
        }

        private void OnClick_DialogButton(ICitizenInfo citizen, IDialogAnswer dialogAnswer)
        {
            _acceptButton.onClick.RemoveAllListeners();
            _declineButton.onClick.RemoveAllListeners();

            Data.HubMapData.CurrentDialogsNumbers[citizen.Id] = dialogAnswer.NextDialogNumber;

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
            IDialog currentDialog = citizen.Dialogs[Data.HubMapData.CurrentDialogsNumbers[citizen.Id]];
            _citizenName.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _dialogText.text = currentDialog.Text;
            _declineButton.GetComponentInChildren<Text>().text = currentDialog.NegativeAnswer.Text;
            _declineButton.onClick.AddListener(() => OnClick_DialogButton(citizen, currentDialog.NegativeAnswer));

            if (currentDialog.PositiveAnswer.Text != "")
            {
                _acceptButton.GetComponentInChildren<Text>().text = currentDialog.PositiveAnswer.Text;
                _acceptButton.onClick.AddListener(() => OnClick_DialogButton(citizen, currentDialog.PositiveAnswer));
                _acceptButton.gameObject.SetActive(true);
            }
            else
            {
                _acceptButton.gameObject.SetActive(false);
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
