using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Shared;
namespace Algorithms.Graph.PrimMst
{
    public static class Algorithm
    {
        public static PrimTree RunPrimMst(UndirectedWeightedGraph graph) 
        {
            PrimTree tree = new PrimTree();

            //PrimNode winner = null;
         
            // Step 1: pick random initial node and assign initial weights. Find winner and add it to the heap. 
            int startNodeId = graph.Nodes[0].NodeId;
            MinHeap<Node> heap = new MinHeap<Node>();
            AddConnectedNodes(startNodeId, heap, graph.Nodes, graph.Edges);
            /*
            for (int i = 1; i < graph.Nodes.Count; i++) 
            {
                PrimEdge edge = graph.Edges.FirstOrDefault
                                            (
                                                e => e.Nodes[0].NodeId == System.Math.Min(startNodeId, graph.Nodes[i].NodeId) && 
                                                e.Nodes[1].NodeId == System.Math.Max(startNodeId, graph.Nodes[i].NodeId)
                                            );
                if (edge != null)
                {
                    graph.Nodes[i].Value = edge.Weight;
                    heap.Enqueue(graph.Nodes[i]);
                }
                else 
                {
                    graph.Nodes[i].Value = int.MaxValue;
                }
            }
            */

            // Main loop 
            List<Node> processedNodes = new List<Node>();
            while (heap.Count > 0) 
            {
                Node node = heap.Dequeue();
                processedNodes.Add(node);
                UndirectedWeightedEdge winner = FindWinner(node.NodeId, graph.Edges);
                tree.Edges.Add(winner);
                AddConnectedNodes(node.NodeId, heap, graph.Nodes, graph.Edges);
                /*
                foreach (PrimEdge edge in graph.Edges.Where(e => e.ContainsNode(node.NodeId) && !e.ContainsNodes(winner.Nodes[0].NodeId, winner.Nodes[1].NodeId))) 
                {
                    heap.Remove(edge.Nodes.First(n => n.NodeId != node.NodeId).NodeId);
                }
                */
            }

            return tree;
        }

        private static void AddConnectedNodes(int nodeId, MinHeap<Node> heap, List<Node> nodes, List<UndirectedWeightedEdge> edges) 
        {
            foreach (Node node in nodes.Where(n => n.Value == int.MaxValue))
            {
                UndirectedWeightedEdge edge = edges.FirstOrDefault(e => e.ContainsNodes(nodeId, node.NodeId));
                if (edge != null)
                {
                    node.Value = edge.Weight;
                    heap.Enqueue(node);
                }
            }
        }

        private static UndirectedWeightedEdge FindWinner(int nodeId, List<UndirectedWeightedEdge> edges) 
        {
            UndirectedWeightedEdge winner = null;

            foreach (UndirectedWeightedEdge edge in edges.Where(e => e.Nodes[0].NodeId == nodeId || e.Nodes[1].NodeId == nodeId)) 
            {
                if (winner == null || winner.Weight > edge.Weight) 
                {
                    winner = edge;
                }
            }

            return winner;
        }
    }

    
}
