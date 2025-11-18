namespace DriverAppTesting
{
    #region Enums

    public enum RideTypeEnum
    {
        Local = 0,
        Rental = 1,
        Outstation = 2
    }

    #endregion

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

    public class DriverLocation
    {
        #region Properties

        public int DriverId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion
    }

    public class RideMessage
    {
        #region Properties

        public int BookingRequestId { get; set; }

        public string Message { get; set; }

        public int SenderId { get; set; }

        public string SenderType { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        #endregion
    }

    public class VerifyOtpRequest
    {
        public string Code { get; set; }
        public string DeviceKey { get; set; }
        public string IpAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class VerifyOtpResponse
    {
        public TokenResponse TokenResponse { get; set; }
    }

    public class TokenResponse
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateBookingRequest
    {
        public int RideRequestId { get; set; }
        public int RideStatus { get; set; }  // cast from RideStatus enum
        public string Otp { get; set; }
        public bool ForceStart { get; set; }
    }

    public enum RideStatus
    {
        Requested,
        Scheduled,
        Assigned,
        StartedToPickup,
        Reached,
        Started,
        Completed,
        Cancelled,
        Failed,
        NoDrivers,
        Rejected
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


    public class ApiResponse<T>
    {
        #region Constructors

        public ApiResponse(T? response, string? message = null, bool success = true)
        {
            Response = response;
            Message = message;
            Success = success;
        }

        #endregion

        #region Properties

        public string? Message { get; set; } = string.Empty;// Informational message

        public T? Response { get; set; }

        public bool Success { get; set; }

        #endregion
    }


    public class RideServiceResponse
    {
        #region Properties

        public string? Message { get; set; }

        public bool Success { get; set; }

        public bool? AddCancellationCharge { get; set; }

        #endregion
    }

}
