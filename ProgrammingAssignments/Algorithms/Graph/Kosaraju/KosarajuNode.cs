using System.Collections.Generic;

using Algorithms.Shared; 

namespace Algorithms.Graph.Kosaraju
{
    public class KosarajuNode : NodeBase
    {
        public KosarajuNode(int nodeId) : base(nodeId, 0) { }

        public LastStep Status { get; set; } = LastStep.Loaded;

        public List<KosarajuNode> NextNodes { get; private set; } = new List<KosarajuNode>();

        public List<int> NextNodeIds { get; private set; } = new List<int>();

        public List<KosarajuNode> PreviousNodes { get; private set; } = new List<KosarajuNode>();

    }
}
