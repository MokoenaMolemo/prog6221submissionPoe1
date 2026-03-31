using System;
using System.Text.RegularExpressions;

namespace CybersecurityChatbot.Chatbot
{
    /// <summary>
    /// Validates user input for the chatbot
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// Validates if the input is not empty or just whitespace
        /// </summary>
        public bool IsValidInput(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Validates if the name contains only valid characters
        /// </summary>
        public bool IsValidName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Allow letters, spaces, hyphens, and apostrophes
            return Regex.IsMatch(name, @"^[a-zA-Z\s\-']+$");
        }

        /// <summary>
        /// Sanitizes input to prevent injection-like issues
        /// </summary>
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Trim and remove extra spaces
            return Regex.Replace(input.Trim(), @"\s+", " ");
        }
    }
}
