using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Spotify
{
    internal class MainMenu
    {
        public static List<Option> mainMenu;
        public static void Menu()
        {
            Console.WriteLine("Spotify applicatie | Sil Theunissen");

            // Main menu
            mainMenu = new List<Option>
            {
                new Option("Playlist", () => PlaylistMenu.Menu()),
                new Option("Friends", () =>  PlaylistMenu.Menu()),
                new Option("Artist", () =>  PlaylistMenu.Menu()),
                new Option("Exit", () => Environment.Exit(0)),
            };

            int index = 0;

            WriteMenu(mainMenu, mainMenu[index]);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < mainMenu.Count)
                    {
                        index++;
                        WriteMenu(mainMenu, mainMenu[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(mainMenu, mainMenu[index]);
                    }
                }

                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    mainMenu[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }

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
