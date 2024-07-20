namespace UdemyProject1.Models.Domain
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
