using System;

namespace EternalQuest
{
    /// <summary>
    /// A creative addition: a large, long-term goal that is reached a little at
    /// a time, e.g. "Train for a marathon" (26.2 miles). Each time you record an
    /// event you tell the program how much progress you made, and you earn points
    /// proportional to that progress. Reaching the target pays the rest of the
    /// points plus a bonus. This models goals that are too big for one session.
    /// </summary>
    public class ProgressGoal : Goal
    {
        private int _target;   // total amount of progress needed (e.g. 26 miles)
        private int _progress; // amount of progress made so far
        private int _bonus;    // extra points awarded when the target is reached

        public ProgressGoal(string name, string description, int points, int target, int bonus)
            : this(name, description, points, target, bonus, 0)
        {
        }

        /// <summary>Overload used when loading a saved goal.</summary>
        public ProgressGoal(string name, string description, int points, int target, int bonus, int progress)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _progress = Math.Clamp(progress, 0, _target);
        }

        public int Progress => _progress;

        public int Target => _target;

        public override int RecordEvent()
        {
            if (IsComplete())
            {
                return 0; // already reached the target
            }

            Console.Write($"How much progress did you make toward \"{Name}\"? (up to {_target - _progress} remaining): ");
            string input = Console.ReadLine() ?? string.Empty;
            int added = int.TryParse(input, out int parsed) ? parsed : 0;
            added = Math.Clamp(added, 0, _target - _progress);
            _progress += added;

            // Points are awarded proportionally to the fraction of the goal done
            // this session, so small steps still earn something.
            int earned = (int)Math.Round((double)Points * added / _target);
            if (IsComplete())
            {
                earned += _bonus;
            }
            return earned;
        }

        public override bool IsComplete() => _progress >= _target;

        public override string GetDetailsString()
        {
            string mark = IsComplete() ? "[X]" : "[ ]";
            return $"{mark} {Name} ({Description}) — Progress: {_progress}/{_target}";
        }

        public override string GetStringRepresentation()
        {
            return $"ProgressGoal|{Name}|{Description}|{Points}|{_target}|{_bonus}|{_progress}";
        }
    }
}
