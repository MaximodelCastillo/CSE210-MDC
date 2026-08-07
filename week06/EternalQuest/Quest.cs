using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    /// <summary>
    /// The quest itself: it owns the player's list of goals and their score,
    /// applies points when events are recorded, handles leveling up and
    /// achievements (the gamification layer), and saves/loads everything to a
    /// single file.
    /// </summary>
    public class Quest
    {
        private const string SaveFile = "quest.txt";

        // Every 1000 points earns a new level, with a fun title at each one.
        private static readonly string[] LevelTitles =
        {
            "Novice", "Seeker", "Apprentice", "Adventurer", "Warrior",
            "Knight", "Champion", "Ninja Unicorn", "Dragon Slayer",
            "Legendary", "Eternal Hero"
        };

        private readonly List<Goal> _goals = new List<Goal>();
        private readonly HashSet<string> _achievements = new HashSet<string>();
        private int _score;
        private int _totalEarned; // lifetime points earned (used for achievements)
        private int _lastAnnouncedLevel = 1; // players start at Level 1

        public int Score => _score;

        public IReadOnlyList<Goal> Goals => _goals;

        public int Level => _score / 1000 + 1;

        public string LevelTitle
        {
            get
            {
                int index = Math.Min(Level - 1, LevelTitles.Length - 1);
                return LevelTitles[index];
            }
        }

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
            Unlock("Goal Setter", _goals.Count >= 1);
            Console.WriteLine($"Goal \"{goal.Name}\" created!");
        }

        /// <summary>
        /// Records an event on the goal at the given index, applies the earned
        /// points to the score, and reports level-ups and achievements. The
        /// call to goal.RecordEvent() is polymorphism: the same base method
        /// behaves differently for each concrete goal type.
        /// </summary>
        public bool RecordEvent(int index)
        {
            if (index < 0 || index >= _goals.Count)
            {
                return false;
            }

            Goal goal = _goals[index];
            int earned = goal.RecordEvent();

            if (earned == 0)
            {
                Console.WriteLine(goal.IsComplete()
                    ? "That goal is already complete — no extra points for it."
                    : "No points were awarded this time.");
                return true;
            }

            _score += earned;
            if (earned > 0)
            {
                _totalEarned += earned;
            }

            if (earned > 0)
            {
                Console.WriteLine($"You earned {earned} points! (+{earned})");
            }
            else
            {
                Console.WriteLine($"Ouch! That bad habit cost you {-earned} points. ({earned})");
            }

            CheckLevelUp();
            CheckAchievements();
            return true;
        }

        public void ShowStatus()
        {
            Console.WriteLine($"=== ETERNAL QUEST — Score: {_score}  |  Level {Level}: {LevelTitle} ===");
        }

        public void ShowAchievements()
        {
            Console.WriteLine("Achievements:");
            if (_achievements.Count == 0)
            {
                Console.WriteLine("  (none yet — keep working on your goals!)");
                return;
            }
            foreach (string achievement in _achievements)
            {
                Console.WriteLine($"  🏆 {achievement}");
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(SaveFile))
            {
                writer.WriteLine($"{_score}|{_totalEarned}");
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine($"Goals and score saved to {SaveFile}.");
        }

        public void Load()
        {
            if (!File.Exists(SaveFile))
            {
                Console.WriteLine($"No save file found ({SaveFile}). Nothing loaded.");
                return;
            }

            string[] lines = File.ReadAllLines(SaveFile);
            if (lines.Length == 0)
            {
                Console.WriteLine("The save file is empty. Nothing loaded.");
                return;
            }

            string[] header = lines[0].Split('|');
            _score = int.Parse(header[0]);
            _totalEarned = int.Parse(header[1]);

            _goals.Clear();
            _achievements.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }
                _goals.Add(Goal.CreateGoalFromString(lines[i]));
            }

            _lastAnnouncedLevel = Level;
            CheckAchievements();
            Console.WriteLine($"Loaded {_goals.Count} goals and a score of {_score} from {SaveFile}.");
        }

        private void CheckLevelUp()
        {
            if (Level > _lastAnnouncedLevel)
            {
                _lastAnnouncedLevel = Level;
                Console.WriteLine($"⭐ LEVEL UP! You are now Level {Level}: {LevelTitle}! ⭐");
            }
        }

        private void CheckAchievements()
        {
            Unlock("First Step", _totalEarned >= 1);
            Unlock("Completionist", _goals.Exists(g => g.IsComplete()));
            Unlock("Point Collector", _totalEarned >= 1000);
            Unlock("Quest Champion", _score >= 5000);
            Unlock("Bad Habit Breaker", _goals.Exists(g => g is NegativeGoal));
            Unlock("Marathoner", _goals.Exists(g => g is ProgressGoal));
        }

        private void Unlock(string name, bool condition)
        {
            if (condition && _achievements.Add(name))
            {
                Console.WriteLine($"🏆 Achievement unlocked: {name}!");
            }
        }
    }
}
