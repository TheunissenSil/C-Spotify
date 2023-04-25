using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace C__Spotify
{
    public class MusicTrack
    {
        private static List<MusicTrack> tracks = new List<MusicTrack>();

        public string Title { get; set; }
        public string Artist { get; set; }
        public int LengthInSeconds { get; set; }

        // Constructor
        public MusicTrack(string title, string artist, int lengthInSeconds)
        {
            Title = title;
            Artist = artist;
            LengthInSeconds = lengthInSeconds;
            tracks.Add(this);
        }

        // Findtrack function
        public static MusicTrack FindTrack(string name)
        {
            foreach (MusicTrack track in tracks)
            {
                if (track.Title == name)
                {
                    return track;
                }
            }

            return null;
        }

        public void PlayTrack(bool isPlaylist)
        {
            Console.Clear();
            Console.WriteLine("Pause = SPACEBAR");
            Console.WriteLine("Repeat = R");
            Console.WriteLine("Exit = Backspace");
            Console.WriteLine();
            Console.WriteLine($"Now playing '{Title}' by {Artist}");

            int duration = LengthInSeconds;
            int progressBarWidth = 20;
            bool repeat = false;

            while (true)
            {
                for (int i = 0; i <= duration; i++)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);

                    Console.Write("[");
                    int pos = 1;
                    for (int j = 0; j < progressBarWidth; j++)
                    {
                        if (j < (int)((float)i / duration * progressBarWidth))
                            Console.Write("=");
                        else
                            Console.Write(" ");
                    }
                    Console.Write("]");

                    Console.Write($" {i} / {duration} seconds");

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Spacebar)
                        {
                            Console.Write(" - paused");
                            Console.ReadKey(true);
                            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            PlaylistMenu.Menu();
                        }
                        else if (keyInfo.Key == ConsoleKey.R)
                        {
                            repeat = !repeat;
                            Console.Write($" - repeat {(repeat ? "on" : "off")}");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                    }

                    if (i == duration)
                    {
                        if (repeat)
                        {
                            Console.WriteLine();
                            i = -1;
                        }
                        else
                        {
                            Console.WriteLine();
                            if (isPlaylist)
                            {
                                return;
                            } else
                            {
                                PlaylistMenu.Menu();
                            }
                            
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}