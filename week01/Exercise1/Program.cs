using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise1 Project.");
        Console.WriteLine("What is your first name? ");
        String first = Console.ReadLine();
        Console.WriteLine("What is your last name? ");
        String last = Console.ReadLine();

        Console.WriteLine($"Your name is {last}, {first} {last}");

    }
}