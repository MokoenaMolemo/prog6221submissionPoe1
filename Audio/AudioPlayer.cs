using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;

namespace CybersecurityChatbot.Audio
{
    /// <summary>
    /// Handles audio playback functionality using NAudio (cross-platform)
    /// </summary>
    public class AudioPlayer : IDisposable
    {
        private WaveOutEvent? _waveOut;
        private AudioFileReader? _audioFile;
        private bool _disposed = false;
        private bool _isPlaying = false;

        /// <summary>
        /// Plays a welcome greeting from a WAV file
        /// </summary>
        /// <param name="filePath">Path to the WAV audio file</param>
        public void PlayGreeting(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // Stop any existing playback
                    StopGreeting();

                    // Initialize audio components
                    _audioFile = new AudioFileReader(filePath);
                    _waveOut = new WaveOutEvent();
                    _waveOut.Init(_audioFile);
                    _waveOut.PlaybackStopped += OnPlaybackStopped;
                    _waveOut.Play();
                    _isPlaying = true;

                    Console.WriteLine("🔊 Playing welcome message...");
                }
                else
                {
                    Console.WriteLine($"⚠️ Audio file not found at: {Path.GetFullPath(filePath)}");
                    Console.WriteLine("   Continuing with text-only mode.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Could not play audio: {ex.Message}");
                Console.WriteLine("   Continuing with text-only mode.");
            }
        }

        /// <summary>
        /// Plays audio and waits for completion
        /// </summary>
        public async Task PlayGreetingAsync(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (var audioFile = new AudioFileReader(filePath))
                    using (var waveOut = new WaveOutEvent())
                    {
                        waveOut.Init(audioFile);
                        waveOut.Play();

                        // Wait until playback is finished
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            await Task.Delay(100);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Could not play audio: {ex.Message}");
            }
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            _isPlaying = false;
        }

        /// <summary>
        /// Stops any currently playing audio
        /// </summary>
        public void StopGreeting()
        {
            try
            {
                if (_waveOut != null && _isPlaying)
                {
                    _waveOut.Stop();
                    _isPlaying = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error stopping audio: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if audio is currently playing
        /// </summary>
        public bool IsPlaying()
        {
            return _isPlaying;
        }

        /// <summary>
        /// Checks if audio file exists at the specified path
        /// </summary>
        public bool AudioFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Gets the recommended audio file path
        /// </summary>
        public string GetRecommendedAudioPath()
        {
            // Try multiple possible locations
            string[] possiblePaths =
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "welcome.wav"),
                Path.Combine(Directory.GetCurrentDirectory(), "welcome.wav"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", "welcome.wav"),
                "welcome.wav"
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "welcome.wav");
        }

        /// <summary>
        /// Creates a sample welcome.wav file (optional - for testing)
        /// </summary>
        public void CreateSampleAudioFile(string filePath)
        {
            try
            {
                // Note: Creating actual speech requires additional libraries
                // This is just a placeholder showing file creation
                Console.WriteLine($"Sample audio would be created at: {filePath}");
                Console.WriteLine("For actual voice greeting, please record your own welcome.wav file");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create sample audio: {ex.Message}");
            }
        }

        // Dispose pattern for proper cleanup
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    StopGreeting();
                    _waveOut?.Dispose();
                    _audioFile?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}