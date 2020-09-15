using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace The_River_Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SimpleTcpClient client = new SimpleTcpClient();

        string ss_name;
        string ss_ip;
        string ss_port;

        bool cntd = false;

        public MainWindow()
        {
            InitializeComponent();
            load_items();  
            client.DataReceived += Client_DataRecieved;  
        }


        public void load_items() //loading / writing server information from / to a specific file.
        {
            listwleft.Items.Clear(); // ListView start here (left side)
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "servers.file"))
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "servers.file");
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + line))
                    {
                        MessageBox.Show("Error while loading " + line + " Server");
                    }
                    else if(line !="")
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + line + @"\server_data.file"))
                        {
                            listwleft.Items.Add(line);
                        }
                        else
                        {
                            MessageBox.Show("Error while loading" + line + "server");
                        }
                    }
                    line = sr.ReadLine();
                }
            }
            else File.Create(AppDomain.CurrentDomain.BaseDirectory + "servers.file");
            listwleft.Items.Add("+");
            listwleft.SelectedItem = -1;
        }

        private void Client_DataRecieved(object sender, Message e)
        {
            if (e.MessageString.ToString() == "Server stopped!")
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>{ cntd = false; connect_btn.IsEnabled = true; }));
            }
            Application.Current.Dispatcher.Invoke(new Action(() => { chat_box.Text += "\n" + e.MessageString.ToString(); }));
        }

        private void connect_btn_Click(object sender, RoutedEventArgs e) //Connect button (top right)
        {
            connect_btn.IsEnabled = false;
            if (ss_name != null && ss_ip != null && ss_port != null)
            {
                try
                {
                    client.StringEncoder = Encoding.UTF8;
                    client.Connect(ss_ip, System.Convert.ToInt32(ss_port));
                    MessageBox.Show("Connected");
                    cntd = true;
                }
                catch
                {
                    MessageBox.Show("Connection Failed to Ip:" + ss_ip + "Port: " + ss_port);
                    connect_btn.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Connection Error");
                connect_btn.IsEnabled = true;
            }
        }

        private void send_btn_Click(object sender, RoutedEventArgs e) //Send button
        {
            if (cntd)
            {
                try
                {
                    client.WriteLine(text_tosend.Text);
                    text_tosend.Text = "";
                }
                catch
                {
                    MessageBox.Show("Failed to send message!");
                    connect_btn.IsEnabled = true;
                }
            }
            else MessageBox.Show("First connect to your server!");
        }
        private void visibility(Visibility v)
        {
            connect_btn.Visibility = v;
            text_tosend.Visibility = v;
            send_btn.Visibility = v;
            chat_box.Visibility = v;           
        }

        private void listwleft_SelectionChanged(object sender, SelectionChangedEventArgs e) //List view task (Server list)
        {
            try
            {
                client.Disconnect();
                connect_btn.IsEnabled = true;
            }
            catch { }
            ListView lv = (ListView)sender;
            if(lv.SelectedItem == "+")
            {
                addserver adds = new addserver();
                adds.ShowDialog();
                load_items();
            }
            else if(lv.SelectedItem != "+" && lv.SelectedItem != null && lv.SelectedIndex != -1)
            {
                string sname = null;
                string ip = null;
                string port = null;
                string message_file = null;
                bool hosting = false;

                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + lv.SelectedItem + @"\server_data.file");
                string line = sr.ReadLine();
                while(line != null)
                {
                    if (line.Contains("hosting:"))
                    {
                        line = line.Replace("hosting: ", "");
                        hosting = bool.Parse(line);
                    }
                    else if (line.Contains("name:"))
                    {
                        line = line.Replace("name: ", "");
                        sname = line;
                    }
                    else if (line.Contains("ip:"))
                    {
                        line = line.Replace("ip: ", "");
                        ip = line;
                    }
                    else if (line.Contains("port:"))
                    {
                        line = line.Replace("port: ", "");
                        port = line;
                    }
                    else if (line.Contains("messages:")) line = line.Replace("messages: ", "");
                    line = sr.ReadLine();
                }
                sr.Close();
                if (hosting == false)
                {
                    /*if (sname != null && ip != null && port != null && message_file != null)
                    {*/
                        ss_name = sname;
                        ss_ip = ip;
                        ss_port = port;
                        chatname.Content = sname;
                        visibility(Visibility.Visible);
                    //}
                }
                else if (hosting == true)
                {
                    /*if(sname != null && ip!= null && port != null && message_file != null)
                    {*/
                        visibility(Visibility.Hidden);
                        MessageBox.Show("Opening hosting interface to start you server", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        hosting hs = new hosting(sname, ip, port, message_file);
                        hs.Show();
                    //}
                }
            }
            
            
        }

        private void text_tosend_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (cntd)
                {
                    try
                    {
                        client.WriteLine(text_tosend.Text);
                        text_tosend.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("Failed to send message!");
                        connect_btn.IsEnabled = true;
                    }
                }
                else MessageBox.Show("First connect to your server!");
            }
        }
    }
}