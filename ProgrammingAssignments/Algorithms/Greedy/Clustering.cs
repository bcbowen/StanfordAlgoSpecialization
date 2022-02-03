using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Algorithms.Shared;

namespace Algorithms.Greedy
{
    public static class Clustering
    {
        public static int RunEdgeCluster(List<UndirectedWeightedEdge> edges, int clusterCount, int nodeCount)
        {
            edges.Sort(UndirectedWeightedEdge.WeightAsecendingComparison);
            UnionFind unionFind = new UnionFind(nodeCount);
            int runningClusterCount = nodeCount;
            
            while (runningClusterCount > clusterCount) 
            {
                for (int i = 0; i < edges.Count; i++) 
                {
                    UndirectedWeightedEdge edge = edges[i];
                    if (unionFind.Find(edge.Nodes[0].NodeId) != unionFind.Find(edge.Nodes[1].NodeId)) 
                    { 
                        unionFind.Union(edge.Nodes[0].NodeId, edge.Nodes[1].NodeId);
                        if (--runningClusterCount == clusterCount) break;
                    }
                }
            }

            // find the shortest edge with endpoints in different clusters
            int maxSpace = 0;
            
            foreach (UndirectedWeightedEdge edge in edges) 
            {
                if (unionFind.Find(edge.Nodes[0].NodeId) != unionFind.Find(edge.Nodes[1].NodeId))
                {
                    maxSpace = edge.Weight;
                    break;
                }
            }
            return maxSpace;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusters">Key is integer that represents the cluster, value is the number of occurrences of the key within the cluster</param>
        /// <returns></returns>
        public static int RunBinaryCluster(Dictionary<string, int> clusters) 
        {
            UnionFind unionFind = new UnionFind(clusters.Keys.Count);
            int runningClusterCount = clusters.Keys.Count;

            Func<char, char> flip = (char c) => 
            {
                return c == '0' ? '1' : '0';
            };

            StringBuilder temp; 
            // merge clusters off by 1
            foreach (string key in clusters.Keys) 
            {
                for (int i = 0; i < key.Length; i++) 
                {
                    temp = new StringBuilder(key);
                    temp[i] = flip(key[i]);
                    string searchKey = temp.ToString();
                    if (clusters.ContainsKey(searchKey)) 
                    {
                        if (unionFind.Find(clusters[key]) != unionFind.Find(clusters[searchKey])) 
                        {
                            unionFind.Union(clusters[key], clusters[searchKey]);
                            runningClusterCount--;
                        }
                    }
                }
            }

            // merge clusters off by 2
            foreach (string key in clusters.Keys)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    for (int j = i + 1; j < key.Length - 1; j++) 
                    {
                        temp = new StringBuilder(key);
                        temp[i] = flip(key[i]);
                        temp[j] = flip(key[j]);
                        string searchKey = temp.ToString();
                        if (clusters.ContainsKey(searchKey))
                        {
                            if (unionFind.Find(clusters[key]) != unionFind.Find(clusters[searchKey]))
                            {
                                unionFind.Union(clusters[key], clusters[searchKey]);
                                runningClusterCount--;
                            }
                        }
                    }
                    
                }
            }

            return runningClusterCount;
        }

        public static (List<UndirectedWeightedEdge>, int) LoadEdgeCollection(string fileName) 
        {
            /*
                128
                1 2 10806
                1 3 14366
                1 4 4124
             */

            List<UndirectedWeightedEdge> edges = new List<UndirectedWeightedEdge>();
            int clusterCount;
            using (StreamReader reader = new StreamReader(fileName)) 
            {
                string line;
                // first line has cluster count, all other lines have edges
                clusterCount = int.Parse(reader.ReadLine());
                string[] fields;
                while ((line = reader.ReadLine()) != null) 
                {
                    fields = line.Split(' ');
                    if (fields.Length > 2) 
                    {
                        edges.Add(new UndirectedWeightedEdge(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2])));
                    }
                }
            }

            return (edges, clusterCount);
        }

        public static Dictionary<string, int> LoadBinaryCluster(string fileName) 
        {
            /*
                4 14
                1 1 0 0 1 0 1 0 1 1 1 0 1 1
                1 1 0 1 0 1 1 0 0 0 1 1 0 0
                0 0 1 0 1 0 0 0 0 0 1 1 0 0
                0 1 0 1 0 1 1 0 0 1 1 1 0 0
            */
            Dictionary<string, int> cluster = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(fileName))
            {   
                string line;
                // first line has info we don't need (number of lines and number of bits per line)
                reader.ReadLine();
                int index = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Replace(" ", "");
                    if (!cluster.ContainsKey(line))
                    {
                        cluster[line] = index++;
                    }
                }
                reader.Close();
            }

            return cluster;

        }
    }
}
