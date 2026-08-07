namespace EternalQuest
{
    /// <summary>
    /// A creative addition: a "bad habit" goal, e.g. "No junk food". Recording
    /// it costs points instead of giving them, which acts as a penalty for
    /// giving in. It is never complete — beating a habit is an ongoing battle.
    /// The points shown to the user are positive; RecordEvent returns them
    /// negated so the quest score goes down.
    /// </summary>
    public class NegativeGoal : Goal
    {
        public NegativeGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override int RecordEvent() => -Points;

        public override bool IsComplete() => false;

        public override string GetDetailsString()
        {
            return $"[!] {Name} ({Description}) — costs {Points} points";
        }

        public override string GetStringRepresentation()
        {
            return $"NegativeGoal|{Name}|{Description}|{Points}";
        }
    }
}
