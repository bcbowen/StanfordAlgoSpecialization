using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
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
