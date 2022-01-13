using System.Collections.Generic;

namespace DataStructures.Kosaraju
{
    public class Component
    {
        public Component(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public Node Leader { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();
    }
}
