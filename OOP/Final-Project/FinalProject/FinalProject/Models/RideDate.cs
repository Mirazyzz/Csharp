﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class RideDate
    {
        [Key]
        public int IdRideData { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ride Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]        
        public DateTime Date { get; set; }

        public int RideId { get; set; }

        [ForeignKey("RideId")]
        public Ride Ride { get; set; }
    }
}
