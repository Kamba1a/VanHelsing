using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenHubMapUIBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _citizenNamePanel;
        [SerializeField] private Image _citizenPortrait;
        [SerializeField] private GameObject _exclamationImage;

        #endregion


        #region Properties

        public Action<int> OnClick_CitizenButtonHandler { get; set; }
        public Action<TempCitizenData> OnClick_CitizenButtonHandler2 { get; set; }
        public int Id { get; private set; }

        #endregion


        #region Methods

        public void Initialize(IHubMapCitizen citizen)
        {
            Id = citizen.Id;
            _citizenNamePanel.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _exclamationImage.SetActive(citizen.Dialogs[Data.HubMapUIData.CurrentDialogsNumbers[citizen.Id]].IsQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen.Id));
        }

        public void Initialize(TempCitizenData citizen)
        {
            _citizenNamePanel.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            Data.HubMapUIData.AddCitizenInDic(citizen);
            _exclamationImage.SetActive(citizen.Dialogs[Data.HubMapUIData.CurrentDialogsNumbers2[citizen]].IsQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen));
        }

        public void OnClick_CitizenButton(int citizenId)
        {
            OnClick_CitizenButtonHandler?.Invoke(citizenId);
        }

        public void OnClick_CitizenButton(TempCitizenData citizen)
        {
            OnClick_CitizenButtonHandler2?.Invoke(citizen);
        }

        public void UpdateInfo(IHubMapCitizen citizen)
        {
            _exclamationImage.SetActive(citizen.Dialogs[Data.HubMapUIData.CurrentDialogsNumbers[citizen.Id]].IsQuest);
        }

        #endregion
    }
}
