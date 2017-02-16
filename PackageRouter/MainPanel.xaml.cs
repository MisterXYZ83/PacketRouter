using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace PacketRouter
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainPanel : Window, IEndPointEventReceiver
    {
        public MainPanel()
        {
            InitializeComponent();
        }

        public void OnChannelReady(EndPointTranceiver src)
        {
        }

        public void OnGenericError(EndPointTranceiver src, int errorcode)
        {
        }

        public void OnRemoteConnection(EndPointTranceiver src, IPAddress remoteIp, int remotePort)
        {
        }

        public void OnRemoteDisconnetion(EndPointTranceiver src)
        {
        }

        public void OnRemotePacketReceived(EndPointTranceiver src)
        {
        }
    }
}
