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

        private Queue<string> queue;

        public PreProcessor()
        {
            _dependencyManager = new DependencyManager();
            _reporter = ErrorReporter.GetInstance();
            _distro = PageDistro.GetInstance();
            queue = new Queue<string>();
        }

        private void updateProcessedFile()
        {

        }

        public bool PreCompileProject(string masterUri)
        {
            Debug.PrintDbg("PreCompile master file " + masterUri);

            queue.Enqueue(masterUri);

            while (queue.Count > 0)
            {
                var uri = queue.Dequeue();
                Execute(_distro.LoadFile(uri));
            }

            return true;
        }

        public PageInfo Execute(PageInfo input)
        {
            var macroTable = new HashSet<string>();

            Debug.PrintDbg("PreCompile " + input.Uri);

            foreach (var line in input.RawCode)
            {
                Console.WriteLine(line);
            }

            return null;
        }
    }
}