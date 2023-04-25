using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;

namespace C__Spotify
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Spotify Applicatie | Sil Theunissen");

            // Example tracks:  
            MusicTrack track1 = new MusicTrack("Track1", "Artist1", 10);
            MusicTrack track2 = new MusicTrack("Track2", "Artist2", 4);
            MusicTrack track3 = new MusicTrack("Track3", "Artist3", 5);
            MusicTrack track4 = new MusicTrack("Track4", "Artist4", 8);
            MusicTrack track5 = new MusicTrack("Track5", "Artist5", 180);
            MusicTrack track6 = new MusicTrack("Track6", "Artist6", 260);
            MusicTrack track7 = new MusicTrack("Track7", "Artist7", 210);
            MusicTrack track8 = new MusicTrack("Track8", "Artist8", 240);
            MusicTrack track9 = new MusicTrack("Track9", "Artist9", 190);
            MusicTrack track10 = new MusicTrack("Track10", "Artist10", 230);

            // Write menu
            MainMenu.Menu();
        }
    }

    
}
