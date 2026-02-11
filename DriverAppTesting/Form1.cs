
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace DriverAppTesting
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;
        private int _driverId = 1;
        private int _currentBookingId = 3;
        private int _userId;
        private RideRequestDto _currentBooking;
        private Location _default = new Location { Latitude = 11.0797, Longitude = 76.9997 };
        private string _number = "8344273152";
        private string _otp = "1234";
        private List<Location> _route = new List<Location>();
        private Location _lastLocation;

        //private string huburl = "https://localhost:7091/hubs/location";
        private string huburl = "https://zhoodrivetracker-erg5hca6dcdtfzcn.canadacentral-01.azurewebsites.net/hubs/location";
        private string CurrentHubURL;

        private MapHelper mapHelper;

        private ApiClient api;
        public Form1()
        {
            InitializeComponent();
            CurrentHubURL = huburl;

            //CurrentHubURL = localurl;

            // Choose environment
            //string apiUrl = "https://localhost:7029/";
            string apiUrl = "https://zhoodrive-b8hwb4hxdsg7eeby.centralindia-01.azurewebsites.net/";

            api = new ApiClient(apiUrl);
            mapHelper = new MapHelper();

            _route = new List<Location> { { _default } };
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
            try
            {
                txtLog.Clear();

                await api.DriverOnRideAsync(new DriverStatusDto { Status = DriverStatusEnum.Online });
                AppendLog("✅ Connected as Driver");
            }
            catch (Exception ex)
            {
            }
        }

        private async void acceptrekla_Click(object sender, EventArgs e)
        {
            AppendLog(" Accepted booking");
            var resp = await api.CreateRideAsync(new AcceptRideRequest
            {
                RideRequestId = _currentBookingId,
                DriverId = _driverId
            });
            //await _connection.InvokeAsync("RespondToBookingByDriver", _userId, _driverId, _currentBookingId, "assigned");
        }



        private async void cancelRekla_Click(object sender, EventArgs e)
        {
            // await _connection.InvokeAsync("RespondToBookingByDriver", _userId, _driverId, _currentBookingId, "rejected");
            await api.CancelRideAsync(_currentBookingId);
            AppendLog("❌ Rejected booking");
        }

        private async void StartPickup_Click(object sender, EventArgs e)
        {
            await UpdateBoking(new UpdateBookingRequest
            {
                RideRequestId = _currentBookingId,
                RideStatus = (int)RideStatus.StartedToPickup
            });

            //await _connection.InvokeAsync("StartPickupNotification", _currentBookingId);
            AppendLog("📍 Started pickup");

            var locations = await mapHelper.GetRoutePoints(_default.Latitude, _default.Longitude, _currentBooking.PickupLatitude.Value,
                _currentBooking.PickupLongitude.Value);

            _route = new List<Location>();

            _routeIndex = 0;

            _route = locations.Locations;
        }

        private async void ReachedPickup_Click(object sender, EventArgs e)
        {
            await UpdateBoking(new UpdateBookingRequest
            {
                RideRequestId = _currentBookingId,
                RideStatus = (int)RideStatus.Reached
            });
            //await _connection.InvokeAsync("PickupReachedNotification", _currentBookingId);
            AppendLog("📍 Reached pickup");

            _route = new List<Location> { new Location
            {
                Latitude = _currentBooking.PickupLatitude.Value,
                Longitude = _currentBooking.PickupLongitude.Value
            } };
        }

        private async void StartTrip_Click(object sender, EventArgs e)
        {
            var otp = OTPTextBox.Text;  // add a textbox for driver to enter OTP

            var result = await UpdateBoking(new UpdateBookingRequest
            {
                RideRequestId = _currentBookingId,
                RideStatus = (int)RideStatus.Started,
                Otp = otp
            });
            //await _connection.InvokeAsync("StartRide", _currentBookingId, otp);
            AppendLog($"🚖 Trip start attempted with OTP {otp}");

            if (result != null)
            {
                var locations = await mapHelper.GetRoutePoints(_currentBooking.PickupLatitude.Value, _currentBooking.PickupLongitude.Value,
                    _currentBooking.DropoffLatitude.Value, _currentBooking.DropoffLongitude.Value);

                _routeIndex = 0;
                _route = locations.Locations;

            }
        }

        private async void EndTrip_Click(object sender, EventArgs e)
        {
            var otp = OTPTextBox.Text;  // reuse textbox for End OTP

            await EndBooking(new UpdateBookingRequest
            {
                RideRequestId = _currentBookingId,
                RideStatus = (int)RideStatus.Completed,
                Otp = otp
            });
            //await _connection.InvokeAsync("EndRide", _currentBookingId, otp);
            AppendLog($"🏁 Trip end attempted with OTP {otp}");

            _route = new List<Location>();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            _locationTimer?.Stop();
            AppendLog("📡 Location updates stopped");
        }

        private async void Initialize_Click(object sender, EventArgs e)
        {
            var LocalhubUrl = $"{CurrentHubURL}";
            var hubUrl = $"{LocalhubUrl}?userId={_driverId}&role=driver"; // ✅ driver role

            _connection = new HubConnectionBuilder()
                            .WithUrl(hubUrl, options =>
                            {
                                options.AccessTokenProvider = () => Task.FromResult(ApiClient._token);
                                options.Transports = HttpTransportType.LongPolling;
                            })
                             .ConfigureLogging(logging =>
                             {
                                 logging.SetMinimumLevel(LogLevel.Trace);
                             })
                            .Build();

            try
            {
                await _connection.StartAsync();

                _connection.Closed += async (error) =>
                {
                    AppendLog(" Connection closed. Reconnecting...");
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await _connection.StartAsync();
                };
            }
            catch (Exception ex)
            {
                AppendLog($"Connection error: {ex.Message}");
            }


            // listeners
            _connection.On<RideRequestDto>("ReceiveBookingRequest", booking =>
            {
                _currentBookingId = booking.RideRequestId;
                _userId = booking.UserId;
                _currentBooking = booking;
                AppendLog($"📩 Booking received {booking.RideRequestId} from user {_userId}. Fare {booking.EstimatedFare}");
            });

            _connection.On<int>("BookingConfirmed", id => AppendLog($" Booking confirmed {id}"));

            _connection.On<RideEventModel>("OnTripNotification", data => AppendLog($"{data.RideRequestId}:{data.Status}"));


            _connection.On<RideMessage>("ReceiveRideMessage", msg =>
            {
                AppendLog($"Message from driver: {msg.Message}");
            });

            AppendLog("Hub initialized. Now click Connect.");
        }

        private async void CancelRide_Click(object sender, EventArgs e)
        {
            var result = await api.CancelRideAsync(_currentBookingId);
            AppendLog($"Ride has been cancelled. {result.Success}");

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var txt = messagebox.Text;
            var rideId = _currentBookingId;
            await _connection.InvokeAsync("SendRideMessage", rideId, txt, "driver", _driverId);
        }

        private async void GetToken_Click(object sender, EventArgs e)
        {
            var token = await api.VerifyOtpAsync(new VerifyOtpRequest
            {
                Code = "1234",
                DeviceKey = "device123",
                IpAddress = "192.168.0.1",
                PhoneNumber = "8344273152"
            });
        }


        private async Task<bool> UpdateBoking(UpdateBookingRequest req)
        {
            return await api.UpdateRideStatusAsync(req);
        }

        private async Task EndBooking(UpdateBookingRequest req)
        {
            var tt = await api.EndRideStatusAsync(req);
            if (tt != null)
            {
                AppendLog($"Ride ended. Total paid: {tt.TotalPaid} {tt.Currency}, Driver: {tt.DriverName}");
            }
        }

        private async void GetToken_Click_1(object sender, EventArgs e)
        {
            var token = await api.VerifyOtpAsync(new VerifyOtpRequest
            {
                Code = "1234",
                DeviceKey = "device123",
                IpAddress = "192.168.0.1",
                PhoneNumber = "8344273152"
            });

            AppendLog("Token generated");
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var data = await api.GetRideInfo();

            if (data != null)
            {
                AppendLog($"Current Ride Id: {data.RideRequestId}, Status: {data.RideStatus}");
                _currentBooking = data;
                _currentBookingId = data.RideRequestId;
            }
            else
            {
                AppendLog("No ongoing ride found.");
            }
        }


        private System.Timers.Timer _locationTimer;
        private int _routeIndex = 0;

        private void UpdateLocation(object sender, EventArgs e)
        {
            _locationTimer = new System.Timers.Timer(2000);

            _locationTimer.Elapsed += async (_, __) =>
            {
                if (_route.Count > _routeIndex)
                {
                    var point = _route[_routeIndex++];
                    var loc = new DriverLocation
                    {
                        DriverId = _driverId,
                        Latitude = point.Latitude,
                        Longitude = point.Longitude
                    };

                    if (_connection.State != HubConnectionState.Connected)
                        await _connection.StartAsync();

                    await _connection.InvokeAsync("UpdateDriverLocation", loc);
                }
            };

            _locationTimer.Start();
            AppendLog("📡 Route simulation started");
        }

        private void reset_Location(object sender, EventArgs e)
        {
            _route = new List<Location> { { _default } };
        }
    }
}
