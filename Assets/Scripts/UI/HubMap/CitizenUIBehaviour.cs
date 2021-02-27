using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CitizenUIBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _citizenNamePanel;
        [SerializeField] private Image _citizenPortrait;
        [SerializeField] private GameObject _exclamationImage;

        #endregion


        #region Properties

        public Action<int> OnClick_CitizenButtonHandler { get; set; }
        public int Id { get; private set; }

        #endregion


        #region Methods

        public void Initialize(ICitizenInfo citizen)
        {
            Id = citizen.Id;
            _citizenNamePanel.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _exclamationImage.SetActive(Data.HubMapData.Dialogs[citizen.CurrentDialogId].IsQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen.Id));
        }

        public void OnClick_CitizenButton(int citizenId)
        {
            OnClick_CitizenButtonHandler?.Invoke(citizenId);
        }

        public void UpdateInfo(ICitizenInfo citizen)
        {
            _exclamationImage.SetActive(Data.HubMapData.Dialogs[citizen.CurrentDialogId].IsQuest);
        }

        #endregion
    }
}
