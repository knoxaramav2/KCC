using System;

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

        public ProgramGraph()
        {
            _refCounter = 1;
            _current = null;
            _root = new ScopeNode("#ROOT#", ulong.MaxValue);
            _current = _root;
        }

        public bool AddAssembly(string name)
        {
            //reset head to assembly level
            _current = _root;

            //create first assembly if needed
            if (_current.Parent == null)
            {
                _current.Parent = new ScopeNode(name, _refCounter++);
                _current = _current.Parent;
                _current.Child = _root;
                return true;
            }

            while (_current.Next != null)
            {
                _current = _current.Next;
            }

            _current.Next = new ScopeNode(name, _refCounter++);
            var tmp = _current;
            _current = _current.Next;
            _current.Previous = tmp;
            _current.Child = _root;

            return true;
        }

        public bool AddClass(string name)
        {

            if (_current.Parent == null)
            {
                _current.Parent = new ScopeNode(name, _refCounter++) {Child = _current};
                _current = _current.Parent;
                return true;
            }

            var child = _current;
            _current = _current.Parent;

            while (_current.Next != null)
            {
                _current = _current.Next;
            }

            _current.Next = new ScopeNode(name, _refCounter++)
            {
                Previous = _current,
                Child = _current.Child
            };
            _current = _current.Next;

            return true;
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
            if (_current == null || _current.Child == null)
            {
                return false;
            }

            _current = _current.Child;

            return true;
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
        public ScopeNode Parent, Child, Previous, Next;

        public ScopeNode(string name, ulong refId) : base (name, refId)
        {

        }
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