using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace C__Spotify
{
    internal class Playlist
    {
        public static List<Playlist> playlists = new List<Playlist>();
        public string Name { get; set; }
        private List<MusicTrack> Tracks { get; set; }

        // Constructor
        public Playlist(string name)
        {
            Name = name;
            Tracks = new List<MusicTrack>();
        }

        // Add a track
        public void AddTrack()
        {
            Console.Clear();
            Console.Write("Enter the name of the track: ");
            string name = Console.ReadLine();

            // Check if the track already exists
            MusicTrack track = MusicTrack.FindTrack(name);
            if (track == null)
            {
                Console.WriteLine($"Track '{name}' does not exist.");
                Thread.Sleep(2000);
                ShowPlaylist();
            }

            // Add the track to the playlist
            Tracks.Add(track);
            Console.WriteLine($"Track '{track.Title}' added to playlist '{Name}'.");
            Thread.Sleep(2000);
            ShowPlaylist();
        }   

        // Remove a track
        public void RemoveTrack()
        {
            Console.Clear();
            Console.Write("Enter the name of the track to remove: ");
            string name = Console.ReadLine();

            MusicTrack trackToRemove = null;
            foreach (MusicTrack track in Tracks)
            {
                if (track.Title == name)
                {
                    trackToRemove = track;
                    break;
                }
            }

            if (trackToRemove != null)
            {
                Tracks.Remove(trackToRemove);
                Console.WriteLine($"Track '{name}' removed from playlist '{Name}'.");
                Thread.Sleep(2000);
                ShowPlaylist();
            }
            else
            {
                Console.WriteLine($"Track '{name}' not found in playlist '{Name}'.");
                Thread.Sleep(2000);
                ShowPlaylist();
            }
        }

        // Delete this playlist
        public static void DeletePlaylist(Playlist playlist)
        {
            Console.Clear();
            Console.Write($"Are you sure you want to delete playlist '{playlist.Name}'? (Y/N): ");
            string input = Console.ReadLine();

            if (input.ToUpper() == "Y")
            {
                playlists.Remove(playlist);
                Console.WriteLine($"Playlist '{playlist.Name}' deleted.");
                Thread.Sleep(2000);
                PlaylistMenu.Menu();
            }
            else
            {
                Console.WriteLine($"Playlist '{playlist.Name}' not deleted.");
                Thread.Sleep(2000);
                playlist.ShowPlaylist();
            }
        }

        // Create a new playlist
        public static void AddPlaylist()
        {
            Console.Clear();

            Console.Write("Enter the name of the playlist: ");
            string name = Console.ReadLine();

            // Check if a playlist with the same name already exists
            if (playlists.Any(p => p.Name == name))
            {
                Console.WriteLine($"A playlist with the name '{name}' already exists.");
                Thread.Sleep(2000);
            }
            else
            {
                Playlist newPlaylist = new Playlist(name);
                playlists.Add(newPlaylist);

                Console.WriteLine($"Playlist '{newPlaylist.Name}' added.");
                Thread.Sleep(2000);
            }

            PlaylistMenu.Menu();
        }

        // Shuffle the list
        static void ShuffleList<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Play the playlist
        private void playPlaylist(bool shuffle)
        {
            List<MusicTrack> originalPlaylist = new List<MusicTrack>(Tracks);

            if (shuffle)
            {
                ShuffleList(Tracks);
                Console.WriteLine("Playlist shuffled!");
            }

            for (int i = 0; i < Tracks.Count; i++)
            {
                Tracks[i].PlayTrack(true);

                if (i == Tracks.Count - 1)
                {
                    Console.WriteLine("End of playlist reached!");
                    Thread.Sleep(2000);

                    if (shuffle)
                    {
                        Tracks = originalPlaylist;
                        Console.WriteLine("Playlist restored to original order!");
                    }

                    ShowPlaylist();
                }
            }
        }

        // Menu
        public void ShowPlaylist()
        {
            List<Option> specificPlaylistMenu = new List<Option>
            {
                new Option("Add song", () => AddTrack()),
                new Option("Remove song", () => RemoveTrack()),
                new Option("Delete playlist", () => DeletePlaylist(this)),
                new Option("Play playlist", () => playPlaylist(false)),
                new Option("Shuffle playlist", () => playPlaylist(true)),
                new Option("", () => ShowPlaylist()),
            };

            // Add each track in the playlist as a menu option
            foreach (MusicTrack track in Tracks)
            {
                specificPlaylistMenu.Add(new Option(track.Title, () => track.PlayTrack(false)));

            }

            // Exit option
            specificPlaylistMenu.Add(new Option("", () => ShowPlaylist()));
            specificPlaylistMenu.Add(new Option("Exit", () => PlaylistMenu.Menu()));

            int index = 0;

            WriteMenu(specificPlaylistMenu, specificPlaylistMenu[index]);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < specificPlaylistMenu.Count)
                    {
                        index++;
                        WriteMenu(specificPlaylistMenu, specificPlaylistMenu[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(specificPlaylistMenu, specificPlaylistMenu[index]);
                    }
                }

                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    specificPlaylistMenu[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
        }
        // Menu WriteMenu
        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }
        // Menu option
        public class Option
        {
            public string Name { get; }
            public Action Selected { get; }

            public Option(string name, Action selected)
            {
                Name = name;
                Selected = selected;
            }
        }
    }
}