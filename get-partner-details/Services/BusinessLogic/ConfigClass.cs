using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace get_partner_details.Services.BusinessLogic
{
    //static means no object creation required - AppConfig.Config (class.Property)
    public static class AppConfig
    {
        //IConfiguration represents app config settings
        public static IConfiguration Config { get; set; }

        public static string ExcelFileName => Config["ExcelFileName"];
        public static string PrefixColumn => Config["PrefixColumn"];
        public static string InputColumnName => Config["InputColumnName"];
    }
}
