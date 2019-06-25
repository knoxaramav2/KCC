using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessor
{
    class DependencyManager
    {
        private DependencyGraph _graph;
        private List<string> _dependencyList;
        private List<string> _queue;

        public DependencyManager()
        {
            _graph = new DependencyGraph();
            _dependencyList = new List<string>();
            _queue = new List<string>();
        }

        public List<string> GetOrderedDependencyList()
        {

            return null;
        }
    }

    internal class DependencyGraph
    {
        private DependencyNode _root;

        public void AddNode(string uri)
        {
            if (uri == null){return;}

            if (_root == null)
            {
                _root = new DependencyNode(uri);
            }
        }
    }

    internal class DependencyNode
    {
        private List<DependencyNode> _referenceNodes;

        public string BaseUri { get; }
        public string LocalId { get; }

        public DependencyNode(string uri)
        {
            _referenceNodes = new List<DependencyNode>();

            var lastForward = uri.LastIndexOf('/');
            var lastBackward = uri.LastIndexOf('\\');
            var idx = lastForward >= lastBackward ? lastForward : lastBackward;
            BaseUri = uri.Substring(0, idx);
            LocalId = uri.Substring(idx+1);
        }

        public string GetFullUri()
        {
            return BaseUri + "/" + LocalId;
        }
    }
}
