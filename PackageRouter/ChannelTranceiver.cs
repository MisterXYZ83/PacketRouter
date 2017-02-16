using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace PacketRouter
{
    //gestisce i thread di comunicazione
    //e le code messaggi

    public class QueuePacket : IDisposable
    {
        public int SequenceNumber;
        public byte[] PayloadData;

        public void Dispose()
        {
            PayloadData = null;
        }
    }

    public class EndPointConfiguration
    {
        public ConcurrentQueue<QueuePacket> InputQueue;
        public ConcurrentQueue<QueuePacket> OutputQueue;

        public IPAddress ListeningAddress;
        public int ListeningPort;

    }

    public interface IEndPointEventReceiver
    {
        void OnChannelReady(EndPointTranceiver src);
        void OnRemoteConnection (EndPointTranceiver src, IPAddress remoteIp, int remotePort);
        void OnRemoteDisconnetion(EndPointTranceiver src);
        void OnRemotePacketReceived(EndPointTranceiver src);
        void OnGenericError(EndPointTranceiver src, int errorcode);
    }


    public enum ChannelStatusEnum
    {
        Listening, 
        Active,
        Idle
    }

    public class EndPointTranceiver : IDisposable
    {
        
        //coda dei pacchetti ricevuti: interna all'endpoint
        protected ConcurrentQueue<QueuePacket> mOutputQueue;
        //coda dei pacchetti da trasmettere: esterna
        protected ConcurrentQueue<QueuePacket> mInputQueue;

        protected int mListeningPort;
        protected IPAddress mListeningAddress;

        protected bool mConfigured;

        protected bool mTranceiverShouldQuit;

        //thread del canale
        //riceve i dati da una socket e li accoda nella read queue;
        //invia i dati presenti nella send queue
        protected Thread mChannelThread;
        protected ChannelStatusEnum mChannelStatus;
        protected object mChannelLock;

        protected IEndPointEventReceiver mEventReceiver;

        public EndPointTranceiver(IEndPointEventReceiver recv)
        {
            mOutputQueue = null;
            mInputQueue = null;

            //thread di rx
            mChannelThread = null;

            mConfigured = false;

            mEventReceiver = recv;

            mChannelLock = new object();

            mChannelStatus = ChannelStatusEnum.Idle;
        }

        
        public ChannelStatusEnum ChannelStatus
        {
            get { return mChannelStatus; }
        }

        public ConcurrentQueue<QueuePacket> InputQueue
        {
            get { return mInputQueue; }
        }

        public ConcurrentQueue<QueuePacket> OutputQueue
        {
            get { return mOutputQueue; }
        }
        
        public EndPointConfiguration Configuration
        {
            set
            {
                EndPointConfiguration config = value as EndPointConfiguration;

                if ( config == null )
                {
                    mConfigured = false;
                    return;
                }
                else
                {
                    mInputQueue = config.InputQueue;
                    mOutputQueue = config.OutputQueue;

                    mListeningAddress = config.ListeningAddress;
                    mListeningPort = config.ListeningPort;

                    mConfigured = true;
                }
                
            }
        }

        public void StartEndPoint()
        {
            mTranceiverShouldQuit = false;

            mChannelThread = new Thread(ChannelTask);
            mChannelThread.Name = "Channel_Task";
            mChannelThread.Start(this);

            return;
        }

        public void StopEndPoint()
        {
            mTranceiverShouldQuit = true;

            try
            {
                mChannelThread.Join();
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// routine del thread di rx
        /// </summary>
        protected static void ChannelTask ( object param )
        {
            EndPointTranceiver thiz = param as EndPointTranceiver;
            int seq_num = 0;
            bool quit_loop = false;
            bool back_accept = false;

            thiz.mChannelStatus = ChannelStatusEnum.Idle;

            if (!thiz.mConfigured) return;

            //1. apro la socket in listening, 1 solo slot in backlog
            Socket listSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket clientSock = null;

            IPEndPoint endp = new IPEndPoint(thiz.mListeningAddress, thiz.mListeningPort);

            //1a. bind
            //1b. listen
            try
            {
                listSock.Bind(endp);
                listSock.Listen(1);
                thiz.mChannelStatus = ChannelStatusEnum.Listening;

                if (thiz.mEventReceiver != null)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        thiz.mEventReceiver.OnChannelReady(thiz);
                    }));
                }
            }
            catch
            {
                if (thiz.mEventReceiver != null)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        thiz.mEventReceiver.OnGenericError(thiz, -2);
                    }));
                }
                return;
            }

            //loop thread
            while (true)
            {

                try
                {
                    //loop ACCEPT
                    while ( true )
                    {
                        if (listSock.Poll(10000, SelectMode.SelectRead))
                        {
                            clientSock = listSock.Accept();
                            break;
                        }
                        else
                        {
                            if ( thiz.mTranceiverShouldQuit )
                            {
                                quit_loop = true;

                                break;
                            }
                        }
                    }

                    if (quit_loop)
                    {
                        thiz.mChannelStatus = ChannelStatusEnum.Idle;
                        
                        listSock.Close();

                        if (clientSock != null)
                        {
                            clientSock.Shutdown(SocketShutdown.Both);
                            clientSock.Close();
                        }

                        return;
                    }

                    //configuro la socket di ricezione 
                    //il canale è attivo
                    thiz.mChannelStatus = ChannelStatusEnum.Active;

                    //notifico la connessione al controller
                    if ( thiz.mEventReceiver != null )
                    {
                        IPEndPoint remote_endp = clientSock.RemoteEndPoint as IPEndPoint;

                        Application.Current.Dispatcher.Invoke(new Action(() => 
                        {
                            thiz.mEventReceiver.OnRemoteConnection(thiz, remote_endp.Address, remote_endp.Port);
                        })); 
                    }
                }
                catch
                {
                    //uscita ACCEPT
                    listSock.Close();
                    listSock = null;

                    try
                    {
                        if (clientSock != null)
                        {
                            clientSock.Shutdown(SocketShutdown.Both);
                            clientSock.Close();
                        }
                    }
                    catch
                    {

                    }
                    
                    thiz.mChannelStatus = ChannelStatusEnum.Idle;

                    /*if (thiz.mEventReceiver != null)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            thiz.mEventReceiver.OnGenericError(thiz, -1);
                        }));
                    }*/

                    return;
                }

                //LOOP RX/TX
                try
                {
                    while (true)
                    {
                        //attesa RX per 50ms
                        if (clientSock.Poll(50000, SelectMode.SelectRead))
                        {
                            //ci sono dati da ricevere?
                            int recv_amout = clientSock.Available;

                            if (recv_amout > 0)
                            {
                                QueuePacket pkt = new QueuePacket();
                                pkt.SequenceNumber = seq_num++;
                                pkt.PayloadData = new byte[recv_amout];

                                int recv = clientSock.Receive(pkt.PayloadData);

                                //la coda è thread safe!
                                thiz.mOutputQueue.Enqueue(pkt);
                            }
                            else
                            {
                                //socket chiusa,torno in accept
                                back_accept = true;
                                break;
                            }

                        }
                        else
                        {
                            //niente da ricevere entro il timeout
                            //trasmetto finche la coda e' piena

                            do
                            {
                                if (thiz.mTranceiverShouldQuit)
                                {
                                    quit_loop = true;
                                    break;
                                }

                                QueuePacket tx_pkt = null;

                                if (thiz.mInputQueue.TryDequeue(out tx_pkt))
                                {
                                    if (tx_pkt != null && tx_pkt.PayloadData != null && tx_pkt.PayloadData.Length > 0)
                                    {
                                        clientSock.Send(tx_pkt.PayloadData);
                                        tx_pkt.Dispose();
                                        tx_pkt = null;
                                    }
                                }
                            }
                            while (!thiz.mInputQueue.IsEmpty);

                            if (quit_loop) break;
                        }
                    }

                    //c'è stata una disconnessione oppure una richiesta di chiusura dal controller
                    
                    //controllo uscita del controller
                    if (quit_loop)
                    {
                        thiz.mChannelStatus = ChannelStatusEnum.Idle;
                        listSock.Close();

                        try
                        {
                            clientSock.Shutdown(SocketShutdown.Both);
                            clientSock.Close();
                        }
                        catch
                        {
                            //non importa niente qui
                        }
                        
                        //se e' una disconnessione devo chiudere
                        return;

                    }

                    //chiusura connessione del canale
                    if ( back_accept )
                    {
                        thiz.mChannelStatus = ChannelStatusEnum.Listening;

                        try
                        {
                            clientSock.Shutdown(SocketShutdown.Both);
                            clientSock.Close();
                        }
                        catch
                        {
                            //non importa niente qui
                        }

                        if (thiz.mEventReceiver != null)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                thiz.mEventReceiver.OnRemoteDisconnetion(thiz);
                            }));
                        }
                    }
                    
                }
                catch
                {
                    //gestione errore
                    return;
                }
                
            }

        }

        public void Dispose()
        {
            StopEndPoint();
            
        }
    }
}
