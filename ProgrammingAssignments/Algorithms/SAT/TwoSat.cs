using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SAT
{
    public static class TwoSat
    {
        private static Random Randomizer = new Random();
        public static bool IsSatisfiable(string fileName) 
        {
            //Dictionary<int, bool> settings;
            //List<Condition> conditions;
            (Dictionary<int, bool> settings, List<Condition> conditions) = LoadFile(fileName);

            //int tweakage = conditions.Count;

            //double outerIterations = System.Math.Min(tweakage, conditions.Count * System.Math.Log2(conditions.Count));
            //double innerIterations = 2 * System.Math.Pow(conditions.Count, 2);

            double outerIterations = conditions.Count;

            for (int i = 0; i < outerIterations; i++) 
            {
                Shuffle(settings);
                
                for (int j = 0; j < outerIterations; j++) 
                {
                    //List<int> unsatisfiedClauses;
                    //bool isSatisfied;
                    (bool isSatisfied, List<int> unsatisfiedClauses) = IsSatisfied(settings, conditions);
                    if (isSatisfied) return true;

                    int randomIndex = Randomizer.Next(0, unsatisfiedClauses.Count - 1);
                    int key = unsatisfiedClauses[randomIndex];
                    settings[key] = !settings[key];
                }
            }

            return false;
        }

        internal static (bool, List<int>) IsSatisfied(Dictionary<int, bool> settings, List<Condition> conditions) 
        {
            // TODO: This doesn't work
            List<int> unsatisfiedClauses = new List<int>();
            bool result; 
            foreach(Condition condition in conditions) 
            {
                result = (settings[condition.Value1] ? condition.Is1 : !condition.Is1);
                result = result | (settings[condition.Value2] ? condition.Is2 : !condition.Is2);
                //if (condition.Is1 != settings[condition.Value1]) unsatisfiedClauses.Add(condition.Value1);
                //if (condition.Is2 != settings[condition.Value2]) unsatisfiedClauses.Add(condition.Value2);
                if (!result) 
                {
                    unsatisfiedClauses.Add(condition.Value1);
                    unsatisfiedClauses.Add(condition.Value2);
                }
            }

            return (unsatisfiedClauses.Count == 0, unsatisfiedClauses);
        }

        private static void Shuffle(Dictionary<int, bool> settings) 
        {
            foreach (int key in settings.Keys) 
            {
                int next = Randomizer.Next(2);
                settings[key] = next == 1;
            }
        }

        public static (Dictionary<int, bool>, List<Condition>) LoadFile(string fileName) 
        { 
            Dictionary<int, bool> settings = new Dictionary<int, bool>();
            List<Condition> conditions = new List<Condition>();

            using (StreamReader reader = new StreamReader(fileName)) 
            {
                string line;
                // first line is count... ignore
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null) 
                {
                    Condition condition = Condition.Parse(line);
                    conditions.Add(condition);
                    if (!settings.ContainsKey(condition.Value1)) 
                    {
                        settings.Add(condition.Value1, true);
                    }

                    if (!settings.ContainsKey(condition.Value2))
                    {
                        settings.Add(condition.Value2, true);
                    }
                }
                reader.Close();
            
            }

            return (settings, conditions);
        }
    }
}
