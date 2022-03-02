using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceryPlugin
{
    public class RelationshipTracker
    {
        public RelationshipTracker()
        {
            relationships_ = new Dictionary<Tuple<int, int>, float>();
        }

        void SetRelationshipStatus(int id1, int id2, float status)
        {
            relationships_[new Tuple<int, int>(id1, id2)] = status;
        }

        float GetRelationshipStatus(int id1, int id2, float status)
        {
            Tuple<int, int> key = new Tuple<int, int>(id1, id2);
            if (relationships_.ContainsKey(key))
            {
                return relationships_[key];
            }
            return float.NaN;
        }

        private Dictionary<Tuple<int, int>, float> relationships_;
    }
}
