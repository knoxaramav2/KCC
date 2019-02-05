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
        public static string SrcUri { get; internal set; }

        public static void Init()
        {
            BaseUri = Directory.GetCurrentDirectory();
            BaseUri = BaseUri.Substring(0 ,BaseUri.LastIndexOf('\\'));
            //BaseUri += '\\';

            SrcUri = BaseUri + "\\src";
        }
    }
}
