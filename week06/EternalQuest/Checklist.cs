namespace EternalQuest
{
    /// <summary>
    /// A goal that must be completed a set number of times before it is done,
    /// e.g. "Attend the temple 10 times" (50 points each time, plus a 500 point
    /// bonus when the 10th visit is recorded).
    /// </summary>
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : this(name, description, points, target, bonus, 0)
        {
        }

        /// <summary>Overload used when loading a saved goal.</summary>
        public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = amountCompleted;
        }

        public int AmountCompleted => _amountCompleted;

        public int Target => _target;

        public int Bonus => _bonus;

        public override int RecordEvent()
        {
            if (IsComplete())
            {
                return 0; // already finished
            }
            _amountCompleted++;
            if (IsComplete())
            {
                return Points + Bonus; // the final time also pays out the bonus
            }
            return Points;
        }

        public override bool IsComplete() => _amountCompleted >= _target;

        public override string GetDetailsString()
        {
            string mark = IsComplete() ? "[X]" : "[ ]";
            return $"{mark} {Name} ({Description}) — Currently completed: {_amountCompleted}/{_target}";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal|{Name}|{Description}|{Points}|{_bonus}|{_target}|{_amountCompleted}";
        }
    }
}
