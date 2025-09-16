// See https://aka.ms/new-console-template for more information
// Change this to your Azure SignalR endpoint
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Data.Common;

var AzurehubUrl = "https://zhoodrivetracker-erg5hca6dcdtfzcn.canadacentral-01.azurewebsites.net/hubs/location";

var LocalhubUrl = "https://localhost:7091/hubs/location";

var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiODM0NDI3MzE1MiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImp0aSI6IjFhYzcwMzUzLWZjYmYtNGJjMS04ZWM1LTM2MDEwZWNmY2QyOSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJEcml2ZXIiLCJWZW5kb3IiXSwiZXhwIjoxNzU3NTg2MDUyLCJpc3MiOiJ6aG9vY2Fyc2FwaSIsImF1ZCI6Inpob28tbW9iaWxlLWNsaWVudHMifQ.NLIuWR4REnSwLLcVMThVJT44dBaZaxSCuCehcCASnS8";

var hubUrl = $"{AzurehubUrl}?userId={14}&role=user"; ; // Change to LocalhubUrl for local testing

var connection = new HubConnectionBuilder()
                    .WithUrl(hubUrl, options =>
                    {
                        // If your hub is protected with JWT
                        options.AccessTokenProvider = async () => token;
                        options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                    })
                    .WithAutomaticReconnect()
                    .Build();


// Subscribe to a hub method (must match your server hub method)


try
{
    Console.WriteLine("Connecting to SignalR hub...");
    await connection.StartAsync();
    Console.WriteLine("✅ Connected to hub!");

    // Optionally, invoke a hub method if your hub has one
    // e.g., connection.InvokeAsync("SendLocation", "driver123", 12.9716, 77.5946);

}
catch (Exception ex)
{
    Console.WriteLine($"❌ Connection failed: {ex.Message}");
}

Console.WriteLine("Press ENTER to exit...");
Console.ReadLine();
