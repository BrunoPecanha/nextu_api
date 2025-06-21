using Microsoft.Extensions.Configuration;
using System;

namespace UFF.Service.Helpers
{
    public class ConfigHelper
    {
        private static IConfigurationRoot configuracaoGeral;
        public class Config
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public bool EnableSsl { get; set; }
            public string FromAddress { get; set; }
            public string Password { get; set; }
            public string AddressTo { get; set; }
        }

        private static IConfigurationRoot GetConfiguracao()
        {
            if (configuracaoGeral == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
                configuracaoGeral = builder.Build();
            }
            return configuracaoGeral;
        }

        public static Config GetEmailSendConfiguration()
        {
            var config = new Config()
            {
                Host = GetConfiguracao()["Smtp:Server"],
                Port = Convert.ToInt32(GetConfiguracao()["Smtp:Port"]),
                EnableSsl = Convert.ToBoolean(GetConfiguracao()["Smtp:EnableSSL"]),
                FromAddress = GetConfiguracao()["Smtp:FromAddress"],
                Password = GetConfiguracao()["Smtp:Password"],
                AddressTo = GetConfiguracao()["Smtp:AddressTo"]
            };
            return config;
        }
    }
}

