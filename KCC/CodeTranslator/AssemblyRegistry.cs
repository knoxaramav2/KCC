using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    /*
     * Track declared assemblies
     */
    class AssemblyRegistry
    {
        private static AssemblyRegistry _this;
        private readonly List<Assembly> _assemblies;

        private AssemblyRegistry()
        {
            _assemblies = new List<Assembly>();
        }

        public Assembly TargetAssembly { get; internal set; }
        public static void Init()
        {
            if (_this != null) return;

            _this = new AssemblyRegistry {TargetAssembly = null};
        }

        public static void CreateAssembly(string name)
        {
            var asm = new Assembly(name);
            _this._assemblies.Add(asm);
            Debug.WriteLine("Created assembly " + name);
            RetargetAssembly(asm);
        }

        public static int RetargetAssembly(string name)
        {
            foreach (var asm in _this._assemblies)
            {
                if (asm.ScopeSymbol != name) continue;
                return RetargetAssembly(asm);
            }

            return -1;
        }

        public static int RetargetAssembly(Assembly asm)
        {
            Debug.WriteLine("Target assembly set to " + asm.ScopeSymbol);
            _this.TargetAssembly = asm;
            return 0;
        }

        public static AssemblyRegistry GetInstance()
        {
            if (_this==null) { Init();}

            return _this;
        }
    }

    class Assembly : Scope
    {
        public Assembly(string scopeSymbol) : base(scopeSymbol)
        {
        }
    }
}
