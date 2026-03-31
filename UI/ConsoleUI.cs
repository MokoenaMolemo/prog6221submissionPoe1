using System;
using System.Threading.Tasks;

namespace CybersecurityChatbot.UI
{
    /// <summary>
    /// Handles all console display formatting and effects
    /// </summary>
    public class ConsoleUI
    {
        private readonly ConsoleColor _defaultForeground;
        private readonly ConsoleColor _defaultBackground;

        public ConsoleUI()
        {
            _defaultForeground = Console.ForegroundColor;
            _defaultBackground = Console.BackgroundColor;
        }

        /// <summary>
        /// Displays text with a typing effect
        /// </summary>
        public async Task TypeTextAsync(string text, int delayMs = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                await Task.Delay(delayMs);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays colored text
        /// </summary>
        public void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = _defaultForeground;
        }

        /// <summary>
        /// Displays a formatted message with border
        /// </summary>
        public void DisplayMessage(string message, string type = "bot")
        {
            var color = type == "bot" ? ConsoleColor.Cyan : ConsoleColor.Yellow;
            var prefix = type == "bot" ? "🤖 Bot: " : "👤 You: ";

            Console.WriteLine();
            WriteColored(prefix, color);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays a section header
        /// </summary>
        public void DisplayHeader(string title)
        {
            Console.Clear();
            Console.WriteLine(AsciiArt.GetLogo());
            Console.WriteLine();
            WriteColored($"=== {title} ===\n", ConsoleColor.Green);
            Console.WriteLine(AsciiArt.GetDivider());
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a separator line
        /// </summary>
        public void DisplaySeparator()
        {
            Console.WriteLine();
            WriteColored(AsciiArt.GetDivider(), ConsoleColor.DarkGray);
            Console.WriteLine();
        }

        /// <summary>
        /// Prompts user for input with formatting
        /// </summary>
        public string GetUserInput(string prompt)
        {
            WriteColored($"\n{prompt} ", ConsoleColor.Green);
            WriteColored("> ", ConsoleColor.Magenta);
            var input = Console.ReadLine() ?? string.Empty;
            return input;
        }

        /// <summary>
        /// Displays error message
        /// </summary>
        public void DisplayError(string message)
        {
            WriteColored($"\n⚠️ Error: {message}\n", ConsoleColor.Red);
        }

        /// <summary>
        /// Displays success message
        /// </summary>
        public void DisplaySuccess(string message)
        {
            WriteColored($"\n✅ {message}\n", ConsoleColor.Green);
        }

        /// <summary>
        /// Displays info message
        /// </summary>
        public void DisplayInfo(string message)
        {
            WriteColored($"\nℹ️ {message}\n", ConsoleColor.Blue);
        }
    }
}