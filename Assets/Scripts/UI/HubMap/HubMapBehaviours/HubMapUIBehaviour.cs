using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUIBehaviour : MonoBehaviour
    {
        private const float CHARACTERS_PANEL_SCROLLBAR_SPEED = 1.0f;

        #region Fields

        [Header("Map objects")]
        [SerializeField] private Button _cityButton;
        [SerializeField] private Button _locationButton_1;
        [SerializeField] private Button _locationButton_2;
        [SerializeField] private Button _locationButton_3;
        [SerializeField] private Button _locationButton_4;

        [Header("Hub map")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;
        [SerializeField] private Button _hideInfoPanelButton;
        [SerializeField] private Button _hubButton;
        [SerializeField] private Button _mapButton;

        [Header("City info panel")]
        [SerializeField] private GameObject _cityInfoPanel;
        [SerializeField] private GameObject _citizenPanel;
        [SerializeField] private GameObject _citySellingPanel;
        [SerializeField] private Image _cityFraction;
        [SerializeField] private Text _cityName;
        [SerializeField] private Text _cityDescription;
        [SerializeField] private Text _cityReputation;

        [Header("Location info panel")]
        [SerializeField] private GameObject _locationInfoPanel;
        [SerializeField] private GameObject _dwellersPanel;
        [SerializeField] private GameObject _ingredientsPanel;
        [SerializeField] private Text _locationName;
        [SerializeField] private Text _locationDescription;
        [SerializeField] private Image _locationScreen;
        [SerializeField] private Button _hikeButton;

        [Header("Dialog panel")]
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private Image _citizenPortrait;
        [SerializeField] private Text _citizenName;
        [SerializeField] private Text _dialogText;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _declineButton;

        [Header("Hike panel")]
        [SerializeField] private Button _hikePanelButton;
        [SerializeField] private Button _closeHikePanelButton;
        [SerializeField] private Button _hikeAcceptButton;
        [SerializeField] private GameObject _hikePanel;
        [SerializeField] private GameObject _hikePreparePanel;
        [SerializeField] private GameObject _charactersPanel;
        [SerializeField] private GameObject _equipmentPanel;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _inventoryItemsPanel;
        [SerializeField] private Button _closeInventoryButton;
        [SerializeField] private Scrollbar _charactersPanelScrollbar;
        [SerializeField] private Button _charactersPanelNextButton;
        [SerializeField] private Button _charactersPanelPreviousButton;

        private List<GameObject> _infoPanelObjectsForDestroy;
        private Dictionary<IHubMapUICitizen, GameObject> _currentDisplayedCitizens;
        private List<HubMapUIEquipmentCellBehaviour> _hikeEquipmentItemCells;
        private (int cellIndex, IHubMapUIItem item) _selectedEquipmentCell;
        private IHubMapUICharacter _selectedCharacter;
        private int _selectedLocationId;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _cityButton.onClick.AddListener(() => ShowCityInfoPanel(Data.HubMapData.CityId));
            _locationButton_1.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.LocationId_1));
            _locationButton_2.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.LocationId_2));
            _locationButton_3.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.LocationId_3));
            _locationButton_4.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.LocationId_4));

            _hubButton.onClick.AddListener(HideMap);
            _mapButton.onClick.AddListener(ShowMap);
            _hideInfoPanelButton.onClick.AddListener(HideRightInfoPanels);
            _hikeAcceptButton.onClick.AddListener(LocationLoad);
            _closeHikePanelButton.onClick.AddListener(CloseHikePanel);
            _hikePanelButton.onClick.AddListener(ShowHikePanel);
            _charactersPanelNextButton.onClick.AddListener(()=>MoveCharactersPanelScrollbar(CHARACTERS_PANEL_SCROLLBAR_SPEED));
            _charactersPanelPreviousButton.onClick.AddListener(() => MoveCharactersPanelScrollbar(-CHARACTERS_PANEL_SCROLLBAR_SPEED));
            _closeInventoryButton.onClick.AddListener(CloseInventoryPanel);
        }

        private void OnDisable()
        {
            _cityButton.onClick.RemoveAllListeners();
            _locationButton_1.onClick.RemoveAllListeners();
            _locationButton_2.onClick.RemoveAllListeners();
            _locationButton_3.onClick.RemoveAllListeners();
            _locationButton_4.onClick.RemoveAllListeners();

            _hubButton.onClick.RemoveAllListeners();
            _mapButton.onClick.RemoveAllListeners();
            _hideInfoPanelButton.onClick.RemoveAllListeners();
            _hikeAcceptButton.onClick.RemoveAllListeners();
            _closeHikePanelButton.onClick.RemoveAllListeners();
            _hikePanelButton.onClick.RemoveAllListeners();
            _charactersPanelNextButton.onClick.RemoveAllListeners();
            _charactersPanelPreviousButton.onClick.RemoveAllListeners();
            _closeInventoryButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            _currentDisplayedCitizens = new Dictionary<IHubMapUICitizen, GameObject>();
            _infoPanelObjectsForDestroy = new List<GameObject>();

            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
            _hikePreparePanel.SetActive(true);
            _inventoryPanel.SetActive(false);

            FillInventoryPanel(Data.HubMapData.InventoryItemsId);

            for (int i = 0; i < Data.HubMapData.Characters.Count; i++)
            {
                GameObject characterUI = GameObject.Instantiate(Data.HubMapData.CharacterUIPrefab);
                characterUI.transform.SetParent(_charactersPanel.transform, false);
                characterUI.transform.localScale = new Vector3(1, 1, 1);
                characterUI.GetComponentInChildren<HubMapUICharacterBehaviour>().Initialize(Data.HubMapData.Characters[i]);
                characterUI.GetComponentInChildren<HubMapUICharacterBehaviour>().OnClick_CharacterButtonHandler = SelectCharacter;
            }

            _hikeEquipmentItemCells = new List<HubMapUIEquipmentCellBehaviour>();
            for (int i = 0; i < Data.HubMapData.HikeEquipmentPanelCellAmount; i++)
            {
                GameObject equipCellUI = GameObject.Instantiate(Data.HubMapData.EquipmentCellUIPrefab);
                equipCellUI.transform.SetParent(_equipmentPanel.transform, false);
                equipCellUI.transform.localScale = new Vector3(1, 1, 1);
                _hikeEquipmentItemCells.Add(equipCellUI.GetComponent<HubMapUIEquipmentCellBehaviour>());
                equipCellUI.GetComponent<Button>().onClick.AddListener(() => SelectEquipmentCell(i, _hikeEquipmentItemCells[i].ItemInCell));
            }
        }

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
            FillCityInfo(Data.HubMapData.Cities.Find(city => city.Id == cityId));
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
        }

        private void ShowLocationInfoPanel(int locationId)
        {
            _selectedLocationId = locationId;
            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationInfo(Data.HubMapData.Locations.Find(location => location.Id == locationId));
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

        private void ShowDialogPanel(IHubMapUICitizen citizen)
        {
            FillDialogPanel(citizen);
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
                IHubMapUIItem hubMapUIItem = Data.HubMapData.Items.Find(item => item.Id == itemsId[i]);
                itemUI.GetComponent<Image>().sprite = hubMapUIItem.Image;
                itemUI.GetComponent<Button>().onClick.AddListener(() => OnClick_InventoryItem(i, hubMapUIItem));
            }
        }

        //WIP
        private void OnClick_InventoryItem(int cellIndex, IHubMapUIItem item)
        {
            //IHubMapUICharacter currentCharacter = new HubMapUICharacter();

            //int equipInCellItemId = 0; //currentCharacter.ItemsId
            //int inventoryItemId = 0;    //Data.HubMapData.InventoryItemsId

            //int selectEquipCellNumber = 0;  //currentCharacter.ItemsId[i]
            //int selectInventoryCellNumber = 0; //Data.HubMapData.InventoryItemsId[i]

            //if (_selectedEquipmentCell.item == null)
            //{
            //    Data.HubMapData.InventoryItemsId.Remove(item);
            //    Data.HubMapData.InventoryItemsId.Insert(selectInventoryCellNumber, equipInCellItemId);
            //}
            //else
            //{
            //    Data.HubMapData.InventoryItemsId.Insert(selectInventoryCellNumber, equipInCellItemId);
            //}
            //currentCharacter.ItemsId[selectEquipCellNumber] = inventoryItemId;

            ////update inventory
            //FillEquipmentPanel(currentCharacter.ItemsId);    //update equipment
        }

        private void SelectEquipmentCell(int cellIndex, IHubMapUIItem item)
        {
            _selectedEquipmentCell.cellIndex = cellIndex;
            _selectedEquipmentCell.item = item;
            ShowInventoryPanel();
        }

        private void SelectCharacter(IHubMapUICharacter character)
        {
            _selectedCharacter = character;
            FillEquipmentPanel(character.ItemsId);
        }

        private void FillEquipmentPanel(int[] itemsId)
        {
            for (int i = 0; i < _hikeEquipmentItemCells.Count; i++)
            {
                _hikeEquipmentItemCells[i].SetInteractable(true);
                if (itemsId[i] != 0)
                {
                    _hikeEquipmentItemCells[i].PutItemInCell(Data.HubMapData.Items.Find(item => item.Id == itemsId[i]));
                }
                else
                {
                    _hikeEquipmentItemCells[i].ClearCell();
                }
            }
        }

        private void FillCityInfo(IHubMapUICity city)
        {
            _cityFraction.sprite = city.Fraction;
            _cityName.text = city.Name;
            _cityDescription.text = city.Description;
            _cityReputation.text = city.CurrentPlayerReputation.ToString();

            for (int i = 0; i < city.CitizensId.Length; i++)
            {
                GameObject citizenUI = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab);
                IHubMapUICitizen hubMapUICitizen = Data.HubMapData.Citizens.Find(citizen => citizen.Id == city.CitizensId[i]);
                _infoPanelObjectsForDestroy.Add(citizenUI);
                _currentDisplayedCitizens.Add(hubMapUICitizen, citizenUI);
                citizenUI.transform.SetParent(_citizenPanel.transform, false);
                citizenUI.transform.localScale = new Vector3(1, 1, 1);
                citizenUI.GetComponentInChildren<HubMapUICitizenBehaviour>().Initialize(hubMapUICitizen);
                citizenUI.GetComponentInChildren<HubMapUICitizenBehaviour>().OnClick_CitizenButtonHandler = ShowDialogPanel;
            }

            for (int i = 0; i < city.SellingItemsId.Length; i++)
            {
                GameObject itemUI = GameObject.Instantiate(Data.HubMapData.SellingItemUIPrefab);
                _infoPanelObjectsForDestroy.Add(itemUI);
                itemUI.transform.SetParent(_citySellingPanel.transform, false);
                itemUI.transform.localScale = new Vector3(1, 1, 1);
                IHubMapUIItem hubMapUIItem = Data.HubMapData.Items.Find(item => item.Id == city.SellingItemsId[i]);
                itemUI.GetComponentInChildren<HubMapUISellingItemBehaviour>().Initialize(hubMapUIItem, city.CurrentPlayerReputation > hubMapUIItem.RequiredReputationForSale);
            }
        }

        private void FillLocationInfo(IHubMapUILocation location)
        {
            _locationScreen.sprite = location.Screenshot;
            _locationName.text = location.Name;
            _locationDescription.text = location.Description;

            for (int i = 0; i < location.DwellersId.Length; i++)
            {
                GameObject dwellerUI = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _infoPanelObjectsForDestroy.Add(dwellerUI);
                dwellerUI.transform.SetParent(_dwellersPanel.transform, false);
                dwellerUI.transform.localScale = new Vector3(1, 1, 1);
                //dwellerUI.GetComponentInChildren<DwellerUIBehaviour>().Initialize(Array.Find(Data.HubMapData.Dwellers, dweller => dweller.Id == location.DwellersId[i]));
            }

            for (int i = 0; i < location.IngredientsId.Length; i++)
            {
                GameObject ingredientUI = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _infoPanelObjectsForDestroy.Add(ingredientUI);
                ingredientUI.transform.SetParent(_ingredientsPanel.transform, false);
                ingredientUI.transform.localScale = new Vector3(1, 1, 1);
                //ingredientUI.GetComponentInChildren<IngredientUIBehaviour>().Initialize(Array.Find(Data.HubMapData.Ingredients, ingredient => ingredient.Id == location.IngredientsId[i]));
            }
        }

        private void ClearRightInfoPanel()
        {
            for (int i=0; i< _infoPanelObjectsForDestroy.Count; i++)
            {
                Destroy(_infoPanelObjectsForDestroy[i]);
            }
            _infoPanelObjectsForDestroy.Clear();
            _currentDisplayedCitizens.Clear();
        }

        private void OnClick_DialogButton(IHubMapUICitizen citizen, IHubMapUIDialogAnswer dialogAnswer)
        {
            _acceptButton.onClick.RemoveAllListeners();
            _declineButton.onClick.RemoveAllListeners();

            Data.HubMapData.CurrentDialogsNumbers[citizen] = dialogAnswer.NextDialogNumber;

            if (dialogAnswer.IsDialogEnd)
            {
                HideDialogPanel();
                UpdateCitizenInfo(citizen);
            }
            else
            {
                FillDialogPanel(citizen);
            }
        }

        private void UpdateCitizenInfo(IHubMapUICitizen citizen)
        {
            _currentDisplayedCitizens[citizen].GetComponentInChildren<HubMapUICitizenBehaviour>().UpdateInfo(citizen);
        }

        private void FillDialogPanel(IHubMapUICitizen citizen)
        {
            IHubMapUIDialog currentDialog = citizen.Dialogs[Data.HubMapData.CurrentDialogsNumbers[citizen]];
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
            Debug.Log("Load location. Location id: " + _selectedLocationId);
        }

        #endregion
    }
}
