using System;
using System.Collections.Generic;
using CommonLangLib;

namespace PreProcessor
{
    public class PreProcessor : IFileProcessor<PageInfo, PageInfo>
    {
        private DependencyManager _dependencyManager;
        private ErrorReporter _reporter;
        private PageDistro _distro;

        public PreProcessor()
        {
            _dependencyManager = new DependencyManager();
            _reporter = ErrorReporter.GetInstance();
            _distro = PageDistro.GetInstance();
        }

        private void updateProcessedFile()
        {

        }

        public bool PreCompileProject(string uri)
        {
            var page =_distro.LoadFile(uri);
            var queue = new List<string> {page.Uri};

            while (queue.Count > 0)
            {
                
            }

            return true;
        }

        public PageInfo Execute(PageInfo input)
        {
            var macroTable = new HashSet<string>();


            foreach (var line in input.RawCode)
            {
                
            }

            return null;
        }
    }
}