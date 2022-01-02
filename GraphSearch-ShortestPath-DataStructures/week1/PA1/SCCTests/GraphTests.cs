using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using Graph;

namespace SCCTests
{

    public class GraphTests
    {            

        private DirectedGraph InitGraphValues(int[] values) 
        {
            DirectedGraph graph = new DirectedGraph();
            foreach (int value in values)
            {
                graph.Nodes.Add(value, new Node { Value = value });
            }

            return graph;
        }
        

        
    }
}
