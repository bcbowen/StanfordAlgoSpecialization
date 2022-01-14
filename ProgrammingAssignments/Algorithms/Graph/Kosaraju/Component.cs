using System.Collections.Generic;

namespace Algorithms.Graph.Kosaraju
{
    public class Component
    {
        public Component(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public KosarajuNode Leader { get; set; }
        public List<KosarajuNode> Nodes { get; set; } = new List<KosarajuNode>();
    }
}
