using NAudio.Wave;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Essenbee.Bot.Core.Utilities
{
    public static class Sfx
    {
        private static object _mutex = new object();

        public static string HeyEssenbee = @"Sounds.hey_essenbee.wav";

        public static void PlaySound(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.FullName.Split(',')[0] ?? string.Empty;
            var resourceName = $"{assemblyName}.{fileName}";
            var resource = assembly.GetManifestResourceStream(resourceName);

            if (resource != null)
            {
                lock (_mutex)
                {
                    using (var audioFile = new WaveFileReader(resource))
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
