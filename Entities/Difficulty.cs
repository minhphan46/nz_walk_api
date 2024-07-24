using NZWalks.Entities.Base;

namespace NZWalks.Entities
{
    public class Difficulty : IBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
