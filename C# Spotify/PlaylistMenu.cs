using System;
using System.Collections.Generic;

namespace C__Spotify
{
    internal class PlaylistMenu
    {
        public static List<Option> playlistMenu;

        public static void Menu()
        {
            Console.WriteLine("PlayLists");

            // Playlist menu
            playlistMenu = new List<Option>
            {
                new Option("Add playlist", () => Playlist.AddPlaylist()),
                new Option("", () => Menu()),
            };

            // Add existing playlists
            for (int i = 0; i < Playlist.playlists.Count; i++)
            {
                Playlist playlist = Playlist.playlists[i];
                playlistMenu.Add(new Option(playlist.Name, () => playlist.ShowPlaylist()));
            }

            // Add exit
            playlistMenu.Add(new Option("", () => Menu()));
            playlistMenu.Add(new Option("Exit", () => MainMenu.Menu()));

            int index = 0;

            WriteMenu(playlistMenu, playlistMenu[index]);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < playlistMenu.Count)
                    {
                        index++;
                        WriteMenu(playlistMenu, playlistMenu[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(playlistMenu, playlistMenu[index]);
                    }
                }

                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    playlistMenu[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
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