using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLangLib
{
    public class PageInfo
    {
        public string [] RawCode { get; private set; }
        private string _uri;

        public PageInfo(string filePath)
        {
            _uri = filePath;
            RawCode = File.ReadAllLines(_uri);
        }

        public string GetUri()
        {
            return _uri;
        }

        public bool ReplaceLine(int idx, string line)
        {
            if (idx >= RawCode.Length)
            {
                return false;
            }

            RawCode[idx] = line;

            return true;
        }
    }

    public class PageDistro
    {
        private static PageDistro _self;
        private ArrayList _pages;

        private int _index;

        private PageDistro()
        {
            _pages = new ArrayList();
            _index = 0;
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
                    e.ToString(), ErrorCode.FileNotFound);
            }
        }

        public PageInfo GetNextPage()
        {
            if (_index >= _pages.Count)
            {
                return null;
            }

            return (PageInfo) _pages[_index++];
        }
    }
}
