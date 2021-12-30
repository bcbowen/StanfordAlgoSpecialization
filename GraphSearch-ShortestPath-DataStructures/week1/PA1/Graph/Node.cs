using System;
using System.Collections.Generic;

namespace Graph
{
    public class Node
    {
        public int Value { get; set; }

        public LastStep Status { get; set; } = LastStep.Loaded;

        public List<Node> NextNodes { get; private set; } = new List<Node>();

        public List<int> NextNodeIds { get; private set; } = new List<int>();

        public List<Node> PreviousNodes { get; private set; } = new List<Node>();

        public int FinishTime { get; set; }
    }

    
}
