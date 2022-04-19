using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceryPlugin
{
    public class RelationshipAgent
    {
        private static int next_id_ = 0;

        public int agentId { get; private set; }

        public RelationshipAgent()
        {
            agentId = next_id_++;
        }
    }
}
