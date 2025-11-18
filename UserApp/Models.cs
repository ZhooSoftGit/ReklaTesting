using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp
{
    public class RideRequestDto
    {
        #region Properties

        public int RideRequestId { get; set; }

        public DateTime? DropoffDateTime { get; set; }

        public double DropoffLatitude { get; set; }

        public string DropoffLocation { get; set; } = null!;

        public double DropoffLongitude { get; set; }

        public double? EstimatedDistance { get; set; }

        public int? EstimatedDuration { get; set; }

        public double? EstimatedFare { get; set; }

        public DateTime PickupDateTime { get; set; }

        public double PickupLatitude { get; set; }

        public string PickupLocation { get; set; } = null!;

        public double PickupLongitude { get; set; }

        public int? RentalHours { get; set; }

        public RideStatus RideStatus { get; set; }

        public RideTypeEnum RideType { get; set; }

        public int UserId { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        #endregion
    }

    public enum VehicleTypeEnum
    {
        Hatchback = 1,      // Compact small cars
        Sedan,          // Medium-sized cars with a separate trunk
        SUV,            // Sport Utility Vehicles
        MPV,            // Multi-Purpose Vehicles
        EV,             // Electric Vehicles
        Luxury,         // High-end luxury cars
        AutoRickshaw,   // Three-wheeler city ride
        BikeTaxi
    }
    public enum RideTypeEnum
    {
        Local = 0,
        Rental = 1,
        Outstation = 2
    }

    public class DriverLocation
    {
        public string DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public enum RideStatus
    {
        Requested,       // Ride just requested
        Scheduled,       // Ride is scheduled for a future time        
        Assigned,        // Driver assigned
        StartedToPickup, // Pickup
        Reached,         // Driver reached pickup point
        Started,         // Ride in progress
        Completed,       // Ride completed successfully
        Cancelled,       // Ride cancelled by user or driver
        Failed,          // System/payment/route failure
        NoDrivers,      // No driver accepted in time
        Rejected         // Driver explicitly rejected
    }

    public class RideMessage
    {
        public int BookingRequestId { get; set; }
        public int SenderId { get; set; }
        public string SenderType { get; set; } // "user" or "driver"
        public string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
