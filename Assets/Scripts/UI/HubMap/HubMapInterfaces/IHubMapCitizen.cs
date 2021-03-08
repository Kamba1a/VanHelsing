using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapCitizen
    {
        public int Id { get; }
        public string Name { get; }
        public Sprite Portrait { get; }
        public IHubMapDialog[] Dialogs { get; }
    }
}
