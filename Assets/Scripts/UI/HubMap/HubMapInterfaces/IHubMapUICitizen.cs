using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUICitizen
    {
        public string Name { get; }
        public Sprite Portrait { get; }
        public IHubMapUIDialog[] Dialogs { get; }
    }
}
