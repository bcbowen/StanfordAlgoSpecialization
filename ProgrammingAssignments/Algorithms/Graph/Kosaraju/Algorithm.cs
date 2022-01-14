using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph.Kosaraju
{
    public static class Algorithm
    {
        /// <summary>
        /// Return the n biggest components (node count) in desc order
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] CalculateStronglyConnectedComponents(int count, KosarajuGraph graph) 
        {
            FirstPass(graph);
            SecondPass(graph);
            int[] result = GetSCCCounts(count, graph);
            return result;
        }

        /// <summary>
        /// Traverse the nodes from back to front and do DFS, mark the finishing time value for each node
        /// </summary>
        internal static void FirstPass(KosarajuGraph graph)
        {
            Stack<KosarajuNode> nodeStack = new Stack<KosarajuNode>();
            KosarajuNode node;
            int nextFinishTime = 1;
            foreach (int key in graph.Nodes.Keys.OrderByDescending(k => k))
            {
                node = graph.Nodes[key];
                if (node.Status != LastStep.FirstPass)
                {
                    node.Status = LastStep.FirstPass;
                    nodeStack.Push(node);
                    KosarajuNode nextNode;
                    do
                    {
                        while ((nextNode = node.NextNodes.FirstOrDefault(n => n.Status != LastStep.FirstPass)) != null)
                        {
                            node = nextNode;
                            node.Status = LastStep.FirstPass;
                            nodeStack.Push(node);
                        }
                        graph.FinishTimes.Add(nextFinishTime++, node);
                        int lastValue = node.Value;
                        do
                        {
                            if (nodeStack.Peek().NextNodes.Any(n => n.Status != LastStep.FirstPass))
                            {
                                // don't remove a node from the stack if it has other edges to explore
                                node = nodeStack.Peek();
                            }
                            else
                            {
                                node = nodeStack.Pop();
                            }

                        } while (node.Value == lastValue && nodeStack.Count > 0);

                        //SetFinishTimes(node);
                    } while (nodeStack.Count > 0);

                    // add the final node
                    graph.FinishTimes.Add(nextFinishTime++, node);

                }

            }
        }

        internal static void SecondPass(KosarajuGraph graph)
        {
            Stack<KosarajuNode> nodeStack = new Stack<KosarajuNode>();
            Component currentComponent;
            KosarajuNode previousNode;
            int nextComponentGroupId = 1;
            for (int i = graph.FinishTimes.Count - 1; i >= 0; i--)
            {
                KosarajuNode node = graph.FinishTimes[graph.FinishTimes.Keys[i]];

                if (node.Status != LastStep.SecondPass)
                {
                    nodeStack.Push(node);
                    currentComponent = new Component(nextComponentGroupId++);
                    currentComponent.Leader = node;
                    currentComponent.Nodes.Add(node);
                    node.Status = LastStep.SecondPass;
                    do
                    {
                        while ((previousNode = node.PreviousNodes.FirstOrDefault(n => n.Status != LastStep.SecondPass)) != null)
                        {
                            node = previousNode;
                            node.Status = LastStep.SecondPass;
                            currentComponent.Nodes.Add(node);
                            nodeStack.Push(node);
                        }

                        int lastValue = node.Value;
                        do
                        {
                            if (nodeStack.Peek().PreviousNodes.Any(n => n.Status != LastStep.SecondPass))
                            {
                                node = nodeStack.Peek();
                            }
                            else
                            {
                                node = nodeStack.Pop();
                            }

                        } while (node.Value == lastValue && nodeStack.Count > 0);

                    } while (nodeStack.Count > 0);

                    graph.Components.Add(currentComponent);

                }
            }
        }

        internal static int[] GetSCCCounts(int count, KosarajuGraph graph)
        {
            int[] result = new[] { 0, 0, 0, 0, 0 };
            var componentCounts = (from component in graph.Components select new { Count = component.Nodes.Count }).OrderByDescending(c => c.Count).Take(count).ToArray();
            for (int i = 0; i < componentCounts.Length; i++)
            {
                result[i] = componentCounts[i].Count;
            }

            return result;
        }
    }
}
