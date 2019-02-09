using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessor
{
    //Stores information about pre-processing directives
    class PreProcessorConfig
    {
        private PreProcessorConfig _self;

        public PreProcessorConfig GetInstance()
        {
            return _self ?? (_self = new PreProcessorConfig());
        }
    }
}
