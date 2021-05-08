﻿using System;
using System.Collections.Generic;

namespace Assignment_02
{
    [Serializable]
    class Ride : ObjectPlus
    {
        #region Properties

        public Guid IdRide { get; set; }
        
        public Client Client { get; set; }
        public Driver Driver { get; set; }
        public List<Issue> Issues { get; set; }

        public string StartPoint { get; set; }
        public string DestinationPoint { get; set; }
        
        public DateTime RideDate { get; set; }  // Complex attribute

        public int? Bonus { get; set; }
        private static int? _taxRate;
        public static int Taxrate
        {
            get => _taxRate ?? 15;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Tax rate cannot be less than 0%");
                else if (value > 100)
                    throw new ArgumentException("Tax rate cannot be more than 100%");

                _taxRate = value;
            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            set => _totalPrice = value + (value / Taxrate) - (Bonus ?? 0);
        }

        #endregion

        #region Constructors

        private Ride(string startPoint, string destinationPoint, decimal totalPrice)
        {
            StartPoint = startPoint;
            DestinationPoint = destinationPoint;
            RideDate = DateTime.Now;
            TotalPrice = totalPrice;

            Issues = new List<Issue>();
        }

        private Ride(string startPoint, string destinationPoint, decimal totalPrice, int bonus)
        {
            StartPoint = startPoint;
            DestinationPoint = destinationPoint;
            RideDate = DateTime.Now;
            TotalPrice = totalPrice;
            Bonus = bonus;

            Issues = new List<Issue>();
        }

        #endregion

        #region With an attribute

        public static Ride AddRide(Client client, Driver driver, string startPoint, string destinationPoint, decimal totalPrice, int bonus = 0)
        {
            var Ride = new Ride(startPoint, destinationPoint, totalPrice, bonus)
            {
                Client = client ?? throw new NullReferenceException("The ride cannot have null value client!"),
                Driver = driver ?? throw new NullReferenceException("The ride cannot have null value driver!")
            };

            client.AddRide(Ride);
            driver.AddRide(Ride);

            return Ride;
        }

        public static void RemoveRide(Ride ride)
        {
            ride.Client.RemoveRide(ride);
            ride.Driver.RemoveRide(ride);
            ride.Client = null;
            ride.Driver = null;
        }

        #endregion

        #region Composition

        public void AddIssue(Issue newIssue)
        {
            if(newIssue != null && !Issues.Contains(newIssue))
            {
                Issues.Add(newIssue);
            }
        }

        #endregion

        public void ShowExtent()
        {
            Console.WriteLine($"Extenet of the class: {nameof(Ride)}");
            int count = 0; // Counter for rides

            foreach (Ride ride in Extent[this.GetType()])
            {
                Console.WriteLine($"----- Ride: {++count} ------");
                Console.WriteLine(ride.ToString());
            }
        }

        public override string ToString()
        {
            return $"Client: [{Client}],\nDriver: [{Driver}],\nStart point: [{StartPoint}],\nDestination point:[{DestinationPoint}]," +
                $"\nRide date: [{RideDate}],\nTotal price: [{TotalPrice}]";
        }
    }
}
