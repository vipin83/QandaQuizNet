using QandaQuizNet.Utilities;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace QandaQuizNet
{
    public class Global : HttpApplication
    {

        private static DateTime housekeepingLastPerformed = DateTime.Now;

        private static System.Timers.Timer timer;
        private static Object KeepAliveLocker = new object();

        // Override and Indicate Time in Minutes to Force the Keep-Alive
        protected virtual int KeepAliveMinutes
        {
            get { return 2; } //call houskeeping in every 'x' minutes
        }


        private void KeepAlive(Object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;
            if (housekeepingLastPerformed < DateTime.Now)
            {
                lock (KeepAliveLocker)
                {
                    try
                    {
                        //housekeep();
                    }
                    catch (Exception e1)
                    {
                        int start = e1.Message.IndexOf("->") > 0 ? e1.Message.IndexOf("->") : e1.Message.Length;
                        int length = (e1.Message.Length - start) > 0 ? (e1.Message.Length - start) : 0;

                    }
                }
            }

            timer.Enabled = true;                        
        }

        private void housekeep()
        {
            housekeepingLastPerformed = DateTime.Now;
            Housekeeping.runMundaneTasks();           
        }
        
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            if (this.KeepAliveMinutes > 0)
            {
                timer = new System.Timers.Timer(60000 * this.KeepAliveMinutes);
                timer.Elapsed += KeepAlive;
                timer.Start();
            }


        }
    }
}