using System;

namespace EternalQuest
{
    /// <summary>
    /// The abstract base class for every kind of goal. It holds the data that all
    /// goals share (name, description, points) and defines the contract that each
    /// concrete goal type must fulfill: record an event, report whether it is
    /// complete, render itself for the goal list, and serialize itself for saving.
    /// </summary>
    public abstract class Goal
    {
        private string _shortName;
        private string _description;
        private int _points;

        protected Goal(string name, string description, int points)
        {
            _shortName = name;
            _description = description;
            _points = points;
        }

        public string Name => _shortName;

        public string Description => _description;

        public int Points => _points;

        /// <summary>
        /// Records progress on this goal and returns the number of points earned.
        /// Returns 0 if the goal is already complete. May return a negative
        /// number for goals that cost points (e.g. NegativeGoal).
        /// </summary>
        public abstract int RecordEvent();

        /// <summary>Whether this goal has been completed.</summary>
        public abstract bool IsComplete();

        /// <summary>
        /// The line shown in the goal list, e.g. "[X] Read scriptures (daily)".
        /// Derived classes override this to show extra progress information.
        /// </summary>
        public virtual string GetDetailsString()
        {
            string mark = IsComplete() ? "[X]" : "[ ]";
            return $"{mark} {_shortName} ({_description})";
        }

        /// <summary>A pipe-delimited string used to save and reload this goal.</summary>
        public abstract string GetStringRepresentation();

        /// <summary>
        /// Reads the first field of a saved line to figure out which concrete
        /// goal type to build, then constructs it from the remaining fields.
        /// This is the polymorphism side of loading: the right subclass is
        /// created here and returned as a base-class reference.
        /// </summary>
        public static Goal CreateGoalFromString(string line)
        {
            string[] parts = line.Split('|');
            switch (parts[0])
            {
                case "SimpleGoal":
                    return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
                case "EternalGoal":
                    return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                case "ChecklistGoal":
                    // Format: type|name|description|points|bonus|target|amountCompleted
                    return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                        int.Parse(parts[5]), int.Parse(parts[4]), int.Parse(parts[6]));
                case "NegativeGoal":
                    return new NegativeGoal(parts[1], parts[2], int.Parse(parts[3]));
                case "ProgressGoal":
                    return new ProgressGoal(parts[1], parts[2], int.Parse(parts[3]),
                        int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
                default:
                    throw new ArgumentException($"Unknown goal type: {parts[0]}");
            }
        }
    }
}
