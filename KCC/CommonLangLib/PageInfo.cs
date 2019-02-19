using System.Collections.Generic;
using System.IO;

namespace CommonLangLib
{
    public class PageInfo
    {
        public string [] RawCode { get; set; }
        public string Uri { get; }
        public string RelativeUri { get; }
        public List<string> DependencyList;

        public PageInfo(string filePath)
        {
            filePath.Replace('\\', '/');
            RelativeUri = filePath;
            Uri = PageDistro.NormalizeUri(filePath) ;
            RawCode = File.ReadAllLines(Uri);
            DependencyList = new List<string>();
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
        
        public void AddDependency(string uri)
        {
            if (string.IsNullOrEmpty(uri)) return;

            if (!DependencyList.Contains(uri))
            {
                DependencyList.Add(uri);
            }

            ErrorReporter.GetInstance().Add("Import redefinition of " + uri + " in " + Uri, ErrorCode.ImportRedefine);
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
            return _index >= _pages.Count ? null : _pages[_index++];
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


        public static string NormalizeUri(string uri)
        {
            if (uri == null) return null;

            if (uri.Length > 1 && uri[1] != ':')
            {
                uri = KCCEnv.BaseUri + '/' + uri;
            }

            return uri;
        }
    }
}
