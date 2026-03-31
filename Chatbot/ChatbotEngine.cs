using System;
using System.Threading.Tasks;
using CybersecurityChatbot.Audio;
using CybersecurityChatbot.Models;
using CybersecurityChatbot.UI;

namespace CybersecurityChatbot.Chatbot
{
    
    /// Main chatbot engine that orchestrates all components
    
    public class ChatbotEngine
    {
        private readonly ConsoleUI _ui;
        private readonly ResponseHandler _responseHandler;
        private readonly InputValidator _inputValidator;
        private readonly AudioPlayer _audioPlayer;
        private UserSession? _currentSession;

        public ChatbotEngine()
        {
            _ui = new ConsoleUI();
            _responseHandler = new ResponseHandler();
            _inputValidator = new InputValidator();
            _audioPlayer = new AudioPlayer();
        }

       
        /// Start the chatbot application
        
        public async Task StartAsync()
        {
            try
            {
                // Display initial header
                _ui.DisplayHeader("CYBERSECURITY AWARENESS BOT");

                
                string audioPath = _audioPlayer.GetRecommendedAudioPath();
                if (_audioPlayer.AudioFileExists(audioPath))
                {
                    _audioPlayer.PlayGreeting(audioPath);
                    // Give audio time to start playing
                    await Task.Delay(500);
                }
                else
                {
                    _ui.DisplayInfo("Voice greeting unavailable. Running in text-only mode.");
                    _ui.DisplayInfo("To add voice greeting, place a welcome.wav file in the application folder.");

                    // Simulate voice greeting with text
                    await _ui.TypeTextAsync("🎙️ [Voice Greeting Simulation] Hello! Welcome to the Cybersecurity Awareness Bot!", 30);
                    await _ui.TypeTextAsync("🎙️ I'm here to help you stay safe online.", 30);
                }

                // Get user name with validation
                await GetUserNameAsync();

                // Display personalized welcome
                await DisplayWelcomeAsync();

                // Main conversation loop
                await StartConversationAsync();

                // End session
                await EndSessionAsync();
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"An error occurred: {ex.Message}");
            }
        }

        
        /// Gets and validates user name
       
        private async Task GetUserNameAsync()
        {
            string? userName = null;
            bool isValid = false;

            while (!isValid)
            {
                userName = _ui.GetUserInput("What's your name?");

                if (_inputValidator.IsValidInput(userName) && _inputValidator.IsValidName(userName!))
                {
                    isValid = true;
                    _currentSession = new UserSession { UserName = userName! };
                    _ui.DisplaySuccess($"Nice to meet you, {userName}! Welcome to the Cybersecurity Awareness Bot.");
                }
                else
                {
                    _ui.DisplayError("Please enter a valid name (letters, spaces, hyphens, and apostrophes only).");
                }
            }

            await Task.Delay(1000);
        }

        
        /// Displays personalized welcome message
        
        private async Task DisplayWelcomeAsync()
        {
            _ui.DisplaySeparator();
            await _ui.TypeTextAsync(_responseHandler.GetGreeting(_currentSession!.UserName), 40);
            _ui.DisplaySeparator();

            // Show help message
            _ui.DisplayMessage(_responseHandler.GetHelpMessage(), "bot");
        }

        
        /// Main conversation loop
       
        private async Task StartConversationAsync()
        {
            bool isRunning = true;

            while (isRunning)
            {
                var userInput = _ui.GetUserInput("Type your message");

                // Validate input
                if (!_inputValidator.IsValidInput(userInput))
                {
                    _ui.DisplayError("Please enter a message. I'm here to help!");
                    continue;
                }

                var sanitizedInput = _inputValidator.SanitizeInput(userInput);

                // Check for exit commands
                if (sanitizedInput.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    sanitizedInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    isRunning = false;
                    break;
                }

                // Check for help
                if (sanitizedInput.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    _ui.DisplayMessage(_responseHandler.GetHelpMessage(), "bot");
                    continue;
                }

                // Track topics discussed
                _currentSession!.AddTopic(sanitizedInput);

                // Get and display response
                var response = await _responseHandler.GetResponseAsync(sanitizedInput);
                _ui.DisplayMessage(response, "bot");
            }
        }

        
        /// Ends the session 
        
        private async Task EndSessionAsync()
        {
            _ui.DisplaySeparator();
            _ui.DisplayMessage(_responseHandler.GetFarewell(_currentSession!.UserName), "bot");
            _currentSession.DisplaySessionSummary();
            _ui.DisplaySeparator();

            _ui.DisplaySuccess("Thank you for using the Cybersecurity Awareness Bot! Stay safe online!");

            // Stop any playing audio
            _audioPlayer.StopGreeting();
        }
    }
}
