﻿using System;
using UnityEngine;


namespace BeastHunter
{
    [Serializable]
    public struct ItemStruct
    {
        #region Fields

        [SerializeField] private string _name;
        [SerializeField] private string _description;

        [SerializeField] private SlotSize _slotSize;
        [SerializeField] private ItemType _itemType;

        [SerializeField] private Mesh _meshOfObject;
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _requiredReputationForSaleInStore;

        #endregion


        #region Properties

        public string Name => _name;
        public string Description => _description;
        public SlotSize SlotSize => _slotSize;
        public ItemType ItemType => _itemType;
        public Mesh MeshOfObject => _meshOfObject;
        public Sprite Icon => _icon;
        public float RequiredReputationForSale => _requiredReputationForSaleInStore;

        #endregion
    }
}

