using Microsoft.Extensions.DependencyInjection;
using System.Text;
using translator.entities;
using translator.services;

namespace translator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //创建服务容器
            var services = new ServiceCollection();
            services.AddSingleton<FrmMain>();
            string configPath = Path.Combine(AppContext.BaseDirectory,"app.json");
            Config c = null;
            if (File.Exists(configPath)){
               string data= File.ReadAllText(configPath, Encoding.UTF8);
                c = System.Text.Json.JsonSerializer.Deserialize<Config>(data);
            }else
            {
                c= new Config();    
            }
            services.AddSingleton<Helper>();
            services.AddSingleton<Config>(c);
            services.AddSingleton<BaiduTranslator>();
            services.AddSingleton<GoogleTranslator>();
            var provider = services.BuildServiceProvider();
     
            Application.Run(provider.GetService<FrmMain>());
        }
    }
}