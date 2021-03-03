using UnityEngine;

namespace BeastHunter
{
    public interface ICharacter
    {
        public int Id { get; }
        public Sprite Portrait { get; }
        public int?[] ItemsId { get; }
    }
}
