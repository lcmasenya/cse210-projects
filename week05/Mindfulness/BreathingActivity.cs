// BreathingActivity.cs
using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity() 
        : base("Breathing", "This activity will help you relax by guiding you to breathe in and out slowly.") { }

    public override void Run()
    {
        StartActivity();
        int duration = GetDuration();
        int elapsed = 0;

        while (elapsed < duration)
        {
            Console.WriteLine("Breathe in...");
            PauseWithSpinner(3);
            elapsed += 3;

            Console.WriteLine("Breathe out...");
            PauseWithSpinner(3);
            elapsed += 3;
        }

        EndActivity();
    }
}
