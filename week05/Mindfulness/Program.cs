using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base class for activities
    abstract class Activity
    {
        protected string Name;
        protected string Description;
        protected int Duration; // in seconds

        public void RunActivity()
        {
            ShowStartMessage();
            PromptDuration();
            ShowPrepare();
            PerformActivity();
            ShowFinish();
        }

        protected virtual void ShowStartMessage()
        {
            Console.WriteLine($"Starting {Name}...");
            Console.WriteLine(Description);
        }

        protected void PromptDuration()
        {
            Console.Write("Enter the duration of the activity in seconds: ");
            Duration = int.Parse(Console.ReadLine());
        }

        protected void ShowPrepare()
        {
            Console.WriteLine("Prepare to begin...");
            ShowSpinner(3);
        }

        protected virtual void PerformActivity()
        {
            // To be overridden
        }

        protected void ShowFinish()
        {
            Console.WriteLine("Good job! You have completed the activity.");
            ShowSpinner(3);
            Console.WriteLine($"You spent {Duration} seconds on {Name}.");
        }

        protected void ShowSpinner(int seconds)
        {
            string[] spinnerChars = { "|", "/", "-", "\\" };
            int endTime = Environment.TickCount + seconds * 1000;
            int i = 0;
            while (Environment.TickCount < endTime)
        {
            Console.Write(spinnerChars[i % spinnerChars.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
            i++;
        }
    }
    class BreathingActivity : Activity
    {
        public BreathingActivity()
        {
            Name = "Breathing Activity";
            Description = "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }

        protected override void PerformActivity()
        {
            int elapsed = 0;
            while (elapsed < Duration)
            {
                Console.WriteLine("Breathe in...");
                ShowCountdown(4); // example inhale
                Console.WriteLine("Breathe out...");
                ShowCountdown(6); // example exhale
                elapsed += 10; // approximate
            }
        }

        private void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    class ReflectionActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
        {
            Name = "Reflection Activity";
            Description = "This activity will help you reflect on times in your life when you have shown strength and resilience.";
        }

        protected override void PerformActivity()
        {
            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine("Reflect on this. You may begin...");
            ShowCountdown(3);

            int elapsed = 0;
            while (elapsed < Duration)
            {
                string question = questions[rand.Next(questions.Count)];
                Console.WriteLine($"Question: {question}");
                ShowSpinner(5);
                elapsed += 5; // approximate
            }
        }

        private void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    class ListingActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
        {
            Name = "Listing Activity";
            Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        protected override void PerformActivity()
        {
            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine("Get ready to list items...");
            ShowCountdown(3);
            List<string> items = new List<string>();
            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    items.Add(item);
                }
            }
            Console.WriteLine($"You listed {items.Count} items.");
        }

        private void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Choose an activity:");
                Console.WriteLine("1. Breathing");
                Console.WriteLine("2. Reflection");
                Console.WriteLine("3. Listing");
                Console.WriteLine("4. Exit");
                Console.Write("Selection: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var breathing = new BreathingActivity();
                        breathing.RunActivity();
                        break;
                    case "2":
                        var reflection = new ReflectionActivity();
                        reflection.RunActivity();
                        break;
                    case "3":
                        var listing = new ListingActivity();
                        listing.RunActivity();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
