using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUICharacter
    {
        public int Id { get; }
        public Sprite Portrait { get; }
        public int[] ItemsId { get; }
    }
}
