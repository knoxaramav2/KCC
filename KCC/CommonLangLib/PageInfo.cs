using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLangLib
{
    public class PageInfo
    {
        private string [] _rawCode;
        private string uri;

        public PageInfo(string filePath)
        {
  
            uri = filePath;

            try
            {
                _rawCode = File.ReadAllLines(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class PageDistro
    {
        private static PageDistro _self;
        private ArrayList _pages;

        private PageDistro()
        {
            _pages = new ArrayList();
        }

        public static PageDistro GetInstance()
        {
            return _self ?? (_self = new PageDistro());
        }

        public void LoadFile(string path)
        {
            if (path.Length == 0)
            {
                return;
            }

            //Relative path
            if (path[1] != ':')
            {
                path = KCCEnv.SrcUri + '\\' + path;
            }

            try
            {
                _pages.Add(new PageInfo(path));
            }
            catch (FileNotFoundException e)
            {
                ErrorReporter.GetInstance().Add(
                    e.ToString(), ErrorCode.FILE_NOT_FOUND);
            }
        }
    }
}
