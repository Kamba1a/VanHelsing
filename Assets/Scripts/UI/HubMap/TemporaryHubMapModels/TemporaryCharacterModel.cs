using System;
using UnityEngine;

namespace BeastHunter
{
    [Serializable]
    public class TemporaryCharacterModel : IHubMapCharacter
    {
        #region Fields

        [SerializeField] [ReadOnlyInUnityInspector] private int _id;
        [SerializeField] private Sprite _portrait;
        [SerializeField] private int[] _itemsId;

        #endregion


        #region Properties

        public int Id => _id;
        public Sprite Portrait => _portrait;
        public int[] ItemsId => _itemsId;

        #endregion


        #region Methods

        public void SetId(int id)
        {
            _id = id;
        }

        public void SetEquipmentsSize(int size)
        {
            Array.Resize(ref _itemsId, size);
        }

        #endregion
    }
}
