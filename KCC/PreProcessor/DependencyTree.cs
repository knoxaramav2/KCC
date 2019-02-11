using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessor
{
    public class DependencyTree
    {
    }

    class TranslationNode
    {
        private TranslationNode _dependants;
        private List<TranslationNode> _dependencies;

        private string _identifier;

    }
}
