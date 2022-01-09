namespace Graph
{
    public class ReferencedNode : Node
    {
        public int Distance { get; set; }

        public ReferencedNode(int id, int distance) : base(id)
        {
            Distance = distance;
        }
    }
}
