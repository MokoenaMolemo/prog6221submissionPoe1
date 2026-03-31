using System;

namespace CybersecurityChatbot.UI
{
    /// <summary>
    /// Contains ASCII art for the chatbot's visual elements
    /// </summary>
    public static class AsciiArt
    {
        public static string GetLogo()
        {
            return @"
    ╔══════════════════════════════════════════════════════════════════╗
    ║                       CYBERSECURITY BOT                          ║
    ║                    ╔══════════════════════════╗                  ║
    ║                    ║  [=====]   [=====]      ║                  ║
    ║                    ║   |||       |||         ║                  ║
    ║                    ║   |||   █   |||         ║                  ║
    ║                    ║   |||   █   |||         ║                  ║
    ║                    ║   |||   █   |||         ║                  ║
    ║                    ║   |||       |||         ║                  ║
    ║                    ║   [===]     [===]       ║                  ║
    ║                    ║    █████████████        ║                  ║
    ║                    ║       SHIELD            ║                  ║
    ║                    ╚══════════════════════════╝                  ║
    ║                      Stay Safe Online!                           ║
    ╚══════════════════════════════════════════════════════════════════╝";
        }

        public static string GetDivider()
        {
            return "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
        }

        public static string GetSmallShield()
        {
            return "🛡️";
        }
    }
}