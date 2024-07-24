using NZWalks.Entities.Base;

namespace NZWalks.Entities
{
    public class Region : IBase
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string RegionImageUrl { get; set; }
    }
}
