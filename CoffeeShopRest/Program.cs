using System;
using log4net;
using Nancy.Hosting.Self;
using Topshelf;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CoffeeShopRest
{
    public class NancySelfHost 
    { 
        private NancyHost _nancyHost;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

         public void Start() 
         {

             Log.Debug("NancySelfHost - Starting...");
             _nancyHost = new NancyHost(new Uri("http://localhost:8888/coffeeshop/")); 
             _nancyHost.Start();
             Console.WriteLine("Nancy now listening - http://localhost:8888/coffeeshop/. Press ctrl-c to stop"); 
         } 
 

         public void Stop() 
         { 
             _nancyHost.Stop(); 
             Console.WriteLine("Stopped. Good bye!"); 
         } 
     } 

    
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                     
             { 
                 x.Service<NancySelfHost>(s =>                        
                 { 
                     s.ConstructUsing(name => new NancySelfHost());  
                     s.WhenStarted(tc => tc.Start());                
                     s.WhenStopped(tc => tc.Stop());                 
                 });

                 x.UseLog4Net(); 
                 x.RunAsLocalSystem();                               
                 x.SetDescription("CoffeeHouse Topshelf Host");      
                 x.SetDisplayName("CoffeeHouse");                     
                 x.SetServiceName("CoffeeHouse");
                 Log.Debug("Starting Main");
             });                                                     

            
        }
    }
}
