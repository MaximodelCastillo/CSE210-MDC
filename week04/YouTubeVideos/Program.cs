using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Learn C# in 20 Minutes", "Code Academy", 1200);
        video1.AddComment(new Comment("Alice", "Very helpful tutorial!"));
        video1.AddComment(new Comment("Bob", "Thanks for explaining constructors."));
        video1.AddComment(new Comment("Charlie", "Easy to understand."));
        videos.Add(video1);

        Video video2 = new Video("Top 10 Travel Destinations", "Travel World", 850);
        video2.AddComment(new Comment("Emma", "I want to visit Japan!"));
        video2.AddComment(new Comment("David", "Amazing video."));
        video2.AddComment(new Comment("Sophia", "Great recommendations."));
        videos.Add(video2);

        Video video3 = new Video("Easy Chocolate Cake Recipe", "Kitchen Fun", 600);
        video3.AddComment(new Comment("James", "Turned out delicious!"));
        video3.AddComment(new Comment("Olivia", "Loved this recipe."));
        video3.AddComment(new Comment("Lucas", "Can't wait to try it."));
        videos.Add(video3);

        Video video4 = new Video("Morning Workout Routine", "Fitness Pro", 900);
        video4.AddComment(new Comment("Mia", "Perfect way to start the day."));
        video4.AddComment(new Comment("Noah", "Great exercises."));
        video4.AddComment(new Comment("Ethan", "Feeling motivated now!"));
        videos.Add(video4);

        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
