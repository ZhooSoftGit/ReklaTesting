using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;

namespace UserApp
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;

        private int _userId = 3;
        private int _requestId = 1;
        private string localurl = "https://localhost:7091/hubs/location";
        private string azureurl = "https://zhoodrivetracker-erg5hca6dcdtfzcn.canadacentral-01.azurewebsites.net/hubs/location";
        private string CurrentHubURL;
        public Form1()
        {
            InitializeComponent();

            CurrentHubURL = localurl;

        }

        private void AppendLog(string text)
        {
            if (txtLog.InvokeRequired)
                txtLog.Invoke(new Action(() => txtLog.AppendText(text + Environment.NewLine)));
            else
                txtLog.AppendText(text + Environment.NewLine);
        }

        private void neardriver_Click(object sender, EventArgs e)
        {
        }

        private async void Initialize_Click(object sender, EventArgs e)
        {
            var LocalhubUrl = $"{CurrentHubURL}";
            var hubUrl = $"{LocalhubUrl}?userId={_userId}&role=user";

            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            // register listeners
            _connection.On<int>("NoDriverAvailable", id => AppendLog($" No drivers for booking {id}"));
            _connection.On<object>("BookingConfirmed", data =>
            {
                AppendLog($"Booking Confirmed: {System.Text.Json.JsonSerializer.Serialize(data)}");
            });

            _connection.On<int>("BookingCancelled", id => AppendLog($" Booking cancelled {id}"));

            _connection.On<object>("ReceiveTripOtps", otps => AppendLog($" OTPs received: {System.Text.Json.JsonSerializer.Serialize(otps)}"));

            _connection.On<int>("StartPickupNotification", id => AppendLog($" Start Pickup Notification {id}"));
            _connection.On<int>("PickupReachedNotification", id => AppendLog($" Pickup Reached Notification {id}"));

            _connection.On<int>("TripStarted", id => AppendLog($" Trip started {id}"));
            _connection.On<int>("TripCompleted", id => AppendLog($" Trip completed {id}"));
            _connection.On<int>("TripCancelled", id => AppendLog($" Trip Cancelled {id}"));

            _connection.On<DriverLocation>("ReceiveDriverLocation", loc =>
            {
                AppendLog($"📍 Driver location: {loc.Latitude},{loc.Longitude}");
            });

            _connection.On<RideMessage>("ReceiveRideMessage", msg =>
            {
                AppendLog($"Message from driver: {msg.Message}");
            });

            AppendLog("Hub initialized. Now click Connect.");
        }

        private async void connectsignal_Click(object sender, EventArgs e)
        {
            await _connection.StartAsync();
            AppendLog(" Connected as User");
        }

        private async void BookingConfirmed_Click(object sender, EventArgs e)
        {
            var booking = new RideRequestDto
            {
                RideType = RideTypeEnum.Local,
                EstimatedFare = 250,
                EstimatedDistance = 12,
                PickupLocation = "Kempegowda International Airport",
                PickupLatitude = 11.0894,
                PickupLongitude = 77.0147,
                PickupDateTime = DateTime.Now,
                DropoffLocation = "MG Road Metro Station",
                DropoffLatitude = 10.9902,
                DropoffLongitude = 76.9629,
                RideRequestId = _requestId,
                UserId = _userId   // int ✅
            };

            await _connection.InvokeAsync("SendBookingRequest", booking);
            AppendLog($" Booking request {_requestId} sent.");
        }

        private async void cancelbooking_Click(object sender, EventArgs e)
        {
            var rideId = _requestId;
            await _connection.InvokeAsync("CancelBooking", rideId);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var rideId = _requestId;
            await _connection.InvokeAsync("CancelTripNotification", rideId);
        }

        private async void SendMsg_Click(object sender, EventArgs e)
        {
            var txt = messagebox.Text;
            var rideId = _requestId;
            await _connection.InvokeAsync("SendRideMessage", rideId, txt, "user", _userId);
        }
    }
}
