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
        /// <summary>
        /// O(mn) Straightforward algorithm from Alghorithms Illuminated vol 3 pg 58
        /// </summary>
        /// <returns></returns>
        public static PrimTree RunPrimMstBasic(UndirectedWeightedGraph graph) 
        {
            List<Node> processedNodes = new List<Node>();
            List<Node> unprocessedNodes = new List<Node>(); 
            
            PrimTree tree = new PrimTree();

            int startNodeId = graph.Nodes[0].NodeId;
            processedNodes.Add(graph.Nodes[0]);
            unprocessedNodes.AddRange(graph.Nodes.Where(n => n.NodeId != startNodeId));

            // main loop 
            UndirectedWeightedEdge minEdge;
            int processedNodeId = -1;
            int unprocessedNodeId = -1;
            do 
            {
                (processedNodeId, unprocessedNodeId, minEdge) = FindMinCrossingEdge(graph.Edges, processedNodes, unprocessedNodes);
                if (minEdge != null) 
                {
                    Node node = unprocessedNodes.FirstOrDefault(n => n.NodeId == unprocessedNodeId);
                    processedNodes.Add(node);
                    unprocessedNodes.Remove(node);
                    tree.Edges.Add(minEdge);
                }
            } 
            while (minEdge != null);

            return tree;
        }

        private static (int, int, UndirectedWeightedEdge) FindMinCrossingEdge(List<UndirectedWeightedEdge> edges, List<Node> processedNodes, List<Node> unprocessedNodes) 
        {
            int processedNodeId = -1;
            int unprocessedNodeId = -1;
            UndirectedWeightedEdge minEdge = null;

            //foreach (UndirectedWeightedEdge edge in edges.Where(e => e.Nodes.ToList().Intersect(processedNodes).ToList().Count == 1 && e.Nodes.ToList().Intersect(unprocessedNodes).ToList().Count == 1)) 
            foreach (UndirectedWeightedEdge edge in edges.Where(e => IsCrossing(e, processedNodes, unprocessedNodes)))
            {
                if (minEdge == null || edge.Weight < minEdge.Weight) 
                {
                    minEdge = edge;
                    processedNodeId = edge.Nodes.First(n => processedNodes.Any(pn => pn.NodeId == n.NodeId)).NodeId;
                    unprocessedNodeId = edge.Nodes.First(n => unprocessedNodes.Any(pn => pn.NodeId == n.NodeId)).NodeId;
                }
            }

            return (processedNodeId, unprocessedNodeId, minEdge);
        }

        private static bool IsCrossing(UndirectedWeightedEdge edge, List<Node> processedNodes, List<Node> unprocessedNodes) 
        {
            if (!processedNodes.Any(n => n.NodeId == edge.Nodes[0].NodeId) && !processedNodes.Any(n => n.NodeId == edge.Nodes[1].NodeId))
            {
                return false;
            }

            return unprocessedNodes.Any(n => n.NodeId == edge.Nodes[0].NodeId) || unprocessedNodes.Any(n => n.NodeId == edge.Nodes[1].NodeId);
        }

        /// <summary>
        /// Todo: Finish heap-based implementation as shown in book (O((m + n) log(n)))
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static PrimTree RunPrimMst(UndirectedWeightedGraph graph) 
        {
            PrimTree tree = new PrimTree();

            //PrimNode winner = null;
         
            // Step 1: pick random initial node and assign initial weights. Find winner and add it to the heap. 
            int startNodeId = graph.Nodes[0].NodeId;
            MinHeap<Node> heap = new MinHeap<Node>();
            List<Node> processedNodes = new List<Node>();

            UndirectedWeightedEdge winner = FindWinner(startNodeId, graph.Edges, processedNodes);
            tree.Edges.Add(winner);
            processedNodes.Add(graph.Nodes.First(n => n.NodeId == startNodeId));
            AddConnectedNodes(startNodeId, heap, graph.Nodes, graph.Edges, processedNodes);
            
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
            
            while (heap.Count > 0) 
            {
                Node node = heap.Dequeue();
                processedNodes.Add(node);
                winner = FindWinner(node.NodeId, graph.Edges, processedNodes);
                if (winner != null)
                {
                    tree.Edges.Add(winner);
                    AddConnectedNodes(node.NodeId, heap, graph.Nodes, graph.Edges, processedNodes);
                }
                
                /*
                foreach (PrimEdge edge in graph.Edges.Where(e => e.ContainsNode(node.NodeId) && !e.ContainsNodes(winner.Nodes[0].NodeId, winner.Nodes[1].NodeId))) 
                {
                    heap.Remove(edge.Nodes.First(n => n.NodeId != node.NodeId).NodeId);
                }
                */
            }

            return tree;
        }

        private static void AddConnectedNodes(int nodeId, MinHeap<Node> heap, List<Node> nodes, List<UndirectedWeightedEdge> edges, List<Node> processedNodes) 
        {
            foreach (Node node in nodes.Where(n => n.Value == int.MaxValue && n.NodeId != nodeId && !processedNodes.Any(pn => pn.NodeId == n.NodeId)))
            {
                UndirectedWeightedEdge edge = edges.FirstOrDefault(e => e.ContainsNodes(nodeId, node.NodeId));
                if (edge != null)
                {
                    node.Value = edge.Weight;
                    heap.Enqueue(node);
                }
            }
        }

        private static UndirectedWeightedEdge FindWinner(int nodeId, List<UndirectedWeightedEdge> edges, List<Node> processedNodes) 
        {
            UndirectedWeightedEdge winner = null;

            // get edge containing this node where the other node has not been processed yet and the weight is min
            foreach (UndirectedWeightedEdge edge in edges.Where(e => e.ContainsNode(nodeId)&& !processedNodes.Any(n => n.NodeId == e.OtherNodeId(nodeId)))) 
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
