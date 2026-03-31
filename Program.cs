using System;
using System.Threading.Tasks;
using CybersecurityChatbot.Chatbot;

namespace CybersecurityChatbot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.CursorVisible = true;

            try
            {
                var chatbot = new ChatbotEngine();
                await chatbot.StartAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nFatal error: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}