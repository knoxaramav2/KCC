using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    class SyntaxErrors
    {
        private static SyntaxErrors _this;

        public static SyntaxErrors GetInstance()
        {
            return _this ?? (_this = new SyntaxErrors());
        }

        private readonly Hashtable _errors = new Hashtable();
        private int _errs = 0;

        public void Add(Error err)
        {
            _errors.Add(_errs++, err.ToString());
        }

        public int Size()
        {
            return _errs;
        }

        public string GetErrors(int index)
        {
            if (index < 0 || index >= _errs) return "";
            return (string) _errors[index];
        }
    }

    class Error
    {
        private readonly String _msg;
        private readonly int _line;

        public Error(String msg, int lineNumber)
        {
            this._msg = msg;
            this._line = lineNumber;
        }

        public override String ToString()
        {
            return _line + " : " + _msg;
        } 
    }

    class Instruction
    {

    }

    class FunctionPrototype
    {
        public Group ArgGroup;
        public string Id;
        public string Type;
    }

    class BlockExec
    {
        private List<Instruction> instructions;
    }

    class FunctionDeclaration
    {
        public FunctionPrototype Prototype;
        public BlockExec BlockExec;
    }

    class Assembly
    {
        public string Id;
        public BlockStruct BlockStruct;
    }

    class BlockStruct
    {
        public List<VarDecl> Members;
    }

    class ExecBlock
    {
        
    }

    class VarDecl
    {
        public string Type;
        public string Symbol;
        public Assignment Assignment;
    }

    class Assignment
    {

    }

    class Group
    {
        public List<Instruction> Instructionsl;
    }

    internal class AssemblyVisitor : KCCBaseVisitor<Assembly>
    {
        private List<Assembly> _assemblies = new List<Assembly>();

        public override Assembly VisitAssembly(KCCParser.AssemblyContext context)
        {
            var asm = new Assembly {Id = context.symbol_id().GetText()};

            Console.WriteLine("Found asm " + asm.Id);

            if (context.block_struct() == null)
            {
                SyntaxErrors.GetInstance().Add(new Error("Invalid or missing assembly body : " + context.GetText(), -1));
                return null;
            }

            var bsListener = new BlockStructVisitor();
            bsListener.VisitBlock_struct(context.block_struct());
            asm.BlockStruct = bsListener.GetBlockStructs()[0];

            _assemblies.Add(asm);
            return base.VisitAssembly(context);
        }

        public override Assembly VisitReturn(KCCParser.ReturnContext context)
        {
            return base.VisitReturn(context);
        }

        public List<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }

    internal class BlockStructVisitor : KCCBaseVisitor<BlockStruct>
    {
        private readonly List<BlockStruct> _blockStructs = new List<BlockStruct>();

        public override BlockStruct VisitBlock_struct(KCCParser.Block_structContext context)
        {
            var blockStruct = new BlockStruct();

            Console.WriteLine("Found blockstruct");

            var instBody = context.inst_body();
            foreach(var inst in instBody)
            {
                if (inst.var_decl() != null)
                {

                } else if (inst.fnc_proto() != null)
                {

                }
                else if(inst.fnc_decl() != null)
                {

                } else if (inst.@class() != null)
                {

                }
                else
                {
                    //ERROR
                }
            }

            return base.VisitBlock_struct(context);
        }

        public List<BlockStruct> GetBlockStructs()
        {
            return _blockStructs;
        }
    }

    internal class ExecBlockVisitor : KCCBaseVisitor<ExecBlock>
    {
        public override ExecBlock VisitBlock_exec(KCCParser.Block_execContext context)
        {
            Console.WriteLine("Found exec struct");

            return base.VisitBlock_exec(context);
        }
    }

    internal class FunctionPrototypeVisitor : KCCBaseVisitor<KCCParser.Fnc_protoContext>
    {
        private readonly List<FunctionPrototype> _prototypes = new List<FunctionPrototype>();

        public override KCCParser.Fnc_protoContext VisitFnc_proto(KCCParser.Fnc_protoContext context)
        {
            Console.WriteLine("Found Fnc Proto");

            var prototype = new FunctionPrototype();

            prototype.Id = context.symbol_id().GetText();
            prototype.Type = context.restric_id().GetText();

            _prototypes.Add(prototype);

            return base.VisitFnc_proto(context);
        }
    }

    internal class FunctionDeclareVisitor : KCCBaseVisitor<KCCParser.Fnc_declContext>
    {
        public override KCCParser.Fnc_declContext VisitFnc_decl(KCCParser.Fnc_declContext context)
        {
            Console.WriteLine("Found Fnc Proto");

            return base.VisitFnc_decl(context);
        }
    }
}
