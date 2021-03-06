﻿namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmodelannotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "Bus_IdBus", "dbo.Buses");
            DropForeignKey("dbo.Tickets", "DiscountReason_IdDiscountReason", "dbo.DiscountReasons");
            DropForeignKey("dbo.Tickets", "Seat_IdSeat", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "TicketClassAttribute_IdTicketClassAttribute", "dbo.TicketClassAttributes");
            DropForeignKey("dbo.Buses", "Driver_IdDriver", "dbo.Drivers");
            DropForeignKey("dbo.Rides", "DestinationPoint_IdLocation", "dbo.Locations");
            DropForeignKey("dbo.Rides", "StartPoint_IdLocation", "dbo.Locations");
            DropForeignKey("dbo.RideStops", "Location_IdLocation", "dbo.Locations");
            DropForeignKey("dbo.RideStops", "Ride_IdRide", "dbo.Rides");
            DropIndex("dbo.Tickets", new[] { "Bus_IdBus" });
            DropIndex("dbo.Tickets", new[] { "DiscountReason_IdDiscountReason" });
            DropIndex("dbo.Tickets", new[] { "Seat_IdSeat" });
            DropIndex("dbo.Tickets", new[] { "TicketClassAttribute_IdTicketClassAttribute" });
            DropIndex("dbo.Buses", new[] { "Driver_IdDriver" });
            DropIndex("dbo.Rides", new[] { "DestinationPoint_IdLocation" });
            DropIndex("dbo.Rides", new[] { "StartPoint_IdLocation" });
            DropIndex("dbo.RideStops", new[] { "Location_IdLocation" });
            DropIndex("dbo.RideStops", new[] { "Ride_IdRide" });
            DropColumn("dbo.Buses", "DriverId");
            RenameColumn(table: "dbo.Tickets", name: "Bus_IdBus", newName: "BusId");
            RenameColumn(table: "dbo.Tickets", name: "DiscountReason_IdDiscountReason", newName: "DiscountReasonId");
            RenameColumn(table: "dbo.Tickets", name: "Seat_IdSeat", newName: "SeatId");
            RenameColumn(table: "dbo.Tickets", name: "TicketClassAttribute_IdTicketClassAttribute", newName: "TicketClassAttributeId");
            RenameColumn(table: "dbo.Buses", name: "Driver_IdDriver", newName: "DriverId");
            RenameColumn(table: "dbo.Rides", name: "DestinationPoint_IdLocation", newName: "DestinationPointId");
            RenameColumn(table: "dbo.Rides", name: "StartPoint_IdLocation", newName: "StartPointId");
            RenameColumn(table: "dbo.RideStops", name: "Location_IdLocation", newName: "LocationId");
            RenameColumn(table: "dbo.RideStops", name: "Ride_IdRide", newName: "RideId");
            AddColumn("dbo.Tickets", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Rides", "StartStation", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Rides", "DestinationStation", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tickets", "BusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "DiscountReasonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "SeatId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "TicketClassAttributeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Buses", "BusName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Buses", "SeatCount", c => c.Int());
            AlterColumn("dbo.Buses", "DriverId", c => c.Int(nullable: false));
            AlterColumn("dbo.Drivers", "Firstname", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Drivers", "LastName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.DiscountReasons", "DiscountName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.TicketClassAttributes", "DiscountRate", c => c.Double());
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Displayings", "AvialableSeats", c => c.Int());
            AlterColumn("dbo.Rides", "DestinationPointId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rides", "StartPointId", c => c.Int(nullable: false));
            AlterColumn("dbo.Locations", "LocationName", c => c.String(nullable: false));
            AlterColumn("dbo.RideStops", "LocationId", c => c.Int(nullable: false));
            AlterColumn("dbo.RideStops", "RideId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TicketClassAttributeId");
            CreateIndex("dbo.Tickets", "BusId");
            CreateIndex("dbo.Tickets", "SeatId");
            CreateIndex("dbo.Tickets", "DiscountReasonId");
            CreateIndex("dbo.Tickets", "CustomerId");
            CreateIndex("dbo.Buses", "DriverId");
            CreateIndex("dbo.Rides", "StartPointId");
            CreateIndex("dbo.Rides", "DestinationPointId");
            CreateIndex("dbo.RideStops", "RideId");
            CreateIndex("dbo.RideStops", "LocationId");
            AddForeignKey("dbo.Tickets", "CustomerId", "dbo.Users", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Tickets", "BusId", "dbo.Buses", "IdBus", cascadeDelete: false);
            AddForeignKey("dbo.Tickets", "DiscountReasonId", "dbo.DiscountReasons", "IdDiscountReason", cascadeDelete: false);
            AddForeignKey("dbo.Tickets", "SeatId", "dbo.Seats", "IdSeat", cascadeDelete: false);
            AddForeignKey("dbo.Tickets", "TicketClassAttributeId", "dbo.TicketClassAttributes", "IdTicketClassAttribute", cascadeDelete: false);
            AddForeignKey("dbo.Buses", "DriverId", "dbo.Drivers", "IdDriver", cascadeDelete: true);
            AddForeignKey("dbo.Rides", "DestinationPointId", "dbo.Locations", "IdLocation", cascadeDelete: false);
            AddForeignKey("dbo.Rides", "StartPointId", "dbo.Locations", "IdLocation", cascadeDelete: false);
            AddForeignKey("dbo.RideStops", "LocationId", "dbo.Locations", "IdLocation", cascadeDelete: true);
            AddForeignKey("dbo.RideStops", "RideId", "dbo.Rides", "IdRide", cascadeDelete: true);
            DropColumn("dbo.Tickets", "TicketNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TicketNumber", c => c.Guid(nullable: false));
            DropForeignKey("dbo.RideStops", "RideId", "dbo.Rides");
            DropForeignKey("dbo.RideStops", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Rides", "StartPointId", "dbo.Locations");
            DropForeignKey("dbo.Rides", "DestinationPointId", "dbo.Locations");
            DropForeignKey("dbo.Buses", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Tickets", "TicketClassAttributeId", "dbo.TicketClassAttributes");
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "DiscountReasonId", "dbo.DiscountReasons");
            DropForeignKey("dbo.Tickets", "BusId", "dbo.Buses");
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.Users");
            DropIndex("dbo.RideStops", new[] { "LocationId" });
            DropIndex("dbo.RideStops", new[] { "RideId" });
            DropIndex("dbo.Rides", new[] { "DestinationPointId" });
            DropIndex("dbo.Rides", new[] { "StartPointId" });
            DropIndex("dbo.Buses", new[] { "DriverId" });
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropIndex("dbo.Tickets", new[] { "DiscountReasonId" });
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropIndex("dbo.Tickets", new[] { "BusId" });
            DropIndex("dbo.Tickets", new[] { "TicketClassAttributeId" });
            AlterColumn("dbo.RideStops", "RideId", c => c.Int());
            AlterColumn("dbo.RideStops", "LocationId", c => c.Int());
            AlterColumn("dbo.Locations", "LocationName", c => c.String());
            AlterColumn("dbo.Rides", "StartPointId", c => c.Int());
            AlterColumn("dbo.Rides", "DestinationPointId", c => c.Int());
            AlterColumn("dbo.Displayings", "AvialableSeats", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.TicketClassAttributes", "DiscountRate", c => c.Double(nullable: false));
            AlterColumn("dbo.DiscountReasons", "DiscountName", c => c.String());
            AlterColumn("dbo.Drivers", "LastName", c => c.String());
            AlterColumn("dbo.Drivers", "Firstname", c => c.String());
            AlterColumn("dbo.Buses", "DriverId", c => c.Int());
            AlterColumn("dbo.Buses", "SeatCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Buses", "BusName", c => c.String());
            AlterColumn("dbo.Tickets", "TicketClassAttributeId", c => c.Int());
            AlterColumn("dbo.Tickets", "SeatId", c => c.Int());
            AlterColumn("dbo.Tickets", "DiscountReasonId", c => c.Int());
            AlterColumn("dbo.Tickets", "BusId", c => c.Int());
            DropColumn("dbo.Rides", "DestinationStation");
            DropColumn("dbo.Rides", "StartStation");
            DropColumn("dbo.Tickets", "CustomerId");
            RenameColumn(table: "dbo.RideStops", name: "RideId", newName: "Ride_IdRide");
            RenameColumn(table: "dbo.RideStops", name: "LocationId", newName: "Location_IdLocation");
            RenameColumn(table: "dbo.Rides", name: "StartPointId", newName: "StartPoint_IdLocation");
            RenameColumn(table: "dbo.Rides", name: "DestinationPointId", newName: "DestinationPoint_IdLocation");
            RenameColumn(table: "dbo.Buses", name: "DriverId", newName: "Driver_IdDriver");
            RenameColumn(table: "dbo.Tickets", name: "TicketClassAttributeId", newName: "TicketClassAttribute_IdTicketClassAttribute");
            RenameColumn(table: "dbo.Tickets", name: "SeatId", newName: "Seat_IdSeat");
            RenameColumn(table: "dbo.Tickets", name: "DiscountReasonId", newName: "DiscountReason_IdDiscountReason");
            RenameColumn(table: "dbo.Tickets", name: "BusId", newName: "Bus_IdBus");
            AddColumn("dbo.Buses", "DriverId", c => c.Int(nullable: false));
            CreateIndex("dbo.RideStops", "Ride_IdRide");
            CreateIndex("dbo.RideStops", "Location_IdLocation");
            CreateIndex("dbo.Rides", "StartPoint_IdLocation");
            CreateIndex("dbo.Rides", "DestinationPoint_IdLocation");
            CreateIndex("dbo.Buses", "Driver_IdDriver");
            CreateIndex("dbo.Tickets", "TicketClassAttribute_IdTicketClassAttribute");
            CreateIndex("dbo.Tickets", "Seat_IdSeat");
            CreateIndex("dbo.Tickets", "DiscountReason_IdDiscountReason");
            CreateIndex("dbo.Tickets", "Bus_IdBus");
            AddForeignKey("dbo.RideStops", "Ride_IdRide", "dbo.Rides", "IdRide");
            AddForeignKey("dbo.RideStops", "Location_IdLocation", "dbo.Locations", "IdLocation");
            AddForeignKey("dbo.Rides", "StartPoint_IdLocation", "dbo.Locations", "IdLocation");
            AddForeignKey("dbo.Rides", "DestinationPoint_IdLocation", "dbo.Locations", "IdLocation");
            AddForeignKey("dbo.Buses", "Driver_IdDriver", "dbo.Drivers", "IdDriver");
            AddForeignKey("dbo.Tickets", "TicketClassAttribute_IdTicketClassAttribute", "dbo.TicketClassAttributes", "IdTicketClassAttribute");
            AddForeignKey("dbo.Tickets", "Seat_IdSeat", "dbo.Seats", "IdSeat");
            AddForeignKey("dbo.Tickets", "DiscountReason_IdDiscountReason", "dbo.DiscountReasons", "IdDiscountReason");
            AddForeignKey("dbo.Tickets", "Bus_IdBus", "dbo.Buses", "IdBus");
        }
    }
}
