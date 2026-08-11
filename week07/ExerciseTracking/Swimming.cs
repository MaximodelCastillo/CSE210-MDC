using System;

namespace ExerciseTracking
{
    /// <summary>
    /// A lap-pool swimming activity. Stores the number of laps; the lap
    /// distance is 50 m, so everything else is derived from the lap count.
    /// </summary>
    class Swimming : Activity
    {
        private int _laps;

        public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
        {
            _laps = laps;
        }

        // 50 m per lap -> km, then km to miles (1 km = 0.62 miles).
        public override double GetDistance() => _laps * 50 / 1000.0 * 0.62;

        public override double GetSpeed() => (GetDistance() / Minutes) * 60;

        public override double GetPace() => Minutes / GetDistance();
    }
}
