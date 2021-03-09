using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUICitizen
    {
        public int Id { get; }
        public string Name { get; }
        public Sprite Portrait { get; }
        public IHubMapUIDialog[] Dialogs { get; }
    }
}
