﻿using System.ComponentModel.DataAnnotations;

namespace UdemyProject1.Models.DTO.Tag
{
    public class AddTagDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }
    }
}
