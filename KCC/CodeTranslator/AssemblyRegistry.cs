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
        private ClassTable _classTable;
        private MethodTable _methodTable;
        private SymbolTable _symbolTable;
        private DataTypeTable _dataTypeTable;

        private IRecordController<T> _recordController;

        public Assembly(string scopeSymbol) : base(scopeSymbol)
        {
            _classTable = new ClassTable();
            _methodTable = new MethodTable();
            _symbolTable = new SymbolTable();
            _dataTypeTable = new DataTypeTable();

            _recordController = null;
        }

        public RecordOperationResult Add(Record value)
        {
            switch (value.Type)
            {
                case RecordType.Class:
                    break;
                case RecordType.Method:
                    break;
                case RecordType.Variable:
                    break;
                case RecordType.Type:
                    break;
                case RecordType.Instruction:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return RecordOperationResult.Ok;
        }
    }
}
