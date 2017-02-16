using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
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
using System.Collections.Concurrent;
using PacketRouter;

namespace PacketRouter
{
    /// <summary>
    /// Logica di interazione per TunnerController.xaml
    /// </summary>
    public partial class TunnerController : UserControl, IEndPointEventReceiver
    {

        protected class NetworkInterfaceItem
        {

            protected NetworkInterface mNetworkIntf;

            public NetworkInterface NetIntf
            {
                get { return mNetworkIntf; }
                set { mNetworkIntf = value; }
            }

            public override string ToString()
            {
                if (mNetworkIntf != null)
                {
                    string name = "";

                    name += mNetworkIntf.NetworkInterfaceType.ToString() + ": " + mNetworkIntf.Description;

                    return name;
                    
                }
                else return null;
            }

        }

        protected class IPAddressItem
        {
            protected UnicastIPAddressInformation mIpAddress;

            public UnicastIPAddressInformation IpAddress
            {
                get { return mIpAddress; }
                set { mIpAddress = value; }
            }

            public override string ToString()
            {
                if (mIpAddress != null) return mIpAddress.Address.ToString();
                else return "Error";
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// 

        protected EndPointTranceiver mChannelA;
        protected EndPointTranceiver mChannelB;


        public TunnerController()
        {
            InitializeComponent();

            netIntfACombo.SelectionChanged += NetIntfACombo_SelectionChanged;
            netIntfBCombo.SelectionChanged += NetIntfBCombo_SelectionChanged;

            LoadNetworkInterfaces();

            UpdateIPAddressesA(0);
            UpdateIPAddressesB(0);

        }

        private void NetIntfBCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aggiorno l'ipB
            UpdateIPAddressesB(0);

        }

        private void NetIntfACombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //aggiorno l'ipA
            UpdateIPAddressesA(0);
        }

        private void startChannelButton_Click(object sender, RoutedEventArgs e)
        {
            //verifico che il canale sia ben configurato
            if ( CheckChannelConfiguration() )
            {
                //canali disponibili
                mChannelA = new EndPointTranceiver(this);
                mChannelB = new EndPointTranceiver(this);

                EndPointConfiguration cA = new EndPointConfiguration();
                EndPointConfiguration cB = new EndPointConfiguration();

                if (GetCurrentChannelConfiguration(cA, cB) )
                {
                    ConcurrentQueue<QueuePacket> iAtoBq = new ConcurrentQueue<QueuePacket>();
                    ConcurrentQueue<QueuePacket> iBtoAq = new ConcurrentQueue<QueuePacket>();

                    cA.InputQueue = iAtoBq;
                    cB.InputQueue = iBtoAq;

                    cA.OutputQueue = iBtoAq;
                    cB.OutputQueue = iAtoBq;


                    mChannelA.Configuration = cA;
                    mChannelB.Configuration = cB;
                }
                else
                {
                    return;
                }

                mChannelA.StartEndPoint();
                mChannelB.StartEndPoint();
                
            }
            else
            {
                MessageBox.Show("Errore nell'apertura dei due endpoint");
                return;
            }

        }

        private void stopChannelButton_Click(object sender, RoutedEventArgs e)
        {
            if (mChannelA != null && mChannelB != null)
            {
                mChannelA.StopEndPoint();
                mChannelB.StopEndPoint();

                UpdateChannelStatusGUI(null, 0);
                
                //elimino le due code
                ConcurrentQueue<QueuePacket> qA = mChannelA.InputQueue;
                ConcurrentQueue<QueuePacket> qB = mChannelA.OutputQueue;

                QueuePacket obj = null;
                while (!qA.IsEmpty) { qA.TryDequeue(out obj); if (obj != null) obj.Dispose(); }
                while (!qB.IsEmpty) { qB.TryDequeue(out obj); if (obj != null) obj.Dispose(); }

                mChannelA.Dispose();
                mChannelB.Dispose();

                mChannelA = null;
                mChannelB = null;

            }

                 
        }

        private void LoadNetworkInterfaces()
        {
            //carico le due combobox
            netIntfACombo.Items.Clear();
            netIntfBCombo.Items.Clear();

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                NetworkInterfaceItem itemA = new NetworkInterfaceItem();
                itemA.NetIntf = nic;

                NetworkInterfaceItem itemB = new NetworkInterfaceItem();
                itemB.NetIntf = nic;

                netIntfACombo.Items.Add(itemA);
                netIntfBCombo.Items.Add(itemB);
            }

            netIntfACombo.SelectedIndex = 0;
            netIntfBCombo.SelectedIndex = 0;
            
        }
  
        private void UpdateIPAddressesA(int sel_idx)
        {
            netAddressesA.Items.Clear();

            try
            {
                NetworkInterfaceItem itemA = netIntfACombo.SelectedItem as NetworkInterfaceItem;
                NetworkInterface netA = itemA.NetIntf;
                IPInterfaceProperties propA = netA.GetIPProperties();

                //carico gli indirizzi dell'interfaccia B
                foreach (UnicastIPAddressInformation ip in propA.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) continue;

                    IPAddressItem item = new IPAddressItem();
                    item.IpAddress = ip;
                    netAddressesA.Items.Add(item);
                }
            }
            catch
            {
                MessageBox.Show("Errore nel caricamento degli indirizzi relativi all'interfaccia A");
                return;
            }

            try
            {
                netAddressesA.SelectedIndex = sel_idx;
            }
            catch
            {
                if (netAddressesA.Items.Count > 0) netAddressesA.SelectedIndex = 0;
            }
        }

        private void UpdateIPAddressesB(int sel_idx)
        {
            netAddressesB.Items.Clear();

            try
            {
                NetworkInterfaceItem itemB = netIntfBCombo.SelectedItem as NetworkInterfaceItem;
                NetworkInterface netB = itemB.NetIntf;
                IPInterfaceProperties propB = netB.GetIPProperties();

                //carico gli indirizzi dell'interfaccia B
                foreach (UnicastIPAddressInformation ip in propB.UnicastAddresses)
                {

                    if (ip.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) continue;

                    IPAddressItem item = new IPAddressItem();
                    item.IpAddress = ip;
                    netAddressesB.Items.Add(item);
                }
            }
            catch
            {
                MessageBox.Show("Errore nel caricamento degli indirizzi relativi all'interfaccia B");
                return;
            }

            try
            {
                netAddressesB.SelectedIndex = sel_idx;
            }
            catch
            {
                if ( netAddressesB.Items.Count > 0 ) netAddressesB.SelectedIndex = 0;
            }
        }

        private bool CheckChannelConfiguration()
        {

            //devo verificare se:
            //provo ad aprire le socket in listening, se ho errori le porte sono occupate

            Socket sockA = null;
            Socket sockB = null;
            
            try
            {
                //interfaccia A
                sockA = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress addrA = (netAddressesA.SelectedItem as IPAddressItem).IpAddress.Address;

                int portA = int.Parse(channAPort.Text);

                IPEndPoint endpA = new IPEndPoint(addrA, portA);

                sockA.Bind(endpA);


                //interfaccia B
                sockB = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress addrB = (netAddressesB.SelectedItem as IPAddressItem).IpAddress.Address;

                int portB = int.Parse(channBPort.Text);

                IPEndPoint endpB = new IPEndPoint(addrB, portB);

                sockB.Bind(endpB);

                //sbindo se possibile
                //la close non lancia eccezioni (non chiude pipe)
                if (sockA != null) sockA.Close();
                if (sockB != null) sockB.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());

                return false;
            }

            
            return true;
        }

        public bool GetCurrentChannelConfiguration(EndPointConfiguration endpA, EndPointConfiguration endpB)
        {
            try
            {
                IPAddress addrA = (netAddressesA.SelectedItem as IPAddressItem).IpAddress.Address;

                int portA = int.Parse(channAPort.Text);


                IPAddress addrB = (netAddressesB.SelectedItem as IPAddressItem).IpAddress.Address;

                int portB = int.Parse(channBPort.Text);


                if (endpA != null && endpB != null)
                {
                    endpA.ListeningAddress = addrA;
                    endpA.ListeningPort = portA;

                    endpB.ListeningAddress = addrB;
                    endpB.ListeningPort = portB;

                    return true;
                }
            }
            catch
            {
                return false;
            }


            return false;
        }


        public void OnRemoteConnection(EndPointTranceiver src, IPAddress remoteIp, int remotePort)
        {
            UpdateChannelStatusGUI(remoteIp, remotePort);
        }

        public void OnRemoteDisconnetion(EndPointTranceiver src)
        {
            UpdateChannelStatusGUI(null, 0);
        }

        public void OnRemotePacketReceived(EndPointTranceiver src)
        {
            
        }

        public void OnGenericError(EndPointTranceiver src, int errorcode)
        {
            UpdateChannelStatusGUI(null, 0);
        }

        public void OnChannelReady(EndPointTranceiver src)
        {
            UpdateChannelStatusGUI(null, 0);
        }

        protected void UpdateChannelStatusGUI(IPAddress addr, int port)
        {
            if (mChannelA == null || mChannelB == null) return;

            ChannelStatusEnum status_a = mChannelA.ChannelStatus;
            ChannelStatusEnum status_b = mChannelB.ChannelStatus;
            
            ////caanale A
            if ( status_a == ChannelStatusEnum.Idle )
            {
                channelStatusA.Text = "NC";
                channelStatusA.Background = new SolidColorBrush(Colors.Red);
                remoteEndpointAText.Text = "Non Connesso";

            }
            else if (status_a == ChannelStatusEnum.Listening )
            {
                channelStatusA.Text = "LIST";
                channelStatusA.Background = new SolidColorBrush(Colors.Yellow);
                remoteEndpointAText.Text = "Non Connesso";
            }
            else if (status_a == ChannelStatusEnum.Active )
            {
                channelStatusA.Text = "ON";
                channelStatusA.Background = new SolidColorBrush(Colors.Green);

                if ( addr != null ) remoteEndpointAText.Text = addr.ToString() + ":" + port;
            }

            ////canale B
            if (status_b == ChannelStatusEnum.Idle)
            {
                channelStatusB.Text = "NC";
                channelStatusB.Background = new SolidColorBrush(Colors.Red);
                remoteEndpointBText.Text = "Non Connesso";
            }
            else if (status_b == ChannelStatusEnum.Listening)
            {
                channelStatusB.Text = "LIST";
                channelStatusB.Background = new SolidColorBrush(Colors.Yellow);
                remoteEndpointBText.Text = "Non Connesso";
            }
            else if (status_b == ChannelStatusEnum.Active)
            {
                channelStatusB.Text = "ON";
                channelStatusB.Background = new SolidColorBrush(Colors.Green);

                if (addr != null) remoteEndpointBText.Text = addr.ToString() + ":" + port;
            }
        }
    }
}
