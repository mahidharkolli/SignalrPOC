using System;
namespace SentinelAutoTest
{
    class Program
    {
       static string refreshToken = string.Empty;
        static string token = string.Empty;
        static string sessionid = string.Empty;
   
        static bool  WebSockets;
        static  void  Main(string[] args)
        {
            try
            {
                //Change this to use logpolling or Webscockets
                WebSockets = true;

                //Intilize SignalR Proxies
                string UserToken = string.Empty;
                using (SignalRManager Userproxy = new SignalRManager(false, UserToken, WebSockets))
                {
                    
                        Userproxy.Init().Wait();
                         Console.WriteLine("DONE Testing!");
                        Console.ReadLine();
                }
            }
            catch(Exception ex)
            {
               
            }
        }
      

        

    }
}
