using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;
using System.Threading.Tasks;

namespace UserApp
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;

        private ApiClient api;

        private int _userId = 14;
        private int _requestId = 1;
        private string huburl = "https://localhost:7091/hubs/location";
        //private string huburl = "https://zhoodrivetracker-erg5hca6dcdtfzcn.canadacentral-01.azurewebsites.net/hubs/location";
        private string CurrentHubURL;
        public Form1()
        {
            InitializeComponent();

            CurrentHubURL = huburl;

            // Choose environment
            string apiUrl = "https://localhost:7029/";
            //string apiUrl = "https://zhoodrive-b8hwb4hxdsg7eeby.centralindia-01.azurewebsites.net/";

            api = new ApiClient(apiUrl);
        }

        private void AppendLog(string text)
        {
            if (txtLog.InvokeRequired)
                txtLog.Invoke(new Action(() => txtLog.AppendText(text + Environment.NewLine)));
            else
                txtLog.AppendText(text + Environment.NewLine);
        }

        private async void neardriver_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _connection.InvokeAsync<List<DriverLocation>>("GetNearbyDrivers", 11.079842, 77.001138);
                string logValue = string.Join(", ",
                            result.Select(d => $"DriverId:{d.DriverId}, VehicleType:{d.VehicleType}")
                    );
                AppendLog($" near by driver: {logValue}");
            }
            catch (Exception ex)
            {
                AppendLog($" Error: {ex.Message}");
            }
        }

        private async void Initialize_Click(object sender, EventArgs e)
        {
            var LocalhubUrl = $"{CurrentHubURL}";
            var hubUrl = $"{LocalhubUrl}?userId={_userId}&role=user";

            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            _connection.Closed += async (error) =>
            {
                AppendLog(" Connection closed. Reconnecting...");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            // register listeners
            _connection.On<object>("BookingConfirmed", data =>
            {
                AppendLog($"Booking Confirmed: {System.Text.Json.JsonSerializer.Serialize(data)}");
            });

            _connection.On<RideEventModel>("OnTripNotification", data =>
            {
                AppendLog($"{data.RideRequestId}:{data.Status}");
            });

            _connection.On<DriverLocation>("ReceiveDriverLocation", loc =>
            {
                AppendLog($"📍 Driver location: {loc.Latitude},{loc.Longitude}");
            });

            _connection.On<int>("NoDriverAvailable", requestid =>
            {
                AppendLog($"No drivers available for ride {requestid}");
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
                PickupLatitude = 11.079842,
                PickupLongitude = 77.001138,
                PickupDateTime = DateTime.Now,
                DropoffLocation = "MG Road Metro Station",
                DropoffLatitude = 11.147958,
                DropoffLongitude = 77.041687,
                RideRequestId = _requestId,
                VehicleType = VehicleTypeEnum.SUV,
                UserId = _userId   // int ✅
            };

            var result = await api.SendRideRequest(booking);
            _requestId = result?.RideRequestId ?? 0;
            AppendLog($" Booking request {_requestId} sent.");
        }

        private async void cancelbooking_Click(object sender, EventArgs e)
        {
            var rideId = _requestId;
            await api.CancelRideAsync(rideId);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var rideId = _requestId;
            await api.CancelRideAsync(rideId);
        }

        private async void SendMsg_Click(object sender, EventArgs e)
        {
            var txt = messagebox.Text;
            var rideId = _requestId;
            await _connection.InvokeAsync("SendRideMessage", rideId, txt, "user", _userId);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var token = await api.VerifyOtpAsync(new VerifyOtpRequest
            {
                Code = "1234",
                DeviceKey = "device123",
                IpAddress = "192.168.0.1",
                PhoneNumber = "8344273150"
            });

            AppendLog("Token generated");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
