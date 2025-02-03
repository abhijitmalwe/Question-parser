using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QParser.Admin.Core;
using QParser.Admin.Models;
using Serilog;
using System;
using System.IO;
using System.Windows.Forms;

namespace QParser.Admin
{
    public class Program
    {
        private static AppSettings _appSettings;
        private static RegexSettings _regexSettings;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = LoadConfiguration();

            IServiceCollection services = new ServiceCollection();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

            _appSettings = new AppSettings();
            config.GetSection("AppSettings").Bind(_appSettings);

            _regexSettings = new RegexSettings();
            config.GetSection("RegexSettings").Bind(_regexSettings);

            services.AddSingleton(_appSettings);
            services.AddSingleton(_regexSettings);

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<IWordprocessor, Wordprocessor>();
            services.AddSingleton<IRegexParser, RegexParser>();
            services.AddSingleton<IDbManager, DbManager>();

            services.AddScoped<MainForm>();

            services.AddLogging(configure => configure.AddSerilog());

            var serviceProvider = services.AddLogging().BuildServiceProvider();

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>().AddFile(config.GetSection("Logging"));

            var mainForm = serviceProvider.GetService<MainForm>();

            Application.Run(mainForm);
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}