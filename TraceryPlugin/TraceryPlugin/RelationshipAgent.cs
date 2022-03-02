using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceryPlugin
{
    class RelationshipAgent
    {
        private static int next_id_ = 0;

        private int my_id_;

        public RelationshipAgent()
        {
            my_id_ = next_id_++;
        }
    }
}
