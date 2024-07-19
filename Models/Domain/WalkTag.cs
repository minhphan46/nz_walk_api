namespace UdemyProject1.Models.Domain
{
    public class WalkTag
    {
        public Guid WalkId { get; set; }
        public Guid TagId { get; set; }

        // Navigation properties
        public Walk Walk { get; set; }
        public Tag Tag { get; set; }
    }
}
