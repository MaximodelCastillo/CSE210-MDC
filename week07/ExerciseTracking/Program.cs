using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    /// <summary>
    /// Creates one activity of each type, puts them all in a single list of
    /// the base type, and prints each activity's summary through the base
    /// class's GetSummary method (polymorphism does the rest).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>
            {
                new Running(new DateTime(2022, 11, 3), 30, 3.0),
                new Cycling(new DateTime(2022, 11, 3), 45, 15.0),
                new Swimming(new DateTime(2022, 11, 3), 60, 40)
            };

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
