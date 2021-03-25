using UnityEngine;

namespace BeastHunter
{
    public class HubMapUICharacterModel
    {
        public string Name { get; set; }
        public Sprite Portrait { get; private set; }
        public HubMapUIStorage Backpack { get; private set; }
        public HubMapUICharacterBehaviour HubMapUIBehaviour { get; set; }


        public HubMapUICharacterModel(HubMapUICharacterData data, int backpackSize)
        {
            Name = data.Name;
            Portrait = data.Portrait;

            Backpack = new HubMapUIStorage(backpackSize);
            for (int i = 0; i < data.StartItems.Length; i++)
            {
                Backpack.PutItem(i, data.StartItems[i]);
            }
        }
    }
}
