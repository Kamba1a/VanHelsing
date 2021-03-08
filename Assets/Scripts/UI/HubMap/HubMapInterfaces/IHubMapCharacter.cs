using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapCharacter
    {
        public int Id { get; }
        public Sprite Portrait { get; }
        public int[] ItemsId { get; }
    }
}
