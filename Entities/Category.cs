using NZWalks.Entities.Base;

namespace NZWalks.Entities
{
    public class Category : IBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<WalkCategory> WalkCategories { get; set; }
    }
}
