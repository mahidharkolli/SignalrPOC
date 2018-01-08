using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;


namespace SentinelAutoTest
{
    /// <summary>
    /// Proxy class for SignalR API
    /// </summary>
    public partial class SignalRManager : IDisposable
    {
        private static SignalRManager _signalRmanager;
        private static object syncLock = new object();

        /// <summary>
        /// Use HUB API you want to connect...
        /// </summary>
        //readonly string HUB_API_ADDRESS = ConfigurationManager.AppSettings["HUB_API_WEB_ADDRESS"];
      readonly string HUB_API_ADDRESS = "http://sentineltesthubserver.cloudapp.net/";//"http://localhost:59899/";//"http://e5bfb1ccfee945de9ebfd9910782b2c2.cloudapp.net/"; //ConfigurationManager.AppSettings["HUB_API_ADDRESS"];
       // readonly string HUB_API_ADDRESS = "http://sentineltesthubserver.cloudapp.net/"; //ConfigurationManager.AppSettings["HUB_API_ADDRESS"];
        // readonly string HUB_API_ADDRESS = "http://localhost:58847/";
        // readonly string HUB_API_ADDRESS = "http://publishprofiletesting.azurewebsites.net/";
       //readonly string HUB_API_ADDRESS = "http://azuregatewaypoc.cloudapp.net/";
        
        private IHubProxy sentinelHubProxy;
        public HubConnection Connection { get; private set; }
  
       
        public bool _IsWebSocket;
        //MainViewModel mainViewModel;

        public SignalRManager(bool isSystem,string tokenString,bool IsWebSocket)
        {
            _IsWebSocket = IsWebSocket;
            sentinelHubProxy = InitHubAndConnectionObjects();
        }
        
        public async Task Init()
        {
            try
            {
                if (_IsWebSocket)
                    await Connection.Start();
                else
                    await Connection.Start(new Microsoft.AspNet.SignalR.Client.Transports.LongPollingTransport());
                if (Connection.State == ConnectionState.Connected)
                    Console.WriteLine("connection connected...");
                else
                    Console.WriteLine("connection not connected...");
            }
            catch(Exception ex)
            {

            }
        }

        private IHubProxy InitHubAndConnectionObjects()
        {
            var queryString = new Dictionary<string, string>();
          //  queryString.Add("Bearertoken", TokenString);

            Connection = new HubConnection(HUB_API_ADDRESS, queryString);
            int transportConnectionTimeout = 30;//int.Parse(ConfigurationManager.AppSettings["transportConnectTimeout"]);
            Connection.TransportConnectTimeout = new TimeSpan(0, 0, transportConnectionTimeout);
            Connection.ConnectionSlow += Connection_ConnectionSlow;

            Connection.Closed += Connection_Closed;
            

            Connection.Error += Connection_Error;

            Connection.Reconnected += Connection_Reconnected;
            Connection.StateChanged += Connection_StateChanged;

            Connection.Reconnecting += Connection_Reconnecting;
            
              sentinelHubProxy = Connection.CreateHubProxy("ChatHub");
            //sentinelHubProxy = Connection.CreateHubProxy("SentinelCloudHub");
            return sentinelHubProxy;
        }

        private void Connection_StateChanged(StateChange obj)
        {
            Console.WriteLine("connection state changed.."+obj.NewState);
        }

        void Connection_Reconnecting()
        {
            Console.WriteLine("connection state Reconnecting..");
        }

        void Connection_Reconnected()
        {
            Console.WriteLine("connection state Reconnected..");
        }

        void Connection_Error(Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
            //App.Current.Dispatcher.Invoke(() =>
            //{
            //    mainViewModel.ConnectionStatus = string.Format("Error while connecting to Hub");
            //    mainViewModel.ErrorMessage.Insert(0, mainViewModel.ConnectionStatus);
            //    mainViewModel.ConnectionState = Brushes.Red;

            //    mainViewModel.JsonResponse = ex.Message;
            //    mainViewModel.ErrorMessage.Insert(0, string.Format("Exception - {0}", ex.Message));
            //});
        }

        void Connection_Closed()
        {
            Console.WriteLine("connection state closed..");
        }

        void Connection_ConnectionSlow()
        {
            Console.WriteLine("connection is slow..");
        }


       
        public void Dispose()
        {
            if (Connection != null)
                Connection.Stop();
        }
       
    }
}
