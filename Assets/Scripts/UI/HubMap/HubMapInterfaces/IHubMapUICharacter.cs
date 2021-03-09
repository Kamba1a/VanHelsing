using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUICharacter
    {
        public Sprite Portrait { get; }
        public int[] ItemsId { get; }
    }
}
