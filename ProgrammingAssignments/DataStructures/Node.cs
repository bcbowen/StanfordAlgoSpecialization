using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class Node : ICloneable
    {
        public int Value { get; set; }
        public List<int> LinkedNodes { get; set; } = new List<int>();

        public object Clone()
        {
            Node node = new Node { Value = Value };
            node.LinkedNodes.AddRange(LinkedNodes);

            return node;
        }

    }
}
