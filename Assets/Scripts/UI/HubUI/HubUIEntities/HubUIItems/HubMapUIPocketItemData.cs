using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIPocketItemData", menuName = "CreateData/HubMapUIData/Items/PocketItem", order = 0)]
    class HubMapUIPocketItemData : HubMapUIBaseItemData
    {
        #region Properties

        public override HubMapUIItemType ItemType { get; protected set; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            ItemType = HubMapUIItemType.PocketItem;
        }

        #endregion
    }
}
