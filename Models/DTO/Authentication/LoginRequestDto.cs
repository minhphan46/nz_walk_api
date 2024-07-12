using System.ComponentModel.DataAnnotations;

namespace UdemyProject1.Models.DTO.Authentication
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
