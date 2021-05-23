using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "PocketItem", menuName = "CreateData/HubUIData/Items/PocketItem", order = 0)]
    public class PocketItemData : BaseItemData
    {
        #region Properties

        public override ItemType ItemType => ItemType.PocketItem;

        #endregion
    }
}
