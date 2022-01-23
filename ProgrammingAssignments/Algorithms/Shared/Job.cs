namespace Algorithms.Shared
{
    public class Job
    {
        public Job(int weight, int length) 
        {
            Weight = weight;
            Length = length;
        }

        public int Weight { get; set; }
        
        public int Length { get; set; }

        public int TotalLength { get; set; }

        public int WeightedCompletionTime 
        {
            get 
            {
                return Weight * TotalLength;
            }
        }

        public int WeightDifference 
        { 
            get { return Weight - Length; } 
        }

        public decimal WeightRatio
        {
            get { return (decimal)Weight / Length; }
        }
    }
}

