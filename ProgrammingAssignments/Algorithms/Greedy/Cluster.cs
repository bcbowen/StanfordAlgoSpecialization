using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Greedy
{
    public class Cluster
    {
        public Cluster(int parent, int rank) 
        {
            Parent = parent;
            Rank = rank;
        }

        public int Parent { get; set; }
        public int Rank { get; set; }
    }
}
