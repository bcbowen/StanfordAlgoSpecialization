using System.Collections.Generic;
using System.IO;

using Algorithms.Shared;

namespace Algorithms.Greedy
{
    public static class Scheduler
    {
        public static List<Job> ScheduleJobsByDifference(string path) 
        {
            List<Job> jobs = LoadJobs(path);
            jobs.Sort(CompareJobsByDifference);
            jobs[0].TotalLength = jobs[0].Length;
            for (int i = 1; i < jobs.Count; i++) 
            {
                jobs[i].TotalLength = jobs[i].Length + jobs[i - 1].TotalLength;
            }

            return jobs;
        }
        public static List<Job> ScheduleJobsByRatio(string path) 
        {
            List<Job> jobs = LoadJobs(path);
            jobs.Sort(CompareJobsByRatio);
            jobs[0].TotalLength = jobs[0].Length;
            for (int i = 1; i < jobs.Count; i++)
            {
                jobs[i].TotalLength = jobs[i].Length + jobs[i - 1].TotalLength;
            }

            return jobs;
        }

        private static List<Job> LoadJobs(string path) 
        {
            List<Job> jobs = new List<Job>();
            using (StreamReader reader = new StreamReader(path)) 
            {
                string line;
                reader.ReadLine(); // first line is number of jobs... discard
                string[] fields; 
                while ((line = reader.ReadLine()) != null) 
                {
                    fields = line.Split(" ");
                    if (fields.Length > 1)
                    {
                        Job job = new Job(int.Parse(fields[0]), int.Parse(fields[1]));
                        jobs.Add(job);
                    }
                }

                reader.Close();
            }

            return jobs;
        }

        internal static int CompareJobsByDifference(Job l, Job r) 
        {
            int result;
            if (l.WeightDifference == r.WeightDifference)
            {
                result = l.Weight.CompareTo(r.Weight);
            }
            else
            {
                result = l.WeightDifference.CompareTo(r.WeightDifference);
            }

            // reverse sign to sort in desc order
            if (result != 0) result = -result;
            
            return result;
        }

        internal static int CompareJobsByRatio(Job l, Job r) 
        {
            // reverse sign to sort in desc order
            return -l.WeightRatio.CompareTo(r.WeightRatio);
        }
    }
}
