using System;
using System.Globalization;

namespace ExerciseTracking
{
    /// <summary>
    /// Base class for all exercise activities. Holds the data every activity
    /// shares (the date and the duration in minutes) and declares the three
    /// calculation methods each activity type must implement. All values are
    /// reported in miles (mph, min per mile); the lap pool is 50 m per lap.
    /// </summary>
    abstract class Activity
    {
        private DateTime _date;
        private int _minutes;

        public Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        // Read-only accessors so callers can read the shared data without
        // reaching into the private fields.
        public DateTime Date => _date;
        public int Minutes => _minutes;

        // Each activity type calculates these from its own stored data.
        public abstract double GetDistance();   // miles
        public abstract double GetSpeed();      // mph
        public abstract double GetPace();       // min per mile

        /// <summary>
        /// Produces a one-line summary for the activity. Uses the abstract
        /// calculation methods, so the same implementation works for every
        /// derived class and needs no overriding.
        /// </summary>
        public string GetSummary()
        {
            // Format explicitly with the invariant culture so the output is
            // always "03 Nov 2022 ... 3.0 miles" regardless of the machine's
            // regional date/number settings.
            string date = _date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
            string distance = GetDistance().ToString("0.0", CultureInfo.InvariantCulture);
            string speed = GetSpeed().ToString("0.0", CultureInfo.InvariantCulture);
            string pace = GetPace().ToString("0.0", CultureInfo.InvariantCulture);

            return $"{date} {GetType().Name} ({_minutes} min)- " +
                   $"Distance {distance} miles, Speed {speed} mph, " +
                   $"Pace: {pace} min per mile";
        }
    }
}
