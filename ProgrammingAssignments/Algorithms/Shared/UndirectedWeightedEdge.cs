using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Shared
{
    public class UndirectedWeightedEdge : UndirectedEdge
    {
        public UndirectedWeightedEdge(int value1, int value2, int weight) : base(value1, value2) 
        {
            Weight = weight;
        }

        public int Weight { get; private set; }
    }
}
