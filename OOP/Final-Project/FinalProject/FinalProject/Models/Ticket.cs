﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }
        [Required]
        [MaxLength(50)]
        public string TicketNumber { get; }
        [Range(1, 1500)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string BookedByFullname { get; set; }
        [DataType(DataType.Duration)]
        public DateTime DepartureTime { get; set; }
        [DataType(DataType.Duration)]
        public DateTime ArrivalTime { get; set; }

        public int TicketClassAttributeId { get; set; }
        public int BusId { get; set; }
        public int SeatId { get; set; }
        public int DiscountReasonId { get; set; }
        public int CustomerId { get; set; }
        public int DisplayingId { get; set; }

        [ForeignKey("TicketClassAttributeId")]
        public TicketClassAttribute TicketClassAttribute { get; set; }
        [ForeignKey("BusId")]
        public Bus Bus { get; set; }
        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }
        [ForeignKey("DiscountReasonId")]
        public DiscountReason DiscountReason { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; }
        [ForeignKey("DisplayingId")]
        public Displaying Displaying { get; set; }

        [EnumDataType(typeof(TicketStatus))]
        public TicketStatus TicketStatus { get; set; }
        [EnumDataType(typeof(TicketType))]
        public TicketType TicketType { get; set; }
    }
}