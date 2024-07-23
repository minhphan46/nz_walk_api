using HotChocolate.Data.Filters;
using NZWalks.Entities;
using NZWalks.GraphQL.DTOs.Walks;

namespace NZWalks.GraphQL.Schema.Filters
{
    public class WalkFilterType : FilterInputType<WalkOutput>
    {
        protected override void Configure(IFilterInputTypeDescriptor<WalkOutput> descriptor)
        {
            descriptor.Ignore(w => w.WalkImageUrl);
            descriptor.Ignore(w => w.DifficultyId);
            descriptor.Ignore(w => w.RegionId);

            base.Configure(descriptor);
        }
    }
}
