namespace EternalQuest
{
    /// <summary>
    /// A goal that is done exactly once, e.g. "Run a marathon" (1000 points).
    /// Once it has been recorded it stays complete and cannot be recorded again.
    /// </summary>
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false;
        }

        /// <summary>Overload used when loading a saved goal.</summary>
        public SimpleGoal(string name, string description, int points, bool isComplete)
            : base(name, description, points)
        {
            _isComplete = isComplete;
        }

        public override int RecordEvent()
        {
            if (_isComplete)
            {
                return 0; // already done — no points for repeating it
            }
            _isComplete = true;
            return Points;
        }

        public override bool IsComplete() => _isComplete;

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal|{Name}|{Description}|{Points}|{_isComplete}";
        }
    }
}
