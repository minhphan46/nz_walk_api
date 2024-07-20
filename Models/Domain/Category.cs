namespace UdemyProject1.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<WalkCategory> WalkCategories { get; set; }
    }
}
