using System;
using System.Collections.Generic;

// Base Activity class
public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    // Abstract methods to be overridden
    public abstract double GetDistance();   // km
    public abstract double GetSpeed();      // kph
    public abstract double GetPace();       // min per km

    // Shared summary method
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min): " +
               $"Distance {GetDistance():0.0} km, " +
               $"Speed {GetSpeed():0.0} kph, " +
               $"Pace {GetPace():0.00} min per km";
    }
}

// Running class
public class Running : Activity
{
    private double _distance; // km

    public Running(DateTime date, int minutes, double distanceKm)
        : base(date, minutes)
    {
        _distance = distanceKm;
    }

    public override double GetDistance() => _distance;
    public override double GetSpeed() => (GetDistance() / Minutes) * 60;
    public override double GetPace() => Minutes / GetDistance();
}

// Cycling class
public class Cycling : Activity
{
    private double _speed; // kph

    public Cycling(DateTime date, int minutes, double speedKph)
        : base(date, minutes)
    {
        _speed = speedKph;
    }

    public override double GetDistance() => (_speed * Minutes) / 60;
    public override double GetSpeed() => _speed;
    public override double GetPace() => 60 / _speed;
}

// Swimming class
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50) / 1000.0; // km
    public override double GetSpeed() => (GetDistance() / Minutes) * 60;
    public override double GetPace() => Minutes / GetDistance();
}

// Program execution
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 40, 20.0),
            new Swimming(new DateTime(2022, 11, 3), 25, 30)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
