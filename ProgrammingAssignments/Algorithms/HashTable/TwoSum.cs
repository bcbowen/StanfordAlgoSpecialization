using System.Collections.Generic;
using System.IO;

namespace Algorithms.HashTable
{
    public static class TwoSum
    {

        public static int GetSums(string path) 
        {
          
            int hits = 0;
            List<long> negativos = new List<long>();
            Dictionary<int, List<long>> positivos = new Dictionary<int, List<long>>();
            string line;
            using (StreamReader reader = new StreamReader(path)) 
            {
                while ((line = reader.ReadLine()) != null) 
                {
                    long value = long.Parse(line);
                    if (value < 0)
                    {
                        negativos.Add(value);
                    }
                    else 
                    {
                        int hash = value.GetHashCode();
                        if (!positivos.ContainsKey(hash)) 
                        { 
                            positivos.Add(hash, new List<long>());  
                        }
                        positivos[hash].Add(value);
                    }
                }
                reader.Close();
            }


            //using (StreamWriter writer = new StreamWriter("matches.log"))
            //{

            for (int t = -10000; t <= 10000; t++) 
            {
                foreach (long x in negativos) 
                {
                    long y = t - x;
                    int hash = y.GetHashCode();
                    if (positivos.ContainsKey(hash) && positivos[hash].Contains(y))
                    { 
                        //writer.WriteLine($"{x}\t{y}\t{t}\t");
                        hits++;
                        break;
                    }
                }
            }       
            //    writer.Close();
            //}

            return hits;
        }
    }
}
