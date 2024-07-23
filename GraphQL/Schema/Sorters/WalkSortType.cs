using HotChocolate.Data.Sorting;
using NZWalks.GraphQL.DTOs.Walks;

namespace NZWalks.GraphQL.Schema.Sorters
{
    public class WalkSortType : SortInputType<WalkOutput>
    {
        protected override void Configure(ISortInputTypeDescriptor<WalkOutput> descriptor)
        {
            descriptor.Ignore(c => c.Id);
            descriptor.Ignore(c => c.WalkImageUrl);
            descriptor.Ignore(w => w.DifficultyId);
            descriptor.Ignore(w => w.RegionId);

            base.Configure(descriptor);
        }
    }
}
