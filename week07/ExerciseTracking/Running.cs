using System;

namespace ExerciseTracking
{
    /// <summary>
    /// A running activity. Stores the distance in miles directly.
    /// </summary>
    class Running : Activity
    {
        private double _distance;   // miles

        public Running(DateTime date, int minutes, double distance) : base(date, minutes)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;

        public override double GetSpeed() => (GetDistance() / Minutes) * 60;

        public override double GetPace() => Minutes / GetDistance();
    }
}
