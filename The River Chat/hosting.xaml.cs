using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SimpleTCP;
using System.Net;
using System.Net.Sockets;
using System.Xml.Schema;
using Mono.Nat;

namespace The_River_Chat
{
    public partial class hosting : Window
    {
        string name;
        string ip;
        string port;
        string messages;
        bool po = false;

        SimpleTcpServer server;
        List<TcpClient> cl = new List<TcpClient>();

        public hosting(string srv_name, string srv_ip, string srv_port, string message_file)
        {
            InitializeComponent(); // Setting values for the variables
            name = srv_name;
            ip = srv_ip;
            port = srv_port;
            messages = message_file;

            //Definying contents for each label
            label_name.Content = name;
            label_ip.Content = ip;
            label_port.Content = port;

            server = new SimpleTcpServer();
            server.Delimiter = 0x13; // enter

            server.DataReceived += Server_DataReceived;
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.StringEncoder = Encoding.UTF8;
            startserver_btn.IsEnabled = true;
        }
        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            MessageBox.Show("Client Disconnected");
            cl.Remove(e);
        }

        private void Server_DataReceived(object sender, Message e)
        {
            foreach(TcpClient tc in cl)
            {
                try
                {
                    tc.Client.Send(Encoding.UTF8.GetBytes(e.MessageString));
                }
                catch { }
            }
        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            cl.Add(e);
            MessageBox.Show("Client Connected");
        }

        private void startserver_btn_Click(object sender, RoutedEventArgs e) //Server start button function
        {
            if(startserver_btn.Content.ToString() == "Start server")
            {
                
                IPAddress nip = IPAddress.Parse(ip);
                server.Start(nip, Convert.ToInt32(port));
                startserver_btn.Content = "Stop hosting";
                MessageBox.Show("Server started!");
            } else
            {
                if (server.IsStarted)
                {
                    foreach (TcpClient tc in cl)
                    {
                        try
                        {
                            tc.Client.Send(Encoding.ASCII.GetBytes("Server stopped!"));
                            tc.Close();
                        }
                        catch { }
                    }
                    server.Stop();
                    startserver_btn.Content = "Start server";
                }
            }

        }

        private void UPNP_btn_Click(object sender, RoutedEventArgs e)
        {
            UPNP_btn.IsEnabled = false;
            NatUtility.DeviceFound += NatUtility_DeviceFound;
            NatUtility.StartDiscovery();
        }

        private void NatUtility_DeviceFound(object sender, DeviceEventArgs e)
        {
            INatDevice device = e.Device;
            device.CreatePortMap(new Mapping(Protocol.Tcp, int.Parse(port), int.Parse(port)));
            if(po == false)
            {
                po = true;
                MessageBox.Show("Port (" + port + ") opened successfully");
            }
        }
    }
   
}
