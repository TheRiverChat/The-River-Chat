using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace The_River_Chat
{
    /// <summary>
    /// Interaction logic for UserKick.xaml
    /// </summary>
    public partial class UserKick : UserControl
    {
        TcpClient tcc;
        public UserKick(TcpClient tc, string uname)
        {
            InitializeComponent();
            nm_lbl.Text = uname;
            tcc = tc;
        }

        private void kick_Click(object sender, RoutedEventArgs e)
        {
            tcc.Client.Send(Encoding.UTF8.GetBytes("Kick!"));
            tcc.Client.Disconnect(false);
            //this.Visibility = Visibility.Hidden;
        }
    }
}
