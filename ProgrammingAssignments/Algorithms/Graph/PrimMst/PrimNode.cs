using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Algorithms.Shared;

namespace Algorithms.Graph.PrimMst
{
    public class PrimNode : Node
    {
        public PrimNode(int value, int nodeId, int referencedNode) : base(value)
        { 
            NodeId = nodeId;
            ReferencedNode = referencedNode;
        }

        public int NodeId { get; set; }
        public int ReferencedNode { get; set; }
    }
}
