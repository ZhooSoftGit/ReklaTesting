using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverAppTesting
{
    public class BookingRequestModel
    {
        public RideTypeEnum BookingType { get; set; }
        public string Fare { get; set; }
        public string DistanceAndPayment { get; set; }
        public string PickupLocation { get; set; }
        public double PickupLatitude { get; set; }
        public double PickupLongitude { get; set; }
        public string PickupAddress { get; set; }
        public string PickupTime { get; set; }
        public string DropoffLocation { get; set; }
        public double DropLatitude { get; set; }
        public double DropLongitude { get; set; }
        public int RemainingBids { get; set; }
        public int BoookingRequestId { get; set; }
        public string? UserName { get; set; }
        public int? DriverId { get; set; }
        public int UserId { get; set; }
    }

    public enum RideTypeEnum
    {
        Local = 0,
        Rental = 1,
        Outstation = 2
    }

    public class DriverLocation
    {
        public int DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
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
