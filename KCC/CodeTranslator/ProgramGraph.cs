using System;
using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    /// <summary>
    /// Compliments the database
    /// Used for recursive symbol resolution
    /// </summary>
    public class ProgramGraph
    {
        private ulong _refCounter;
        private ScopeNode _current;
        private ScopeNode _root;
        private InstructionBase _instructionBase;

        public ProgramGraph()
        {
            _refCounter = 1;
            _current = null;
            _root = new ScopeNode("#ROOT#", ulong.MaxValue);
            _current = _root;
            ProgramInit = false;
            _instructionBase = new InstructionBase();
        }

        //Scopes
        private ulong AddFieldScope(string name, string meta, bool executable)
        {
            if (_current.Fields == null)
            {
                _current.Fields = new ScopeNode(name, _refCounter++) { Host = _current };
                _current = _current.Fields;
                Debug.PrintDbg("   => " + GetScopeString() + " exec? " + executable);
                return _refCounter;
            }

            _current = _current.Fields;

            while (_current.Next != null)
            {
                _current = _current.Next;
            }

            _current.Next = new ScopeNode(name, _refCounter++, meta, executable)
            {
                Previous = _current,
                Host = _current.Host
            };
            _current = _current.Next;

            Debug.PrintDbg("   => " + GetScopeString() + " exec? " + executable);

            return _refCounter;
        }

        public ulong AddAssembly(string name)
        {
            ProgramInit = true;

            //reset head to assembly level
            _current = _root;

            //create first assembly if needed
            if (_current.Fields == null)
            {
                _current.Fields = new ScopeNode(name, _refCounter++);
                _current = _current.Fields;
                _current.Host = _root;
                Debug.PrintDbg($"> Asm + {GetScopeString()}");
                return _refCounter;
            }

            while (_current.Next != null)
            {
                _current = _current.Next;
            }

            _current.Next = new ScopeNode(name, _refCounter++);
            var tmp = _current;
            _current = _current.Next;
            _current.Previous = tmp;
            _current.Host = _root;

            Debug.PrintDbg($"> Asm + {GetScopeString()}");

            return _refCounter;
        }

        public ulong AddClass(string name)
        {
            var refId = AddFieldScope(name, null, false);
            Debug.PrintDbg($"> Class + {GetScopeString()}");
            return refId;
        }

        /// <summary>
        /// Creates a function scope field
        /// Overload string = (type+args+meta).GetHashCode().ToString()
        /// </summary>
        /// <param name="name">Name of function</param>
        /// <param name="meta">Overload identifier string</param>
        /// <returns></returns>
        public ulong AddFunction(string name, string meta)
        {
            var refId = AddFieldScope(name, meta, true);
            Debug.PrintDbg($"> Function + {GetScopeString()}");
            _instructionBase.AddFunction(refId);

            return refId;
        }

        //Body fields
        public ulong AddVariable(string name, string type)
        {
            _current.AddBodyField(new BodyField(name, _refCounter++, type));

            return _refCounter;
        }

        public void AddInstruction(string op, string arg0 = null, string arg1 = null)
        {
            if (!_instructionBase.AddInstruction(op, arg0, arg1))
            {
                //TODO Not in function
                return;
            }

            Debug.PrintDbg($"    > {_instructionBase.GetCurrentFunction().RefId} | {op} {arg0} {arg1}");
        }

        /// <summary>
        /// Stop tracking current scope and drop to previous
        /// </summary>
        /// <returns>
        /// True if there exists a scope below
        /// False if final scope is exited
        /// </returns>
        public bool LeaveScope()
        {
            if (_current?.Host == null)
            {
                return false;
            }

            _instructionBase.Untrack();
            _current = _current.Host;

            return true;
        }

        //Util
        public bool ProgramInit { get; private set; }

        public string GetScopeString()
        {
            var ret = _current.Name;

            var n = _current;
            while (n.Host != null)
            {
                ret = n.Host.Name + "." + ret;
                n = n.Host;
            }

            return ret;
        }

        public ulong GetCurrentReferenceId()
        {
            return _current.RefId;
        }
    }

    internal class InstructionBase
    {
        private List<InstructionUnit> _units;
        private InstructionUnit _current;

        public InstructionBase()
        {
            _units = new List<InstructionUnit>();
            _current = null;
        }

        public bool SelectFunction(ulong refId)
        {
            Untrack();

            foreach (var unit in _units)
            {
                if (unit.RefId != refId) continue;
                _current = unit;
                return true;
            }

            return false;
        }

        public void Untrack()
        {
            _current = null;
        }

        public void AddFunction(ulong refId)
        {
            Untrack();
            var unit = new InstructionUnit(refId);
            _units.Add(unit);
            _current = unit;
        }

        public bool AddInstruction(string ops, string arg0, string arg1)
        {
            if (_current == null)
            {
                return false;
            }

            _current.AddInstruction(ops, arg0, arg1);

            return true;
        }

        public InstructionUnit GetCurrentFunction()
        {
            return _current;
        }
    }

    internal class InstructionUnit
    {
        public ulong RefId;
        private List<string[]> _terms;
        private int _termIndex;

        public InstructionUnit(ulong refId)
        {
            RefId = refId;
            _terms = new List<string[]>();
            _termIndex = 0;
        }

        public void AddInstruction(string ops, string arg0, string arg1)
        {
            _terms.Add(new []{ops, arg0, arg1});
        }

        public string[] GetNextInstruction()
        {
            if (_termIndex >= _terms.Count)
            {
                return null;
            }

            return _terms[_termIndex++];
        }

        public void Rewind()
        {
            _termIndex = 0;
        }
    }

    internal class EltNode
    {
        public ulong RefId; //0 if unresolved
        public string Name;

        public EltNode(string name, ulong refId)
        {

        }
    }

    internal class ScopeNode : EltNode
    {
        public ScopeNode Fields, Host, Previous, Next;
        public bool Executable;
        public string Meta;//Function overloading info

        public ScopeNode(string name, ulong refId, string meta = null, bool executable=false) : base (name, refId)
        {
            _fields = new List<BodyField>();
            Executable = executable;
            Meta = meta;

            RefId = refId;
            Name = name;
        }

        public void AddBodyField(BodyField e)
        {
            _fields.Add(e);
        }

        private List<BodyField> _fields;
    }

    internal class BodyField : EltNode
    {
        public string Type;

        public BodyField(string name, ulong refId, string type) : base(name, refId)
        {
            Type = type;
        }
    }
}