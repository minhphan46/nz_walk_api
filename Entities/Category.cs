namespace UdemyProject1.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<WalkCategory> WalkCategories { get; set; }
    }
}
