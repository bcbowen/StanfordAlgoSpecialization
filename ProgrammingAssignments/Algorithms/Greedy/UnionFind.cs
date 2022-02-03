using System.Collections.Generic;


namespace Algorithms.Greedy
{
    public class UnionFind
    {
        private List<Cluster<int>> _clusters;
        public int ClusterCount 
        {
            get { return _clusters.Count; }
        }
        public UnionFind(int n) 
        {
            _clusters = new List<Cluster<int>>(n);
            
            // Initially there is one set per number with rank 1; add extra node at the end to compensate for index offset
            for (int i = 0; i <= n; i++) _clusters.Add(new Cluster<int>(i, 1));
        }

        public int Find(int x)
        {
            // do path compression during find
            if (_clusters[x].Parent != x) _clusters[x].Parent = Find(_clusters[x].Parent);

            return _clusters[x].Parent;
        }

        public void Union(int x, int y)
        {
            int xParent = Find(x);
            int yParent = Find(y);

            if (xParent == yParent) return;

            // union by rank
            if (_clusters[xParent].Rank < _clusters[yParent].Rank)
                _clusters[xParent].Parent = yParent;
            else
            {
                _clusters[yParent].Parent = xParent;
                if (_clusters[xParent].Rank == _clusters[yParent].Rank) _clusters[xParent].Rank++;
            }
        }

    }
}
