using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graph.Dijkstra
{
    public static class Algorithm
    {
        
        public static void CalculateShortestPaths_old(List<DijkstraNode> nodes, int startNodeId) 
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node = nodes.First(n => n.NodeId == startNodeId);
            Dictionary<int, List<int>> nodePaths = new Dictionary<int, List<int>>();
            AddNodesToHeap(startNodeId, nodes, heap, 0);
            while (heap.Count > 0) 
            {
                node = heap.Dequeue();
                if (nodes.Any(n => n.NodeId == node.ReferencedNode.NodeId && n.ReferencedNode != null))
                {
                    AddNodesToHeap(node.ReferencedNode.NodeId, nodes, heap, node.Value);
                    node.Processed = true;
                }
                else 
                {
                    if (!nodes.Any(n => n.NodeId == node.ReferencedNode.NodeId))
                    {
                        nodes.Add(new DijkstraNode(node.ReferencedNode.NodeId, int.MaxValue)); 
                    }

                    DijkstraNode leaf = nodes.First(n => n.NodeId == node.ReferencedNode.NodeId);
                    if (leaf.Value > node.Value) leaf.Value = node.Value;
                }
            }

        }

        public static Dictionary<int, NodeStatus> CalculateShortestPaths(List<DijkstraNode> nodes, int startNodeId)
        {
            DijkstraHeap heap = new DijkstraHeap();
            List<DijkstraNode> processed = new List<DijkstraNode>();
            Dictionary<int, NodeStatus> lengths = new Dictionary<int, NodeStatus>();
            //Dictionary<int, List<int>> nodePaths = new Dictionary<int, List<int>>();
            //nodePaths.Add(startNodeId, new List<int> { startNodeId });

            //Action<int, int, List<int>> SetNodeMinValue = (int nodeId, int value, List<int> path) =>
            Action<int, int> SetNodeMinValue = (int nodeId, int value) =>
            {
                /*
                if (!lengths.ContainsKey(nodeId))
                {
                    lengths.Add(nodeId, new NodeStatus);
                }
                else
                {
                */
                    if (lengths[nodeId].Length > value) 
                    {
                        lengths[nodeId].Length = value;
                    }
                    //lengths[nodeId] = System.Math.Min(lengths[nodeId], value);
                //}

                //if (!nodePaths.ContainsKey(nodeId)) nodePaths.Add(nodeId, new List<int>());
                //nodePaths[nodeId] = path;
            }; 

            foreach (DijkstraNode node in nodes) 
            {
                int distance = node.NodeId == startNodeId ? 0 : DijkstraNode.NoPathDistance;
                node.Value = distance;
                heap.Enqueue(node);
                if (!lengths.ContainsKey(node.NodeId)) lengths.Add(node.NodeId, new NodeStatus { Length = distance, UnprocessedReferenceCount = 0 }  );
                lengths[node.NodeId].UnprocessedReferenceCount++;
                //if (!nodePaths.ContainsKey(node.NodeId)) nodePaths.Add(node.NodeId, new List<int>());
            }

            int[] nodeIds = nodes.Select(n => n.NodeId).ToArray();
            int[] referencedNodeIds = nodes.Select(n => n.ReferencedNode.NodeId).ToArray();

            int[] leafIds = referencedNodeIds.Where(rn => !nodeIds.Any(n => n == rn)).Distinct().ToArray();
            // add leaves
            foreach (int leafId in leafIds) 
            {
                lengths.Add(leafId, new NodeStatus { Length = DijkstraNode.NoPathDistance, IsLeaf = true });
            }

            while (heap.Count > 0)
            {
                DijkstraNode node = heap.Dequeue();
                processed.Add(node);
                if (lengths[node.NodeId].Done && !lengths[node.NodeId].IsLeaf) continue;
                if (lengths[node.NodeId].Length > node.Value) lengths[node.NodeId].Length = node.Value;
                lengths[node.NodeId].UnprocessedReferenceCount--;

                // step 11: Maintain invariant
                List<DijkstraNode> matchingNodes = heap.Find(nodeId: node.NodeId);
                matchingNodes.Add(node);
                DijkstraNode updateNode;
                foreach (DijkstraNode matchingNode in matchingNodes) 
                {
                    if (matchingNode.Value != lengths[node.NodeId].Length) 
                    {
                        updateNode = heap.Remove(matchingNode.Index);
                        updateNode.Value = lengths[node.NodeId].Length;
                        heap.Enqueue(updateNode);
                    }
                    //List<int> path = new List<int>(nodePaths[matchingNode.NodeId]);
                    //if (!path.Contains(matchingNode.NodeId)) path.Add(matchingNode.NodeId);

                    List<DijkstraNode> referencedNodes = heap.Find(nodeId: matchingNode.ReferencedNode.NodeId);
                    if (referencedNodes.Count > 0)
                    {
                        foreach (DijkstraNode referencedNode in referencedNodes)
                        {
                            //SetNodeMinValue(referencedNode.NodeId, matchingNode.Value + matchingNode.ReferencedNode.Distance, path);
                            SetNodeMinValue(referencedNode.NodeId, matchingNode.Value + matchingNode.ReferencedNode.Distance);

                            if (referencedNode.Value != matchingNode.Value)
                            {
                                updateNode = heap.Remove(referencedNode.Index);
                                updateNode.Value = matchingNode.Value;
                                heap.Enqueue(updateNode);
                            }
                        }

                    }
                    else 
                    {
                        // this is a leaf
                        //SetNodeMinValue(matchingNode.ReferencedNode.NodeId, matchingNode.Value + matchingNode.ReferencedNode.Distance, path);
                        SetNodeMinValue(matchingNode.ReferencedNode.NodeId, matchingNode.Value + matchingNode.ReferencedNode.Distance);
                    }
                }
                // reset nodes still in the heap pointing to this node
                /*
                List<DijkstraNode> referencingNodes = heap.Find(referencedNodeId: node.NodeId);
                foreach (DijkstraNode referencingNode in referencingNodes.Where(n => !n.Processed)) 
                {
                    updateNode = heap.Remove(referencingNode.Index);
                    updateNode.Value = DijkstraNode.NoPathDistance;
                    heap.Enqueue(updateNode);
                }
                */

                /*
                if (nodes.Any(n => n.NodeId == node.ReferencedNode.NodeId && n.ReferencedNode != null))
                {
                    AddNodesToHeap(node.ReferencedNode.NodeId, nodes, heap, node.DijkstraValue);
                    node.Processed = true;
                }
                else
                {
                    if (!nodes.Any(n => n.NodeId == node.ReferencedNode.NodeId))
                    {
                        nodes.Add(new DijkstraNode(node.ReferencedNode.NodeId, int.MaxValue));
                    }

                    DijkstraNode leaf = nodes.First(n => n.NodeId == node.ReferencedNode.NodeId);
                    if (leaf.Value > node.DijkstraValue) leaf.Value = node.DijkstraValue;
                }

                */
            }
            return lengths;

        }

        private static void AddNodesToHeap(int nodeId, List<DijkstraNode> nodes, DijkstraHeap heap, int distance) 
        {
            DijkstraNode[] sourceNodes = nodes.Where(n => n.NodeId == nodeId && !n.Processed).ToArray();
            foreach (DijkstraNode sourceNode in sourceNodes)
            {
                if (sourceNode.Value > distance) sourceNode.Value = distance;
                heap.Enqueue(sourceNode);
            }            
        }

        public static Dictionary<int, NodeStatus> CalculateShortestPaths(string path, int startNodeId)
        {
            List<DijkstraNode> nodes = LoadGraph(path);
            return CalculateShortestPaths(nodes, startNodeId);
        }

        public static List<DijkstraNode> LoadGraph(string path)
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                DijkstraNode node;
                int nodeId;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    nodeId = int.Parse(fields[0]);

                    for (int i = 1; i < fields.Length; i++)
                    {

                        string[] nodeInfo = fields[i].Split(',');
                        if (nodeInfo.Length == 2)
                        {
                            node = new DijkstraNode(nodeId, DijkstraNode.NoPathDistance, int.Parse(nodeInfo[0]), int.Parse(nodeInfo[1]));
                            nodes.Add(node);
                        }
                    }
                }
                reader.Close();
            }

            return nodes;
        }
    }
}
