namespace Algorithms.Tree.OptimizedBinaryTree
{
    public class OptimizedTreeNode
    {
        public OptimizedTreeNode(int nodeId, decimal weight) 
        {
            NodeId = nodeId;
            Weight = weight;
        }

        public int NodeId { get; set; }
        public decimal Weight { get; set; }

        public static int CompareNodeIds(OptimizedTreeNode node1, OptimizedTreeNode node2) 
        {
            return node1.NodeId.CompareTo(node2.NodeId);
        }

        public OptimizedTreeNode Left { get; set; } = null;
        
        public OptimizedTreeNode Right { get; set; } = null;

        public void Append(OptimizedTreeNode node) 
        {
            if (node.NodeId < NodeId)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else 
                {
                    Left.Append(node);
                }
            }
            else 
            {
                if (Right == null)
                {
                    Right = node;
                }
                else 
                {
                    Right.Append(node);
                }

            }
        }

        public OptimizedTreeNode Find(int nodeId) 
        {
            if (NodeId == nodeId)
            {
                return this;
            }
            else 
            {
                if (nodeId < NodeId) 
                {
                    return Left != null ? Left.Find(nodeId) :null;
                }
                else 
                {
                    return Right != null ? Right.Find(nodeId) : null;
                }
            
            }
        }
    }
}
