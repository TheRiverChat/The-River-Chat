using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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


namespace The_River_Chat
{
    public partial class addserver : Window
    {
        public addserver()
        {
            InitializeComponent();
            try
            {
                serverip.Text = GetLocalIPAddress();
            }
            catch
            {
                MessageBox.Show("Error while getting you public IP address", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            serverport.Text = "2222";
        }     
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters exist with vaild IPv4 address");
        }
        public void snlf(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(servername.Text))
                servername.Text = "Server name";
        }
        public void spgf(object sender, EventArgs e)
        {
            if (serverport.Text =="Server port")
            {
                serverport.Text = "";
            }
        }
        public void splf(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serverport.Text))
                serverport.Text = "Server port";
        }     
        public void sagf(object sender, EventArgs e)
        {
            if(serverip.Text == "Server ip")
            {
                serverip.Text = "";
            }
        }
        public void salf (object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serverip.Text))
                serverip.Text = "Server IP";
        }
        private void host_btn_Click(object sender, RoutedEventArgs e)
        {
            if(servername.Text!="Server name"&& serverip.Text != "Server IP" && serverport.Text != "Server port" && servername.Text != ""&& serverip.Text!="" && serverport.Text !="")
            {
                MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + servername.Text);
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + servername.Text))
                {
                    MessageBox.Show("A server with this name already exist!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + servername.Text);
                    StreamWriter swb = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "servers.file");
                    swb.WriteLine(servername.Text);
                    swb.Close();
                    StreamWriter sws = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + servername.Text + @"\server_data.file");
                    serverip.Text = serverip.Text.Replace(" ", "");
                    sws.WriteLine("");
                    sws.WriteLine("name: " + servername.Text);
                    sws.WriteLine("ip: " + serverip.Text);
                    sws.WriteLine("port: " + serverport.Text);
                    sws.WriteLine("messages: messages.txt");
                    sws.WriteLine("hosting: true");
                    sws.Close();
                    MessageBox.Show("Opening Host interface", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    hosting hs = new hosting(servername.Text, serverip.Text, serverport.Text, "messages.txt");
                    hs.Show();
                    this.Close();
                }
            }
        }
        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (servername.Text != "" && serverip.Text != "" && serverport.Text != "")
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + servername.Text))
                {
                    MessageBox.Show("A server with this name already exist!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + servername.Text);
                    StreamWriter swb = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "servers.file");
                    swb.WriteLine(servername.Text);
                    swb.Close();
                    StreamWriter sws = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + servername.Text + @"\server_data.file");
                    sws.WriteLine("");
                    sws.WriteLine("name: " + servername.Text);
                    sws.WriteLine("ip: " + serverip.Text);
                    sws.WriteLine("port: " + serverport.Text);
                    sws.WriteLine("messages: messages.txt");
                    sws.WriteLine("hosting: false");
                    sws.Close();
                    MessageBox.Show("Server added successfully to the list", "INFO", MessageBoxButton.OK, MessageBoxImage.None);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("All the fields should be filled!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
