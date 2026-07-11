using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine();
            Console.WriteLine("Journal Program");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Save");
            Console.WriteLine("4. Load");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                string prompt = promptGenerator.GetRandomPrompt();
                Console.WriteLine(prompt);

                Console.Write("> ");
                string answer = Console.ReadLine();

                Entry entry = new Entry();
                entry._date = DateTime.Now.ToShortDateString();
                entry._promptText = prompt;
                entry._entryText = answer;

                journal.AddEntry(entry);
            }
            else if (choice == "2")
            {
                journal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("Enter file name: ");
                string fileName = Console.ReadLine();

                journal.SaveToFile(fileName);
            }
            else if (choice == "4")
            {
                Console.Write("Enter file name: ");
                string fileName = Console.ReadLine();

                journal.LoadFromFile(fileName);
            }
            else if (choice == "5")
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Please choose a number from 1 to 5.");
            }
        }
    }
}

class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string fileName)
    {
        StreamWriter output = new StreamWriter(fileName);

        foreach (Entry entry in _entries)
        {
            output.WriteLine(entry._date + "|" + entry._promptText + "|" + entry._entryText);
        }

        output.Close();

        Console.WriteLine("Journal saved.");
    }

    public void LoadFromFile(string fileName)
    {
        _entries.Clear();

        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                Entry entry = new Entry();
                entry._date = parts[0];
                entry._promptText = parts[1];
                entry._entryText = parts[2];

                _entries.Add(entry);
            }

            Console.WriteLine("Journal loaded.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;

    public void Display()
    {
        Console.WriteLine("Date: " + _date);
        Console.WriteLine("Prompt: " + _promptText);
        Console.WriteLine("Entry: " + _entryText);
        Console.WriteLine();
    }
}

class PromptGenerator
{
    List<string> _prompts = new List<string>()
    {
        "What made you happy today?",
        "What did you learn today?",
        "Who helped you today?",
        "What are you thankful for today?",
        "What was the best part of your day?"
    };

    Random random = new Random();

    public string GetRandomPrompt()
    {
        int number = random.Next(_prompts.Count);
        return _prompts[number];
    }
}
