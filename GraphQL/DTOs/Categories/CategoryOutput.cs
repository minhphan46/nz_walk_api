using NZWalks.GraphQL.DTOs.Base;

namespace NZWalks.GraphQL.DTOs.Categories
{
    public class CategoryOutput : IBaseOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
