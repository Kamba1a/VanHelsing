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

        public Action<HubMapUICitizen> OnClick_CitizenButtonHandler { get; set; }

        #endregion


        #region Methods

        public void FillCitizenInfo(HubMapUICitizen citizen)
        {
            _citizenNamePanel.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            Data.HubMapData.DialogsController.AddCitizen(citizen);
            //_exclamationImage.SetActive(citizen.Dialogs[Data.HubMapData.DialogsController.GetCurrentDialogId(citizen)].IsQuest);
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen));
        }

        public void OnClick_CitizenButton(HubMapUICitizen citizen)
        {
            OnClick_CitizenButtonHandler?.Invoke(citizen);
        }

        public void UpdateInfo(HubMapUICitizen citizen)
        {
            //_exclamationImage.SetActive(citizen.Dialogs[Data.HubMapData.DialogsController.GetCurrentDialogId(citizen)].IsQuest);
        }

        #endregion
    }
}
