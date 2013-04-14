using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    public class ConfigurationUtil {
        public static string Get(string key) {
            return ConfigurationManager.AppSettings[key];
        }

        public static void Set(string key, string value) {
            ConfigurationManager.AppSettings.Set(key, value);
        }
    }
}