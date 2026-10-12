// ListingActivity.cs
using System;
using System.Collections.Generic;
using System.Threading;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing", "This activity helps you reflect on the good things in your life by listing items.") { }

    public override void Run()
    {
        StartActivity();
        Random rand = new Random();
        Console.WriteLine(_prompts[rand.Next(_prompts.Count)]);
        PauseWithSpinner(3);

        int duration = GetDuration();
        int elapsed = 0;
        List<string> items = new List<string>();

        while (elapsed < duration)
        {
            Console.Write("Enter an item: ");
            items.Add(Console.ReadLine());
            elapsed += 5; // assume ~5 seconds per entry
        }

        Console.WriteLine($"You listed {items.Count} items!");
        EndActivity();
    }
}
