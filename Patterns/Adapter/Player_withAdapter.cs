namespace Patterns.Adapter
{
    public class Player_withAdapter
    {
        static void Main(string[] args)
        {
            IAudioPlayer player = new UniversalPlayer();
            player.Play("Music", "wav");
            player.Stop();

            player = new MP3PlayerAdapter();
            player.Play("Song");
            player.Stop();
        }
    }

    public interface IAudioPlayer
    {
        void Play(string file, string format = null);
        void Pause();
        void Stop();
    }

    public class MP3Player
    {
        public void Play(string file)
        {
            Console.WriteLine($"Playing MP3:{file}");
        }

        public void Break()
        {
            Console.WriteLine("Playing MP3 stopped");
        }
    }

    public class UniversalPlayer : IAudioPlayer 
    {
        public void Play(string file, string format)
        {
            Console.WriteLine($"Playing {format}: {file}, duration X:XX");
        }

        public void Pause()
        {
            Console.WriteLine("Playing paused");
        }

        public void Stop()
        {
            Console.WriteLine("Playing stopped");
        }
    }

    public class MP3PlayerAdapter : IAudioPlayer
    {
        private MP3Player mp3player = new MP3Player();

        public void Play(string file, string format = null)
        {
            mp3player.Play(file);
        }

        public void Pause()
        {
            Console.WriteLine("Playing MP3 paused");
        }

        public void Stop()
        {
            mp3player.Break();
        }
    }
}
