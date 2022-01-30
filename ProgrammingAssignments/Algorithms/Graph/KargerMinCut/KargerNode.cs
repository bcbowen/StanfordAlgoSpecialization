using System;
using System.Collections.Generic;

using Algorithms.Shared;

namespace Algorithms.Graph.KargerMinCut
{
    public class KargerNode : NodeBase, ICloneable
    {
        public KargerNode(int nodeId) : base(nodeId, 0)
        {
        
        }

        
        public List<int> LinkedNodes { get; set; } = new List<int>();

        public object Clone()
        {
            KargerNode node = new KargerNode(Value);
            node.LinkedNodes.AddRange(LinkedNodes);

            return node;
        }

    }
}
