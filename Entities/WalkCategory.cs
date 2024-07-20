namespace UdemyProject1.Entities
{
    public class WalkCategory
    {
        public Guid WalkId { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation properties
        public Walk Walk { get; set; }
        public Category Category { get; set; }
    }
}
