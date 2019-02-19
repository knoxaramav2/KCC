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
            const int stateNormal = 0;
            const int stateLineComment = 1;
            const int stateMultiLineComment = 2;
            const int statepreProcDirective = 3;

            var macroTable = new HashSet<string>();

            Debug.PrintDbg("PreCompile " + input.Uri);

            var fSlash = input.RelativeUri.LastIndexOf('/') + 1;
            var bSlash = input.RelativeUri.LastIndexOf('\\') + 1;

            var baseFile = input.RelativeUri.Substring(fSlash>bSlash?fSlash:bSlash);
            var i = 0;
            var mode = 0;

            //TODO Update to more c# friendly code
            foreach (var line in input.RawCode)
            {
                Debug.PrintDbg(i.ToString().PadLeft(3, '0') + " | " + baseFile + ": " +line);

                //check for special characters, state
                for (var x = 0; x < line.Length; ++x)
                {
                    var nonSpecialStart = false;

                    switch (line[x])
                    {
                        case '#':

                            break;
                        case '@':
                            if (nonSpecialStart) continue;

                            break;
                        case '\\':
                            ++x;
                            break;
                        default:
                            nonSpecialStart = true;
                            continue;
                    }
                }

                ++i;
            }

            return null;
        }
    }
}