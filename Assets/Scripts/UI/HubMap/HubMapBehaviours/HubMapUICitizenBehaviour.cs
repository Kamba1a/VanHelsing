using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class HubMapUICitizenBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _citizenNamePanel;
        [SerializeField] private Image _citizenPortrait;
        [SerializeField] private GameObject _exclamationImage;

        #endregion


        #region Properties

        public Action<IHubMapUICitizen> OnClick_CitizenButtonHandler { get; set; }

        #endregion


        #region Methods

        public void Initialize(IHubMapUICitizen citizen)
        {
            _citizenNamePanel.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _exclamationImage.SetActive(citizen.Dialogs[Data.HubMapData.CurrentDialogsNumbers[citizen]].IsQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen));
        }

        public void OnClick_CitizenButton(IHubMapUICitizen citizen)
        {
            OnClick_CitizenButtonHandler?.Invoke(citizen);
        }

        public void UpdateInfo(IHubMapUICitizen citizen)
        {
            _exclamationImage.SetActive(citizen.Dialogs[Data.HubMapData.CurrentDialogsNumbers[citizen]].IsQuest);
        }

        #endregion
    }
}
