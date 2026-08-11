using System;

namespace ExerciseTracking
{
    /// <summary>
    /// A stationary-bicycle activity. Stores the speed in miles per hour
    /// directly; distance and pace are derived from it.
    /// </summary>
    class Cycling : Activity
    {
        private double _speed;      // mph

        public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
        {
            _speed = speed;
        }

        // distance (miles) = speed (mph) * hours, and minutes / 60 = hours.
        public override double GetDistance() => (_speed * Minutes) / 60;

        public override double GetSpeed() => _speed;

        public override double GetPace() => 60 / _speed;
    }
}
