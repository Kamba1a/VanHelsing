﻿using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUICitizen", menuName = "CreateData/HubMapUIData/HubMapUICitizen", order = 0)]
    public class HubMapUICitizen : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int _firstDialogId;
        [SerializeField] private HubMapUIDialogNode[] _dialogs;

        #endregion


        #region Properties

        public string Name => _name;
        public Sprite Portrait => _portrait;
        public int FirstDialogId => _firstDialogId;
        public HubMapUIDialogNode[] Dialogs => _dialogs;

        #endregion
    }
}
