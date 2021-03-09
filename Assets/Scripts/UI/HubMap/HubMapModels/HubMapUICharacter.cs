using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class HubMapUICharacter : HubMapUIBaseEntity, IHubMapUICharacter
    {
        #region Fields

        [SerializeField] private Sprite _portrait;
        [SerializeField] private int[] _itemsId;

        #endregion


        #region Properties

        public Sprite Portrait => _portrait;
        public int[] ItemsId => _itemsId;

        #endregion


        #region Methods

        public void SetEquipmentsSize(int size)
        {
            Array.Resize(ref _itemsId, size);
        }

        #endregion
    }
}
