﻿using System;

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
                ConsoleKeyInfo k = Console.ReadKey();
                Console.WriteLine();

                switch (k.KeyChar) 
                {
                    case '7': 
                        Console.WriteLine("median maintenance dude");
                        Console.WriteLine();
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
