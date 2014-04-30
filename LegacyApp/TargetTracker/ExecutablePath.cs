using System.IO;

namespace TargetTracker
{
    public static class ExecutablePath
    {
        private static string execPath;
        /// <summary>
        /// путь к исполняемому файлу без завершающего слэш
        /// </summary>
        public static string ExecPath
        {
            get
            {
                var sm = System.Reflection.Assembly.GetEntryAssembly();
                if (string.IsNullOrEmpty(execPath))
                    execPath = Path.GetDirectoryName(sm.Location);
                //System.Reflection.Assembly.GetEntryAssembly().Location);
                return execPath;
            }
        }
    }

}
