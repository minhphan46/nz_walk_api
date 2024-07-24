using NZWalks.GraphQL.DTOs.Base;

namespace NZWalks.GraphQL.DTOs.Regions
{
    public class RegionOutput : IBaseOutput
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string RegionImageUrl { get; set; }
    }
}
