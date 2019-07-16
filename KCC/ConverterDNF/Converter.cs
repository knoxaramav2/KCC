using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CodeTranslator;
using CodeTranslator.Targets;
using CommonLangLib;
using KCC;
using TargetPluginSDK;

namespace Converterx
{
    /// <summary>
    /// Creates assembly files from data
    /// </summary>
    public class Converter
    {
        private List<string> _asm;
        private CliOptions _cli;
        private LocaleArchFrags _fragments;
        private InstDeclController _controller;
        private ITarget _target;

        public Converter()
        {
            _asm = new List<string>();
            _cli = CliOptions.GetInstance();
            _controller = InstDeclController.GetInstance();
        }

        public void LogInternalTranslation()
        {
            if (!_cli.OutputInternals)
            {
                return;
            }

            string directives = _controller.GetDirectiveLog();
            string log = directives + _controller.DumpInternalCode(System.Console.WindowWidth);

            if (_cli.VerboseLevel == Verbosity.Detailed)
            {
                Debug.PrintDbg(log);
            }

            string logPath = $@"{KCCEnv.BaseUri}/{_cli.OutputName}_log.txt";
            using (var file = new StreamWriter(logPath, false))
            {
                file.Write(log);
            }
        }

        public void Build()
        {
            //TODO Support other architectures than x86-x64
            LoadTargetPlugin();
            CreateAssembly();
        }

        public void CreateAssembly()
        {

            //TODO Choose from list

            IArchAgent agent = new Gasx86_64();
            agent.Init(_controller);

            //TODO support multiple files
            while (true)
            {
                _asm.Add(agent.GetAll());

                string asmPath = $@"{KCCEnv.BaseUri}/{_cli.OutputName}.s";

                using (var file = new StreamWriter(asmPath, false))
                {
                    foreach (var line in _asm)
                    {
                        file.Write(line);
                    }
                }

                agent.InvokeLocalAssembler(asmPath);

                break;
            }
        }

        public void LoadTargetPlugin()
        {
            //var target = _cli.TargetOption;

            //var plugins = Directory.GetFiles("libs", "*.dll");
            string plugin = _cli.TargetOption != null ? _cli.TargetOption : GetDefaultPlugin();

            string cpath = Environment.CurrentDirectory;
            string pluginPath = cpath
                .Substring(0, cpath.LastIndexOf("bin")) + $@"libs";

            if (!Directory.Exists(pluginPath))
            {
                throw new TargetPluginNotFoundException($"Unable to find plugin directory: {pluginPath}");
            }

            var fileNames = Directory.GetFiles(pluginPath, "*.dll");
            var assemblies = new List<Assembly>(fileNames.Length);

            foreach (string pluginFile in fileNames)
            {
                var asmName = AssemblyName.GetAssemblyName(pluginFile);
                var asm = Assembly.Load(asmName);
                assemblies.Add(asm);
            }

            Type pluginType = typeof(ITarget);
            var pluginTypes = new List<Type>();
            foreach(var assembly in assemblies)
            {
                if (assembly == null) continue;
                var types = assembly.GetTypes();

                foreach(var type in types)
                {
                    if (!type.IsInterface && !type.IsAbstract && type.GetInterface(pluginType.FullName)!=null)
                    {
                        pluginTypes.Add(type);
                    }
                }
            }

            var plugins = new List<ITarget>(pluginTypes.Count);
            foreach(Type t in pluginTypes)
            {
                ITarget p = (ITarget)Activator.CreateInstance(t);
                p.Init(_cli, _controller);
                plugins.Add(p);
            }

            Console.WriteLine(plugins.Count);


            /*
            AssemblyName asmName = AssemblyName.GetAssemblyName(pluginPath);
            Assembly pluginAsm = Assembly.Load(asmName);
            Type type = typeof(ITarget);
            Type[] types = pluginAsm.GetTypes();

            _target = (ITarget)Activator.CreateInstance(pluginAsm.GetType());
            _target.Init(_cli, _controller);
            */
        }

        private string GetDefaultPlugin()
        {
            switch (CliOptions.Arch.Arch)
            {
                case ProcessorArchitecture.Amd64:
                    return "GasX86_64";
            }

            return "";
        }
    }

    class TargetPluginNotFoundException : Exception
    {
        public TargetPluginNotFoundException(string msg) : base(msg)
        {

        }
    }
}