using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLangLib
{
    //Provides environment information
    public static class KCCEnv
    {
        public static string BaseUri { get; internal set; }
        public static string ExeUri { get; internal set; }

        public static void Init()
        {
            BaseUri = Directory.GetCurrentDirectory();
            ExeUri = System.Reflection.Assembly.GetEntryAssembly().Location;

            BaseUri = BaseUri.Replace('\\', '/');
            ExeUri = ExeUri.Replace('\\', '/');
        }
    }
}
