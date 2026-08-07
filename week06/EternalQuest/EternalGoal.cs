namespace EternalQuest
{
    /// <summary>
    /// A goal that is never finished, e.g. "Study the scriptures" (100 points
    /// each time). Recording it always awards points and it always stays open.
    /// </summary>
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override int RecordEvent() => Points;

        public override bool IsComplete() => false;

        public override string GetStringRepresentation()
        {
            return $"EternalGoal|{Name}|{Description}|{Points}";
        }
    }
}
