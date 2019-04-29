using System;

namespace CodeTranslator.Targets
{
    public class Nasm32 : IArchAgent
    {
        private GlobalSymbolTable _symbolTable;
        private TypeTable _typeTable;
        private ProgramGraph _graph;

        public Nasm32()
        {
        }

        public void Init(ProgramGraph graph)
        {
            _symbolTable = GlobalSymbolTable.GetInstance();
            _typeTable = TypeTable.GetInstance();
            _graph = graph;
        }

        public string GetHeader()
        {
            var header = "";

            header = 
                    ".global" +
                     ".data" +
                     ".align 4";

            return header;
        }

        public string GetGlobals()
        {
            var global = "";


            return global;
        }

        public string GetConstData()
        {
            var cdat = "";


            return cdat;
        }

        public string GetFunctionDefs()
        {
            var fnc = "";

            return fnc;
        }

        public string FormatInstruction(InstOp opcode, int arg0, int arg1, int spcl1, int spcl2, InstOp mode)
        {
            var ret = "";

            switch (opcode)
            {
                    
            }


            return ret;
        }
    }
}