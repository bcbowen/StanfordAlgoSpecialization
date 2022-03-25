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
            Dictionary<int, bool> settings;
            List<Condition> conditions;
            (settings, conditions) = LoadFile(fileName);

            for (int i = 0; i < System.Math.Log2(conditions.Count); i++) 
            {
                Shuffle(settings);
                for (int j = 0; j < 2 * System.Math.Pow(conditions.Count, 2); j++) 
                {
                    List<int> unsatisfiedClauses;
                    bool isSatisfied;
                    (isSatisfied, unsatisfiedClauses) = IsSatisfied(settings, conditions);
                    if (isSatisfied) return true;

                    int randomIndex = Randomizer.Next(0, unsatisfiedClauses.Count - 1);
                    settings[randomIndex] = !settings[randomIndex];
                }
            }

            return false;
        }

        private static (bool, List<int>) IsSatisfied(Dictionary<int, bool> settings, List<Condition> conditions) 
        {
            List<int> unsatisfiedClauses = new List<int>();
            foreach(Condition condition in conditions) 
            {
                if (condition.Is1 != settings[condition.Value1]) unsatisfiedClauses.Add(condition.Value1);
                if (condition.Is2 != settings[condition.Value2]) unsatisfiedClauses.Add(condition.Value2);
            }

            return (unsatisfiedClauses.Count == 0, unsatisfiedClauses);
        }

        private static void Shuffle(Dictionary<int, bool> settings) 
        {
            foreach (int key in settings.Keys) 
            {
                settings[key] = Randomizer.Next(0, 1) == 1;
            }
        }

        public static (Dictionary<int, bool>, List<Condition>) LoadFile(string fileName) 
        { 
            Dictionary<int, bool> settings = new Dictionary<int, bool>();
            List<Condition> conditions = new List<Condition>();

            using (StreamReader reader = new StreamReader(fileName)) 
            {
                string line;
                while ((line = reader.ReadLine()) != null) 
                {
                    Condition condition = Condition.Parse(line);
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
