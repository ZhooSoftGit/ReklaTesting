
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;

namespace DriverAppTesting
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;
        private int _driverId = 2;
        private System.Timers.Timer _locationTimer;
        private int _currentBookingId;
        private int _userId;

        public Form1()
        {
            InitializeComponent();


        }

        private void AppendLog(string text)
        {
            if (txtLog.InvokeRequired)
                txtLog.Invoke(new Action(() => txtLog.AppendText(text + Environment.NewLine)));
            else
                txtLog.AppendText(text + Environment.NewLine);
        }

        private async void Online_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            await _connection.StartAsync();
            AppendLog("✅ Connected as Driver");
        }

        private async void acceptrekla_Click(object sender, EventArgs e)
        {
            AppendLog(" Accepted booking");
            await _connection.InvokeAsync("RespondToBookingByDriver", _userId, _driverId, _currentBookingId, "assigned");
        }

        private async void cancelRekla_Click(object sender, EventArgs e)
        {
            await _connection.InvokeAsync("RespondToBookingByDriver", _userId, _driverId, _currentBookingId, "rejected");
            AppendLog("❌ Rejected booking");
        }

        private async void StartPickup_Click(object sender, EventArgs e)
        {
            await _connection.InvokeAsync("StartPickupNotification", _currentBookingId);
            AppendLog("📍 Started pickup");
        }

        private async void ReachedPickup_Click(object sender, EventArgs e)
        {
            await _connection.InvokeAsync("PickupReachedNotification", _currentBookingId);
            AppendLog("📍 Reached pickup");
        }

        private async void StartTrip_Click(object sender, EventArgs e)
        {
            var otp = "1234";  // add a textbox for driver to enter OTP
            await _connection.InvokeAsync("StartRide", _currentBookingId, otp);
            AppendLog($"🚖 Trip start attempted with OTP {otp}");
        }

        private async void EndTrip_Click(object sender, EventArgs e)
        {
            var otp = "1234";  // reuse textbox for End OTP
            await _connection.InvokeAsync("EndRide", _currentBookingId, otp);
            AppendLog($"🏁 Trip end attempted with OTP {otp}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _locationTimer = new System.Timers.Timer(2000);
            _locationTimer.Elapsed += async (_, __) =>
            {
                var loc = new DriverLocation { DriverId = _driverId, Latitude = 11.0797, Longitude = 76.9997 };
                await _connection.InvokeAsync("UpdateDriverLocation", loc);
            };
            _locationTimer.Start();
            AppendLog("📡 Location updates started");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _locationTimer?.Stop();
            AppendLog("📡 Location updates stopped");
        }

        private void Initialize_Click(object sender, EventArgs e)
        {
            var LocalhubUrl = "https://localhost:7091/hubs/location";
            var hubUrl = $"{LocalhubUrl}?userId={_driverId}&role=driver"; // ✅ driver role

            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            // listeners
            _connection.On<BookingRequestModel>("ReceiveBookingRequest", booking =>
            {
                _currentBookingId = booking.BoookingRequestId;
                _userId = booking.UserId;
                AppendLog($"📩 Booking received {booking.BoookingRequestId} from user {_userId}");
            });

            _connection.On<int>("BookingConfirmed", id => AppendLog($" Booking confirmed {id}"));
            _connection.On<int>("BookingExpired", id => AppendLog($" Booking expired {id}"));
            _connection.On<int>("BookingCancelled", id => AppendLog($" Booking cancelled {id}"));

            _connection.On<int>("TripStarted", id => AppendLog($"Trip started {id}"));
            _connection.On<int>("TripCompleted", id => AppendLog($" Trip completed {id}"));

            _connection.On<int>("TripCancelled", id => AppendLog($" Trip Cancelled {id}"));

            _connection.On<RideMessage>("ReceiveRideMessage", msg =>
            {
                AppendLog($"Message from driver: {msg.Message}");
            });

            AppendLog("Hub initialized. Now click Connect.");
        }

        private async void CancelRide_Click(object sender, EventArgs e)
        {
            await _connection.InvokeAsync("CancelTripNotification", _currentBookingId);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var txt = messagebox.Text;
            var rideId = _currentBookingId;
            await _connection.InvokeAsync("SendRideMessage", rideId, txt, "driver", _driverId);
        }
    }
}
