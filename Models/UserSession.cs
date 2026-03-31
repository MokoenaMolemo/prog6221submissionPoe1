using System;
using System.Collections.Generic;

namespace CybersecurityChatbot.Models
{
    /// <summary>
    /// Stores user session information
    /// </summary>
    public class UserSession
    {
        public string UserName { get; set; } = string.Empty;
        public DateTime SessionStart { get; set; }
        public List<string> TopicsDiscussed { get; set; } = new List<string>();

        public UserSession()
        {
            SessionStart = DateTime.Now;
        }

        public void AddTopic(string topic)
        {
            if (!TopicsDiscussed.Contains(topic))
            {
                TopicsDiscussed.Add(topic);
            }
        }

        public void DisplaySessionSummary()
        {
            Console.WriteLine($"\n📊 Session Summary:");
            Console.WriteLine($"   User: {UserName}");
            Console.WriteLine($"   Duration: {(DateTime.Now - SessionStart).TotalMinutes:F1} minutes");
            Console.WriteLine($"   Topics covered: {(TopicsDiscussed.Count > 0 ? string.Join(", ", TopicsDiscussed) : "None")}");
        }
    }
}
