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
using System.Text.RegularExpressions;
using System.Threading;
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
        string DisplayName = "";

        bool cntd = false;
        bool encrypt = false;
        bool inkillprocess = false;

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

        private void Client_DataRecieved(object sender, Message e)  //IF CLIENT RECIVE DATA
        {
            if (e.MessageString.ToString() == "Server stopped!")
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { cntd = false; connect_btn.IsEnabled = true; MessageBox.Show("Server Stopped"); }));
            }else if (e.MessageString.ToString().Contains("Kick!"))
            {
                MessageBox.Show("You are kicked!");
            }
            else
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (encrypt && e.MessageString.ToString().Contains("ENCRYPTED MESSAGE from ") && !e.MessageString.ToString().Contains("Server stopped!") && !e.MessageString.ToString().Contains("Kick!")) //IF DATA HAS ENCRYPTED and server was not stopped and server was not kicked you
                    {
                        string s = e.MessageString.ToString();
                        s = s.Replace(s.Last().ToString(), "");
                        s = s.Replace("ENCRYPTED MESSAGE from ", "").Replace("\n", "").Replace("\r", "");
                        chat c = new chat(CustomDeEn.coder.Decrypt(s));
                        if (sv.VerticalOffset == sv.ScrollableHeight)
                        {
                            st.Children.Add(c);
                            sv.ScrollToEnd();
                        }
                        else
                        {
                            st.Children.Add(c);
                            nw_ms.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        chat c = new chat(e.MessageString.ToString());
                        if (sv.VerticalOffset == sv.ScrollableHeight)
                        {
                            st.Children.Add(c);
                            sv.ScrollToEnd();
                        }
                        else
                        {
                            st.Children.Add(c);
                            nw_ms.Visibility = Visibility.Visible;
                        }
                    }
                }));
            }
        }

        private void connect_btn_Click(object sender, RoutedEventArgs e) //Connect button (top right)
        {
            
            connect_btn.IsEnabled = false;
            if(DisplayName != null && DisplayName != "")
            {
                if (ss_name != null && ss_ip != null && ss_port != null)
                {
                    try
                    {
                        client.StringEncoder = Encoding.UTF8;
                        client.Connect(ss_ip, System.Convert.ToInt32(ss_port));
                        client.WriteLine(DisplayName);
                        //MessageBox.Show("Connected");
                        cntd = true;
                    }
                    catch
                    {
                        MessageBox.Show("Connection Failed to Ip:" + ss_ip + " Port: " + ss_port);
                        Task.Run(enable_cntd_btn);
                    }
                }
                else
                {
                    MessageBox.Show("Connection Error");
                    Task.Run(enable_cntd_btn);
                }
            }
            else
            {
                DisplayNameDockPanel.Visibility = Visibility.Visible;
            }

        }

        private async void enable_cntd_btn()
        {
            await Task.Delay(2000);
            Application.Current.Dispatcher.Invoke(new Action(() => { connect_btn.IsEnabled = true; }));
        }

        private void send_btn_Click(object sender, RoutedEventArgs e) //Send button
        {
            if (cntd)
            {
                try
                {
                    if (encrypt && text_tosend.Text != "") client.WriteLine("ENCRYPTED MESSAGE from " + CustomDeEn.coder.Encrypt(DisplayName + ": " + text_tosend.Text));
                    else if(text_tosend.Text != "") client.WriteLine(DisplayName + ": " + text_tosend.Text);
                    text_tosend.Text = "";
                }
                catch
                {
                    MessageBox.Show("Failed to send message! Try again in 2 seconds");
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
            sv.Visibility = v;           
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

        private async void text_tosend_KeyDown(object sender, KeyEventArgs e)
        {
            if (!inkillprocess)
            {
                if (e.Key == Key.Enter)
                {
                    if (cntd)
                    {
                        try
                        {
                            if (encrypt)
                            {
                                client.WriteLine("ENCRYPTED MESSAGE from " + CustomDeEn.coder.Encrypt(DisplayName + ": " + text_tosend.Text));
                            }

                            else client.WriteLine(DisplayName + ": " + text_tosend.Text);
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dname_box.Text != "")
            {
                DisplayName = dname_box.Text;
                DisplayNameDockPanel.Visibility = Visibility.Hidden;
                connect_btn.IsEnabled = true;
            }
            else MessageBox.Show("Fill all fields!");
        }

        private void char_bx_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (e.Key != Key.Back && e.Key != Key.Delete && t.Text != "" )
            {
                e.Handled = true;
            }
        }

        private void charbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (!t.Name.ToString().Contains("_8") && t.Text != "")
            {
                string next_box_name = t.Name.ToString();
                int id = int.Parse(next_box_name.Replace("charbox_", ""));
                id++;
                TextBox te = (TextBox)char_panel.FindName("charbox_" + id);
                te.Focus();
            }
        }

        private void charbox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (e.Key == Key.Back)
            {
                if (t.Text == "" && !t.Name.ToString().Contains("_1"))
                {
                    string next_box_name = t.Name.ToString();
                    int id = int.Parse(next_box_name.Replace("charbox_", ""));
                    id--;
                    TextBox te = (TextBox)char_panel.FindName("charbox_" + id);
                    te.Focus();
                }
            }else if(e.Key == Key.Left)
            {
                if (!t.Name.ToString().Contains("_1"))
                {
                    string next_box_name = t.Name.ToString();
                    int id = int.Parse(next_box_name.Replace("charbox_", ""));
                    id--;
                    TextBox te = (TextBox)char_panel.FindName("charbox_" + id);
                    te.Focus();
                }
            }else if(e.Key == Key.Right)
            {
                if (!t.Name.ToString().Contains("_8"))
                {
                    string next_box_name = t.Name.ToString();
                    int id = int.Parse(next_box_name.Replace("charbox_", ""));
                    id++;
                    TextBox te = (TextBox)char_panel.FindName("charbox_" + id);
                    te.Focus();
                }
            }
        }

        private void numbox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            int value = 0;

            //Char keyChar = (Char)System.Text.Encoding.ASCII.GetBytes(e.Key)[0];

            if (t.Text.ToString() != "") value = int.Parse(t.Text.ToString() + convert_to_string(e.Key));
            if(value > 255)
            {
                e.Handled = true;
            }
        }

        private void numbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void numbox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (e.Key == Key.Back)
            {
                if (t.Text == "" && !t.Name.ToString().Contains("_1"))
                {
                    string next_box_name = t.Name.ToString();
                    int id = int.Parse(next_box_name.Replace("numbox_", ""));
                    id--;
                    TextBox te = (TextBox)num_panel.FindName("numbox_" + id);
                    te.Focus();
                }
            }
            else if (e.Key == Key.Left)
            {
                if (!t.Name.ToString().Contains("_1"))
                {
                    string next_box_name = t.Name.ToString();
                    int id = int.Parse(next_box_name.Replace("numbox_", ""));
                    id--;
                    TextBox te = (TextBox)num_panel.FindName("numbox_" + id);
                    te.Focus();
                }
            }
            else if (e.Key == Key.Right)
            {
                if (!t.Name.ToString().Contains("_8"))
                {
                    string next_box_name = t.Name.ToString();
                    int id = int.Parse(next_box_name.Replace("numbox_", ""));
                    id++;
                    TextBox te = (TextBox)num_panel.FindName("numbox_" + id);
                    te.Focus();
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NotNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private string convert_to_string(Key k)
        {
            if (k == Key.D0)
            {
                return "0";
            }
            else if (k == Key.D1)
            {
                return "1";
            }
            else if (k == Key.D2)
            {
                return "2";
            }
            else if (k == Key.D3)
            {
                return "3";
            }
            else if (k == Key.D4)
            {
                return "4";
            }
            else if (k == Key.D5)
            {
                return "5";
            }
            else if (k == Key.D6)
            {
                return "6";
            }
            else if (k == Key.D7)
            {
                return "7";
            }
            else if (k == Key.D8)
            {
                return "8";
            }
            else if (k == Key.D9)
            {
                return "9";
            }
            else return "0";
        }

        private void SetEncryptKeys_Click(object sender, RoutedEventArgs e)
        {
            bool filled = true;
            string keys ="";
            List<int> nums = new List<int>();
            for (int i = 1; i < 9;)
            {
                TextBox te = (TextBox)char_panel.FindName("charbox_" + i);
                if (te.Text.ToString() == "")
                {
                    filled = false;
                }
                else keys += te.Text.ToString();
                i++;
            }
            for (int i = 1; i < 9;)
            {
                TextBox te = (TextBox)num_panel.FindName("numbox_" + i);
                if (te.Text.ToString() == "")
                {
                    filled = false;
                } else {
                    int num = int.Parse(te.Text.ToString());
                    nums.Add(num);
                }
                i++;
            }
            if(filled != true)
            {
                MessageBox.Show("Fill all fields");
                return;
            }
            CustomDeEn.coder.SecretKey = keys;
            byte[] b45 = { Convert.ToByte(nums[0]), Convert.ToByte(nums[1]), Convert.ToByte(nums[2]), Convert.ToByte(nums[3]), Convert.ToByte(nums[4]), Convert.ToByte(nums[5]), Convert.ToByte(nums[6]), Convert.ToByte(nums[7]) };
            CustomDeEn.coder.b4 = b45;

            /*string ecy = CustomDeEn.coder.Encrypt("Almás temető");
            MessageBox.Show(ecy);   ITS WORKING
            string decy = CustomDeEn.coder.Decrypt(ecy);
            MessageBox.Show(decy);*/

            encrypt = true;
            EncryptCodeDockPanel.Visibility = Visibility.Hidden;
            ECIcon_Locked.Visibility = Visibility.Visible;
            ECIcon_Open.Visibility = Visibility.Hidden;
        }

        private void ECButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!encrypt)
            {
                ECIcon_Open_Warning.Visibility = Visibility.Hidden;
                ECIcon_Open.Visibility = Visibility.Visible;
            }
        }

        private void ECButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!encrypt)
            {
                ECIcon_Open_Warning.Visibility = Visibility.Visible;
                ECIcon_Open.Visibility = Visibility.Hidden;
            }
        }

        private async void ECButton_Click(object sender, RoutedEventArgs e)
        {
            if (!encrypt)
            {
                encrypt = true;
                EncryptCodeDockPanel.Visibility = Visibility.Visible;
                await Task.Delay(100);
                encrypt = false;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure change encrypt keys?", "ENCRYPT KEYS", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    EncryptCodeDockPanel.Visibility = Visibility.Visible;
                    ECIcon_Locked.Visibility = Visibility.Hidden;
                    ECIcon_Open.Visibility = Visibility.Visible;
                    await Task.Delay(100);
                    encrypt = false;
                }
            }
        }

        private void nw_ms_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sv.ScrollToEnd();
            nw_ms.Visibility = Visibility.Hidden;
        }
    }
}