using System;
using System.Collections.Generic;
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
        private List<Assembly> _assemblies;
        public Assembly TargetAssembly { get; internal set; }
        public static void Init()
        {
            if (_this != null) return;

            _this = new AssemblyRegistry {TargetAssembly = null};
        }
        public static void CreateAssembly(string name)
        {
            var asm = new Assembly(name);
            _this.TargetAssembly = asm;
            _this._assemblies.Add(asm);
        }

    }

    class Assembly : Scope
    {
        public Assembly(string scopeSymbol) : base(scopeSymbol)
        {
        }
    }
}
