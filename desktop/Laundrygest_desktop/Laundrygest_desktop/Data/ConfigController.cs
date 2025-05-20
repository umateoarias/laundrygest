using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Laundrygest_desktop.Model;
using Microsoft.Extensions.Configuration;

namespace Laundrygest_desktop.Data
{
    public static class ConfigController
    {
        private static string ConfigFileName = "appsettings.json";
        private static JsonSerializerOptions JsonOptions = new JsonSerializerOptions { WriteIndented = true };
        

        private static readonly string ConfigFilePath =
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,"..","..","..", ConfigFileName));

        public static Settings GetSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(ConfigFilePath, optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var settings = new Settings();
            settings.Company = new Company();
            settings.Clerks = new List<string>();
            configuration.GetSection("Settings").Bind(settings);
            return settings;
        }

        public static void SaveSettings(Settings s)
        {
            var configObj = new { Settings = s };
            string json = JsonSerializer.Serialize(configObj, JsonOptions);
            File.WriteAllText(ConfigFilePath, json);
        }

    }
}
