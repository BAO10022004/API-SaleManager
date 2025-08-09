using SaleManagerWebAPI.Interface.IResponsitories;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace SaleManagerWebAPI.Services
{
    public class DeviceServices : IDeviceResponsitories
    {
        public string GetDeviceIP()
        {
            try
            {
                using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
                socket.Connect("8.8.8.8", 65530);

                if (socket.LocalEndPoint is IPEndPoint endPoint)
                {
                    return endPoint.Address.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Can not get local devices IP : " + ex.Message);
            }

            throw new ArgumentNullException("Can not get local devices IP : ");
        }

        public string GetDeviceMAC()
        {
            try
            {
                // Tìm IP của interface đang kết nối internet
                string localIP;
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint?.Address.ToString();
                }

                if (string.IsNullOrEmpty(localIP))
                    return string.Empty;

                // Tìm MAC address của interface có IP này
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        var ipProperties = nic.GetIPProperties();
                        foreach (var ip in ipProperties.UnicastAddresses)
                        {
                            if (ip.Address.ToString() == localIP)
                            {
                                return nic.GetPhysicalAddress().ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting primary MAC: {ex.Message}");
            }

            return string.Empty;
        }
    }
}
