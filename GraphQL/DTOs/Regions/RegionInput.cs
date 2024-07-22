using System.ComponentModel.DataAnnotations;

namespace NZWalks.GraphQL.DTOs.Regions
{
    public class RegionInput
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string RegionImageUrl { get; set; }
    }
}
