using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLangLib
{
    public class PageInfo
    {
        public string [] RawCode { get; }
        public string Uri { get; }

        public PageInfo(string filePath)
        {
            Uri = filePath;
            RawCode = File.ReadAllLines(Uri);
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
        private List<PageInfo> _pages;

        private int _index;

        private PageDistro()
        {
            _pages = new List<PageInfo>();
            _index = 0;
        }

        public static PageDistro GetInstance()
        {
            return _self ?? (_self = new PageDistro());
        }

        public PageInfo LoadFile(string path)
        {
            if (path.Length == 0)
            {
                return null;
            }

            PageInfo page = null;

            path = NormalizeUri(path);

            try
            {
                page = new PageInfo(path);
                _pages.Add(page);
            }
            catch (FileNotFoundException e)
            {
                ErrorReporter.GetInstance().Add(
                    e.ToString(), ErrorCode.FileNotFound);
            }

            return page;
        }

        public PageInfo GetNextPage()
        {
            if (_index >= _pages.Count)
            {
                return null;
            }

            return (PageInfo) _pages[_index++];
        }

        public bool IsFileLoaded(string uri)
        {
            uri = NormalizeUri(uri);

            foreach (var page in _pages)
            {
                if (uri == page.Uri)
                {
                    return true;
                }
            }

            return false;
        }


        private string NormalizeUri(string uri)
        {
            if (uri == null) return null;

            if (uri.Length > 1 && uri[1] != ':')
            {
                uri = KCCEnv.SrcUri = '\\' + uri;
            }

            return uri;
        }
    }
}
