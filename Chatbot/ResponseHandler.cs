
using CybersecurityChatbot.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CybersecurityChatbot.Chatbot
{
    /// <summary>
    /// Handles generating responses to user queries
    /// </summary>
    public class ResponseHandler
    {
        private Dictionary<string, string> _responses;

        public ResponseHandler()
        {
            _responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // General responses
                { "how are you", "I'm functioning well, thank you for asking! Ready to help you stay cyber-safe! 🛡️" },
                { "what's your purpose", "I'm your Cybersecurity Awareness Assistant! I'm here to educate and help you stay safe from cyber threats like phishing, malware, and social engineering attacks." },
                { "what can i ask you about", "You can ask me about:\n   • Password safety and best practices\n   • How to identify phishing emails\n   • Recognizing suspicious links\n   • Safe browsing habits\n   • Social engineering awareness\n   • General cybersecurity tips" },
                
                // Password safety
                { "password", "🔐 Strong passwords are essential! Use:\n   • At least 12 characters\n   • Mix of uppercase, lowercase, numbers, and symbols\n   • Avoid common words or personal info\n   • Use a password manager\n   • Enable 2-factor authentication whenever possible!" },
                { "phishing", "🎣 Phishing alert! Watch out for:\n   • Urgent or threatening language\n   • Suspicious email addresses\n   • Spelling and grammar errors\n   • Requests for personal info\n   • Unexpected attachments\n   • Always verify before clicking!" },
                { "safe browsing", "🌐 Safe browsing tips:\n   • Look for 'https://' and padlock icon\n   • Don't click suspicious pop-ups\n   • Keep browser updated\n   • Use ad-blockers\n   • Clear cookies regularly\n   • Avoid public WiFi for sensitive transactions" },
                { "social engineering", "🧠 Social engineering attacks manipulate human psychology:\n   • Never share passwords over phone/email\n   • Verify unexpected requests through official channels\n   • Be wary of creating urgency\n   • Trust your instincts - if it seems suspicious, it probably is!" },
                { "malware", "🦠 Malware protection:\n   • Keep antivirus software updated\n   • Don't download from untrusted sources\n   • Be careful with email attachments\n   • Regular system updates\n   • Backup important data regularly!" }
            };
        }

        /// <summary>
        /// Gets a response based on user input
        /// </summary>
        public async Task<string> GetResponseAsync(string userInput)
        {
            // Simulate typing effect with delay
            await Task.Delay(500);

            var sanitizedInput = userInput.ToLower().Trim();

            // Check for matches in responses
            foreach (var keyword in _responses.Keys)
            {
                if (sanitizedInput.Contains(keyword))
                {
                    return _responses[keyword];
                }
            }

            // Default response for unrecognized queries
            return "I'm not sure I understand that question. Could you ask me about password safety, phishing, safe browsing, social engineering, or malware? Or try asking 'what can I ask you about' for more options! 😊";
        }

        /// <summary>
        /// Gets a personalized greeting
        /// </summary>
        public string GetGreeting(string userName)
        {
            return $"Hello {userName}! 👋\n\nI'm your Cybersecurity Awareness Assistant. I'm here to help you stay safe online by teaching you about cyber threats and how to avoid them.\n\nWhat would you like to learn about today?";
        }

        /// <summary>
        /// Gets a farewell message
        /// </summary>
        public string GetFarewell(string userName)
        {
            var messages = new[]
            {
                $"Stay safe out there, {userName}! Remember, cybersecurity is everyone's responsibility! 🛡️",
                $"Thanks for chatting, {userName}! Keep practicing safe online habits! 🔒",
                $"Goodbye {userName}! Stay vigilant and stay secure! 💪"
            };

            return messages[new Random().Next(messages.Length)];
        }

        /// <summary>
        /// Gets help message
        /// </summary>
        public string GetHelpMessage()
        {
            return $"\n{AsciiArt.GetDivider()}\n" +
                   "💡 TIPS FOR USING THIS CHATBOT:\n" +
                   "   • Ask about specific topics: 'password', 'phishing', 'safe browsing'\n" +
                   "   • Ask general questions: 'how are you', 'what's your purpose'\n" +
                   "   • Type 'help' to see this message again\n" +
                   "   • Type 'exit' or 'quit' to end the conversation\n" +
                   $"{AsciiArt.GetDivider()}";
        }
    }
}