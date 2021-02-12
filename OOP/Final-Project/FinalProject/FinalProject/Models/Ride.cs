﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Ride
    {
        [Key]
        public int IdRide { get; set; }
        public double TotalHours { get; set; }
        public int StartPointId { get; set; }
        public int DestinationPointId { get; set; }
        [Required]
        [MaxLength(50)]
        public string StartStation { get; set; }
        [Required]
        [MaxLength(50)]
        public string DestinationStation { get; set; }

        [ForeignKey("StartPointId")]
        public Location StartPoint { get; set; }
        [ForeignKey("DestinationPointId")]
        public Location DestinationPoint { get; set; }
    }
}