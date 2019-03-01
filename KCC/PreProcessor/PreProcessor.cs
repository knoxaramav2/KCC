using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CommonLangLib;

namespace PreProcessor
{
    public class PreProcessor : IFileProcessor<PageInfo, PageInfo>
    {
        private DependencyManager _dependencyManager;
        private ErrorReporter _reporter;
        private PageDistro _distro;

        private Queue<string> queue;

        public List<string> preCompiledStrings { get; }

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
            var macroTable = new Hashtable();

            Debug.PrintDbg("PreCompile " + input.Uri);

            var fSlash = input.RelativeUri.LastIndexOf('/') + 1;
            var bSlash = input.RelativeUri.LastIndexOf('\\') + 1;

            var baseFile = input.RelativeUri.Substring(fSlash>bSlash?fSlash:bSlash);
            var i = 0;

            var processedList = new List<string>();

            //TODO Update to more c# friendly code
            foreach (var line in input.RawCode)
            {
                Debug.PrintDbg(i.ToString().PadLeft(3, '0') + " | " + baseFile + ": " +line);
                var modLine = "";
                var directive = "";

                bool doubleQuote = false, singleQuote = false;
                var multiLineComment = false;
                var directiveFlag = false;

                for(var x = 0; x<line.Length; ++x)
                {
                    var c = line[x];
                    switch (c)
                    {
                        case '\\':
                            modLine += '\\';
                            if (x + 1 < line.Length)
                            {
                                modLine += line[++x];
                            }
                            break;
                        case '\'':
                            if (doubleQuote || multiLineComment) break;
                            singleQuote = !singleQuote;
                            break;
                        case '\"':
                            if (multiLineComment) break;
                            doubleQuote = !doubleQuote;
                            break;
                        case '@':
                            if (singleQuote || doubleQuote || multiLineComment) break;
                            directiveFlag = true;
                            break;
                        case '#':
                            if (x+1<line.Length && line[x+1] == '*') { multiLineComment=true;}
                            else { x = line.Length;}
                            break;
                        case '*':
                            if (x + 1 < line.Length && line[x + 1] == '#') { multiLineComment=false;}
                            break;
                        default:
                            if (multiLineComment) break;

                            if (directiveFlag)
                            {
                                directive += c;
                            }
                            else
                            {
                                modLine += c;
                            }
                            break;
                    }
                }

                if (directiveFlag)
                {
                    var options = directive.Split(new []{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                    if (options.Length != 0)
                    {
                        var comm = options[0];
                        
                        if (comm == "use")
                        {
                            for (var lib = 1; lib < options.Length; ++lib)
                            {
                                queue.Enqueue(comm);
                            }
                        } else if (comm == "define")
                        {
                            var size = options.Length;
                            if (size > 1)
                            {
                                var macro = options[1];
                                var def = "";
                                for (var term = 2; term < options.Length; ++term)
                                {
                                    def += term + ' ';
                                }

                                def = def.Trim();

                                macroTable.Add(macro, def);
                            }
                        }
                        else
                        {
                            _reporter.Add(comm, ErrorCode.MacroUndefined);
                        }
                    }
                }

                if (modLine.Length > 0)
                {
                    processedList.Add(modLine);
                    Debug.PrintDbg(" => " + line);
                }

                ++i;
            }

            input.RawCode = input.RawCode.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return input;
        }
    }
}