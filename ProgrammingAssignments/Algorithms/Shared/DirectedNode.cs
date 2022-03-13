using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Shared
{
    public class DirectedNode : NodeBase
    {
        public DirectedNode(int nodeId, int referencedNodeId, int value) : base(nodeId, value) 
        {
            ReferencedNodeId = referencedNodeId;
        }

        public int ReferencedNodeId { get; set; }

    }
}
