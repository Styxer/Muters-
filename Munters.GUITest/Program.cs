using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Munters.Core.Caching;
using Munters.Giphy.Interfaces;
using Munters.Giphy.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Munters.GUITest
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var services = new ServiceCollection();
            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }

          
        }

        private static void ConfigureServices(ServiceCollection services)
        {
          //  services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddScoped<IGiphyManager>(_ => new GiphyManager(Properties.Settings.Default.ApiKey, new HttpManager(), new MemoryCacheManager()));
            services.AddScoped<Form1>();
        }


    }
}
