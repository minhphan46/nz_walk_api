using System.ComponentModel.DataAnnotations;

namespace UdemyProject1.Models.DTO.Media
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FileDescription { get; set; }
    }
}
