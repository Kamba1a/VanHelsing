﻿using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class CityInfoPanelBehaviour: MonoBehaviour
    {
        [SerializeField] private GameObject _cityName;
        [SerializeField] private GameObject _cityDescription;
        [SerializeField] private GameObject _citizenPanel;

        public void Initialize(ICityInfo city)
        {
            _cityName.GetComponent<Text>().text = city.Name;
            _cityDescription.GetComponent<Text>().text = city.Description;

            for (int i = 0; i < city.Citizens.Count; i++)
            {
                Transform citizen = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab).transform;
                citizen.SetParent(_citizenPanel.transform, false);
                citizen.localScale = new Vector3(1, 1, 1);
                citizen.GetComponentInChildren<CitizenInfoBehaviour>().Initialize(city.Citizens[i]);
            }
        }
    }
}
