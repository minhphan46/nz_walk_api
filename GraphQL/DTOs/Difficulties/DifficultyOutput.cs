using NZWalks.GraphQL.DTOs.Base;

namespace NZWalks.GraphQL.DTOs.Difficulties
{
    public class DifficultyOutput : IBaseOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
