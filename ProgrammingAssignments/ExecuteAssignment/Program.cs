using System;

namespace ExecuteAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;
            do
            {
                Console.WriteLine("Choose a numeric option, or anything else to exit");

                Console.WriteLine("7. Median Maintenance (Course 2 week 3)");
                Console.WriteLine("8. Two Sum (Course 2 week 4)");
                ConsoleKeyInfo k = Console.ReadKey();
                Console.WriteLine();

                switch (k.KeyChar) 
                {
                    case '7':
                        Course2.Week3.MedianMaintenanceRunner.Run();
                        Console.WriteLine();
                        break;
                    case '8':
                        int result = Course2.Week4.TwoSumRunner.Run();
                        Console.WriteLine($"Result: {result}");
                        break;
                    default:
                        Console.WriteLine("Selected key doesn't match choice, we're outta here");
                        done = true;
                        break;
                }
            } while (!done);
            


            Console.WriteLine("Press any key to exit, man.");
            Console.ReadKey();
        }
    }
}
