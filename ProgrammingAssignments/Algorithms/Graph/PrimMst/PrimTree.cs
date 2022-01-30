using System.Collections.Generic;
using System.Linq;

using Algorithms.Shared;

namespace Algorithms.Graph.PrimMst
{
    public class PrimTree
    {
        public List<UndirectedWeightedEdge> Edges { get; private set; } = new List<UndirectedWeightedEdge>();

        public long TotalWeight
        {
            get 
            {
                return Edges.Sum(e => e.Weight);
            }
        }

    }
}
