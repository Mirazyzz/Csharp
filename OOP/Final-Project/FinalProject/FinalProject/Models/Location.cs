﻿using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Location
    {
        [Key]
        public int IdLocation { get; set; }
        [Required]
        public string LocationName { get; set; }
    }
}