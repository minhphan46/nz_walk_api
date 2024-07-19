namespace UdemyProject1.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<WalkTag> WalkTags { get; set; }
    }
}
