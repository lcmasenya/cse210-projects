using System;
using System.Collections.Generic;
using System.IO;

// Base abstract class
public abstract class Goal
{
    protected string name;
    protected string description;
    protected int points;
    protected bool isComplete;

    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        this.isComplete = false;
    }

    public string Name => name;
    public bool IsComplete => isComplete;

    public abstract int RecordEvent();
    public abstract string DisplayStatus();
    public abstract string SaveString();
}

// Simple one-time goal
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        isComplete = true;
        return points;
    }

    public override string DisplayStatus()
    {
        return $"{(isComplete ? "[X]" : "[ ]")} {name} ({description})";
    }

    public override string SaveString()
    {
        return $"SimpleGoal|{name}|{description}|{points}|{isComplete}";
    }
}

// Eternal goal (never complete)
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        return points;
    }

    public override string DisplayStatus()
    {
        return $"[∞] {name} ({description})";
    }

    public override string SaveString()
    {
        return $"EternalGoal|{name}|{description}|{points}";
    }
}

// Checklist goal
public class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        currentCount++;
        if (currentCount >= targetCount)
        {
            isComplete = true;
            return points + bonusPoints;
        }
        return points;
    }

    public override string DisplayStatus()
    {
        return $"{(isComplete ? "[X]" : "[ ]")} {name} ({description}) -- Completed {currentCount}/{targetCount}";
    }

    public override string SaveString()
    {
        return $"ChecklistGoal|{name}|{description}|{points}|{targetCount}|{bonusPoints}|{currentCount}|{isComplete}";
    }
}

// Manager class
public class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;
    private int level = 1;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(int index)
    {
        if (index >= 0 && index < goals.Count)
        {
            int earned = goals[index].RecordEvent();
            score += earned;
            Console.WriteLine($"You earned {earned} points!");

            // Leveling system
            if (score >= level * 1000)
            {
                level++;
                Console.WriteLine($"🎉 Congratulations! You leveled up to Level {level}!");
            }

            // Badge system
            if (score >= 5000)
            {
                Console.WriteLine("🏆 Badge unlocked: Eternal Champion!");
            }
        }
    }

    public void ShowGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].DisplayStatus()}");
        }
    }

    public void ShowScore()
    {
        Console.WriteLine($"Total Score: {score} | Level: {level}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(score);
            outputFile.WriteLine(level);
            foreach (Goal g in goals)
            {
                outputFile.WriteLine(g.SaveString());
            }
        }
        Console.WriteLine("Goals saved successfully!");
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            score = int.Parse(lines[0]);
            level = int.Parse(lines[1]);

            goals.Clear();
            for (int i = 2; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                string type = parts[0];

                if (type == "SimpleGoal")
                {
                    SimpleGoal sg = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                    if (bool.Parse(parts[4])) sg.RecordEvent();
                    goals.Add(sg);
                }
                else if (type == "EternalGoal")
                {
                    goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                }
                else if (type == "ChecklistGoal")
                {
                    ChecklistGoal cg = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    for (int j = 0; j < int.Parse(parts[6]); j++) cg.RecordEvent();
                    goals.Add(cg);
                }
            }
            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No save file found.");
        }
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Eternal Quest Menu ---");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Choose goal type: 1=Simple, 2=Eternal, 3=Checklist");
                    string type = Console.ReadLine();
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());

                    if (type == "1")
                        manager.AddGoal(new SimpleGoal(name, desc, points));
                    else if (type == "2")
                        manager.AddGoal(new EternalGoal(name, desc, points));
                    else if (type == "3")
                    {
                        Console.Write("Enter target count: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Enter bonus points: ");
                        int bonus = int.Parse(Console.ReadLine());
                        manager.AddGoal(new ChecklistGoal(name, desc, points, target, bonus));
                    }
                    break;

                case "2":
                    manager.ShowGoals();
                    Console.Write("Enter goal number to record: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordEvent(index);
                    break;

                case "3":
                    manager.ShowGoals();
                    break;

                case "4":
                    manager.ShowScore();
                    break;

                case "5":
                    manager.SaveGoals("goals.txt");
                    break;

                case "6":
                    manager.LoadGoals("goals.txt");
                    break;

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}