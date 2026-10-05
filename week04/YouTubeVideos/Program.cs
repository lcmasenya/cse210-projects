using System;
using System.Collections.Generic;

public class Comment
{
    private string _name;
    private string _text;

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    public string GetComment()
    {
        return $"{_name}: {_text}";
    }
}

public class Video
{
    private string _title;
    private string _author;
    private int _length; // in seconds
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");

        foreach (Comment comment in _comments)
        {
            Console.WriteLine($"   {comment.GetComment()}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Healthy Living Tips", "Lebogang", 300);
        Video video2 = new Video("Chess Strategies", "Alex", 420);
        Video video3 = new Video("Swimming for Beginners", "Sam", 250);
        Video video4 = new Video("Web Development Basics", "BYU-Idaho", 600);

        // Add comments
        video1.AddComment(new Comment("Thabo", "Great advice, thanks!"));
        video1.AddComment(new Comment("Maria", "I’ll try these tips."));
        video1.AddComment(new Comment("John", "Very helpful video."));

        video2.AddComment(new Comment("Chris", "I love chess!"));
        video2.AddComment(new Comment("Nina", "This helped me win a game."));
        video2.AddComment(new Comment("David", "Clear explanations!"));

        video3.AddComment(new Comment("Lerato", "Swimming is fun!"));
        video3.AddComment(new Comment("Peter", "Good beginner guide."));
        video3.AddComment(new Comment("Sarah", "I learned a lot."));

        video4.AddComment(new Comment("Michael", "This clarified HTML for me."));
        video4.AddComment(new Comment("Emily", "CSS examples were great."));
        video4.AddComment(new Comment("Jacob", "Looking forward to more lessons."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3, video4 };

        // Display info for each video
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
