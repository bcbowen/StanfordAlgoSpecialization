using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Greedy
{
    public class Cluster<T>
    {
        public Cluster(T parent, int rank) 
        {
            Parent = parent;
            Rank = rank;
        }

        public T Parent { get; set; }
        public int Rank { get; set; }
    }
}
