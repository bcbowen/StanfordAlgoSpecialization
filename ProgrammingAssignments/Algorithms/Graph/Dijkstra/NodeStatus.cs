namespace Algorithms.Graph.Dijkstra
{
    public class NodeStatus
    {
        public int UnprocessedReferenceCount { get; set; }

        public int Length { get; set; }

        public bool Done 
        {
            get { return UnprocessedReferenceCount == 0; }
        }

        public bool IsLeaf { get; set; } = false;
    }
}
