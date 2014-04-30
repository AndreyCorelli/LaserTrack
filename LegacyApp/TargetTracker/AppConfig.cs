using System.Configuration;

namespace TargetTracker
{
    public static class AppConfig
    {
        public static string GetStringParam(string key, string defaultValue)
        {
            var str = ConfigurationManager.AppSettings.Get(key);
            return string.IsNullOrEmpty(str) ? defaultValue : str;
        }

        public static int GetIntParam(string key, int defaultValue)
        {
            var str = ConfigurationManager.AppSettings.Get(key);
            return string.IsNullOrEmpty(str) ? defaultValue : 
                int.Parse(str);
        }

        public static bool GetBooleanParam(string key, bool defaultValue)
        {
            var str = ConfigurationManager.AppSettings.Get(key);
            return string.IsNullOrEmpty(str) ? defaultValue :
                str.ToBool();
        }
    }
}
