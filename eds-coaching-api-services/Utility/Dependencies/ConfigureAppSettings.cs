using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Dependencies
{
    public static class ConfigureAppSettings
    {
        public static IConfiguration Configure = ConfigureAppSetting();

        private static IConfiguration ConfigureAppSetting()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

    }
}
