using System;

namespace EternalQuest
{
    /// =====================================================================
    ///  ETERNAL QUEST — a gamified goal tracker
    /// =====================================================================
    ///  How this program exceeds the base requirements:
    ///
    ///  1. LEVELING SYSTEM (gamification)
    ///     Every 1000 points the player levels up with a fun title, from
    ///     "Novice" to "Ninja Unicorn" to "Eternal Hero", and the program
    ///     announces the level-up (see Quest.cs).
    ///
    ///  2. ACHIEVEMENTS / BADGES
    ///     The player earns badges for milestones such as recording their
    ///     first event, completing a goal, or becoming a "Quest Champion".
    ///     Badges are shown in a dedicated menu option and persist across
    ///     saves (see Quest.cs).
    ///
    ///  3. NEGATIVE GOALS (a new goal type)
    ///     A "bad habit" goal that costs points when you slip up, so
    ///     stopping a habit is also tracked as part of the quest
    ///     (see NegativeGoal.cs).
    ///
    ///  4. PROGRESS GOALS (a new goal type)
    ///     A large goal reached a little at a time, such as "Train for a
    ///     marathon". You record how much progress you made each session
    ///     and earn points proportional to it, with a bonus for reaching
    ///     the target (see ProgressGoal.cs).
    ///
    ///  Design: inheritance (Goal base + one class per goal type),
    ///  polymorphism (overridden RecordEvent/IsComplete/GetDetailsString
    ///  and a factory that rebuilds the right subclass on load), and
    ///  encapsulation (private fields, public properties/methods).
    /// =====================================================================
    class Program
    {
        static void Main(string[] args)
        {
            Quest quest = new Quest();
            bool running = true;

            Console.WriteLine("==============================================");
            Console.WriteLine("  ETERNAL QUEST — a goal-tracking adventure");
            Console.WriteLine("==============================================");

            while (running)
            {
                quest.ShowStatus();
                Console.WriteLine();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Create New Goal");
                Console.WriteLine("  2. List Goals");
                Console.WriteLine("  3. Save Goals");
                Console.WriteLine("  4. Load Goals");
                Console.WriteLine("  5. Record Event");
                Console.WriteLine("  6. Show Achievements");
                Console.WriteLine("  7. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal(quest);
                        break;
                    case "2":
                        ListGoals(quest);
                        break;
                    case "3":
                        quest.Save();
                        break;
                    case "4":
                        quest.Load();
                        break;
                    case "5":
                        RecordEvent(quest);
                        break;
                    case "6":
                        quest.ShowAchievements();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please pick a number from the menu.");
                        break;
                }
            }

            Console.WriteLine("May your quest ever continue. Goodbye!");
        }

        static void CreateGoal(Quest quest)
        {
            Console.WriteLine("The types of goals are:");
            Console.WriteLine("  1. Simple Goal    — complete it once (e.g. run a marathon)");
            Console.WriteLine("  2. Eternal Goal   — never complete, repeatable (e.g. study scriptures)");
            Console.WriteLine("  3. Checklist Goal — complete it N times for a bonus (e.g. temple 10x)");
            Console.WriteLine("  4. Progress Goal  — work toward a big goal a little at a time");
            Console.WriteLine("  5. Negative Goal  — a bad habit that costs points when you slip");
            Console.Write("Which type of goal would you like to create? ");
            string type = ReadLine();

            Console.Write("What is the name of your goal? ");
            string name = ReadLine();
            Console.Write("What is a short description of it? ");
            string description = ReadLine();
            Console.Write("How many points is it worth? ");
            int points = ReadInt("points");

            Goal goal;
            switch (type)
            {
                case "1":
                    goal = new SimpleGoal(name, description, points);
                    break;
                case "2":
                    goal = new EternalGoal(name, description, points);
                    break;
                case "3":
                    int target = ReadInt("how many times it must be completed");
                    int bonus = ReadInt("the bonus for finishing it");
                    goal = new ChecklistGoal(name, description, points, target, bonus);
                    break;
                case "4":
                    int progressTarget = ReadInt("the total amount of progress needed");
                    int progressBonus = ReadInt("the bonus for reaching the target");
                    goal = new ProgressGoal(name, description, points, progressTarget, progressBonus);
                    break;
                case "5":
                    goal = new NegativeGoal(name, description, points);
                    break;
                default:
                    Console.WriteLine("That is not a valid goal type.");
                    return;
            }

            quest.AddGoal(goal);
        }

        static void ListGoals(Quest quest)
        {
            if (quest.Goals.Count == 0)
            {
                Console.WriteLine("You have no goals yet — create one first!");
                return;
            }

            Console.WriteLine("The goals are:");
            int completed = 0;
            for (int i = 0; i < quest.Goals.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {quest.Goals[i].GetDetailsString()}");
                if (quest.Goals[i].IsComplete())
                {
                    completed++;
                }
            }
            Console.WriteLine($"Completed: {completed}/{quest.Goals.Count}");
        }

        static void RecordEvent(Quest quest)
        {
            if (quest.Goals.Count == 0)
            {
                Console.WriteLine("You have no goals yet — create one first!");
                return;
            }

            Console.WriteLine("Which goal did you accomplish?");
            for (int i = 0; i < quest.Goals.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {quest.Goals[i].GetDetailsString()}");
            }
            Console.Write("Enter the number: ");
            int index = ReadInt("the number of the goal") - 1;

            if (!quest.RecordEvent(index))
            {
                Console.WriteLine("That goal number is not valid.");
            }
        }

        /// <summary>Reads a line of input, treating end-of-stream as empty text.</summary>
        static string ReadLine() => Console.ReadLine() ?? string.Empty;

        /// <summary>Reads an integer from the user, re-prompting until valid input.</summary>
        static int ReadInt(string label)
        {
            int value;
            Console.Write($"Enter {label}: ");
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write($"That is not a number. Enter {label}: ");
            }
            return value;
        }
    }
}
