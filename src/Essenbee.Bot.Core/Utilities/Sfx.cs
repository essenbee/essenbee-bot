using NAudio.Wave;
using System.IO;
using System.Threading;

namespace Essenbee.Bot.Core.Utilities
{
    public static class Sfx
    {
        private static object _mutex = new object();

        public static string HeyEssenbee = @"D:\sounds\hey_essenbee.wav";

        public static void PlaySound(string fileName)
        {
            if (File.Exists(fileName))
            {
                lock (_mutex)
                {
                    using (var audioFile = new AudioFileReader(fileName))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(500);
                        }
                    }
                }
            }
        }
    }
}
