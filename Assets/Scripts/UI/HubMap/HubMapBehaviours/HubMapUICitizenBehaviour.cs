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
        [SerializeField] private GameObject _questionImage;

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
            GetComponent<Button>().onClick.AddListener(() => OnClick_CitizenButton(citizen));
            SetQuestMarker(Data.HubMapData.QuestService.GetQuestMarker(citizen));
        }

        public void OnClick_CitizenButton(HubMapUICitizen citizen)
        {
            OnClick_CitizenButtonHandler?.Invoke(citizen);
        }

        public void UpdateInfo(HubMapUICitizen citizen)
        {
            SetQuestMarker(Data.HubMapData.QuestService.GetQuestMarker(citizen));
        }

        private void SetQuestMarker(HubMapUIQuestMarkerType questMarkerType)
        {
            switch (questMarkerType)
            {
                case HubMapUIQuestMarkerType.Exclamation: _exclamationImage.SetActive(true); break;
                case HubMapUIQuestMarkerType.Question: _questionImage.SetActive(true); break;
                default: 
                    _exclamationImage.SetActive(false);
                    _questionImage.SetActive(false);
                    break;
            }
        }

        #endregion
    }
}
