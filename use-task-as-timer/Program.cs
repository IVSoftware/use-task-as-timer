using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace use_task_as_timer
{
    class Program
    {
        static Startup _app;
        static void Main(string[] args)
        {
            _app = new Startup();
            Console.ReadKey(); // Block here for demo
        }
    }
    public partial class Startup
    {   

        void RestartDelay()
        {
            Task.Delay(1000).GetAwaiter().OnCompleted(() => { PerformUpdate(); });
        }

        private void PerformUpdate()
        {
            Debug.WriteLine(_stopwatch.Elapsed.ToString() + " - Update Controls");
            RestartDelay();
        }
        public Startup()
        {
            Configuration(new AppBuilder());
        }
        
        Stopwatch _stopwatch = new Stopwatch();    
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            _stopwatch.Start();
            RestartDelay();
        }

        private void ConfigureAuth(IAppBuilder app) { } // Pretend
        public interface IAppBuilder { } // Pretend
        public class AppBuilder : IAppBuilder { } // Pretend
    }
}
