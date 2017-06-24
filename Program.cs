using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running Times");
            Console.WriteLine();
            Console.WriteLine("Press 't' and press ENTER to create a new track (I just need his length)");
            Console.WriteLine();
            Console.WriteLine("Press 'a' and press ENTER to sumbit a new time for a track");
            Console.WriteLine();
            Console.WriteLine("Press 'v' and press ENTER to view all your times for a track");
            Console.WriteLine();
            string input = Console.ReadLine();
            if (input.ToUpper() == "T")
            {
                Console.Clear();
                Console.WriteLine("Enter a name for the track:");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter the track's length (in kilometers, excat number)");
                string trackLength = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Now, leave a note, like the location this track starts and/or leads to. (you can enter it empty)");
                string note = Console.ReadLine();
                int track = int.Parse(File.ReadAllText(@"count.txt"));
                track = track + 1;
                File.WriteAllText(@"count.txt", track.ToString());
                string[] TextForTrack = new string[6];
                TextForTrack[0] = $"Track name: {name}";
                TextForTrack[1] = "";
                TextForTrack[2] = $"Length = {trackLength} kilometers";
                TextForTrack[3] = "";
                TextForTrack[4] = $"Note about the track: ";
                TextForTrack[5] = $"{note}";
                File.WriteAllLines($@"track{track}.txt", TextForTrack);
                Console.WriteLine();
                Console.WriteLine("The track has been created! Press any key and ENTER to return to the main meun");
                Console.ReadLine();
                Console.Clear();
                Main(args);
            }
            else if (input.ToUpper() == "A")
            {
                Console.Clear();
                string[] trackText;
                Console.WriteLine("Enter the number of the track (that you want to sumbit the run to):");
                Console.WriteLine();
                int tracksCount = int.Parse(File.ReadAllText("count.txt"));
                for (int i = 0; i <= tracksCount; i++)
                {
                    if (File.Exists($@"track{i}.txt"))
                    {
                        trackText = File.ReadAllLines($@"track{i}.txt");
                        Console.WriteLine($"{trackText[0]} (Track number {i})");
                        Console.WriteLine();
                    }
                }
                int input2 = int.Parse(Console.ReadLine());
                for (int i = 0; i <= tracksCount; i++)
                {
                    if (input2 == i)
                    {
                        trackText = File.ReadAllLines($@"track{i}.txt");
                        Console.Clear();
                        Console.WriteLine($"{trackText[0]}");
                        Console.WriteLine();
                        Console.WriteLine("Reminder: 1 hour = 3600 seconds, 1 minute = 60 seconds, and don't include hundreds!");
                        Console.WriteLine("Another reminder, there's a calculator application in your computer");
                        Console.WriteLine("So, for example, a run that took 1 hours and 5 minutes took:");
                        Console.WriteLine("3600 * 1 (hour) + 60 * 5 (minutes) = 3900 seconds. Calculate your run's seconds like that");
                        Console.WriteLine();
                        Console.WriteLine("Enter the total amount of seconds the run took:");
                        int newTime = int.Parse(Console.ReadLine());
                        int hours = 0;
                        int minutes = 0;
                        int seconds = 0;
                        Console.WriteLine();
                        Console.WriteLine("Additional notes (you can leave this empty):");
                        string note2 = Console.ReadLine();
                        if ((newTime / 3600 >= 1))
                        {
                            double remains = newTime % 3600;
                            double ForHours = newTime - remains;
                            hours = (int)ForHours / 3600;
                            newTime = newTime - (hours * 3600);
                        }
                        if (newTime / 60 >= 1)
                        {
                            double remains2 = newTime % 60;
                            double ForMinutes = newTime - remains2;
                            minutes = (int)ForMinutes / 60;
                            newTime = newTime - (minutes * 60);
                        }
                        if (newTime >= 1)
                        {
                            seconds = newTime;
                        }
                        int intialNumLines = trackText.Count() - 1;
                        string time = "";
                        if (minutes < 10 && seconds >= 10)
                        {
                            time = $"{hours}:0{minutes}:{seconds}";
                        }
                        if (seconds < 10 && minutes >= 10)
                        {
                            time = $"{hours}:{minutes}:0{seconds}";
                        }
                        if (seconds < 10 && minutes < 10)
                        {
                            time = $"{hours}:0{minutes}:0{seconds}";
                        }
                        if(seconds > 10 && minutes > 10)
                        {
                            time = $"{hours}:{minutes}:{seconds}";
                        }
                        string[] newFile = new string[intialNumLines + 7];
                        for (int y = 0; y <= intialNumLines; y++)
                        {
                            newFile[y] = trackText[y];
                        }
                        newFile[intialNumLines + 1] = "";
                        newFile[intialNumLines + 2] = "A run:";
                        newFile[intialNumLines + 3] = "";
                        newFile[intialNumLines + 4] = $"Data of sumbiting the run: {DateTime.Now}";
                        newFile[intialNumLines + 5] = $"Run time = {time}";
                        newFile[intialNumLines + 6] = $"Note: {note2}";
                        File.Delete($@"track{i}.txt");
                        File.WriteAllLines($@"track{i}.txt", newFile);
                        Console.WriteLine();
                        Console.WriteLine("The run has been sumbited!");
                        Console.WriteLine("You can view it by entering 'v' in the main meun and then choosing this run's track");
                        Console.WriteLine("Press any key to return to the main meun");
                        Console.ReadLine();
                        Console.Clear();
                        Main(args);
                    }
                }
            }
            else if (input.ToUpper() == "V")
            {
                Console.Clear();
                string[] trackText;

                Console.WriteLine("Enter the number of the track (that you want to view your runs in it):");
                Console.WriteLine();

                int tracksCount = int.Parse(File.ReadAllText("count.txt"));
                for (int i = 0; i <= tracksCount; i++)
                {
                    if (File.Exists($@"track{i}.txt"))
                    {
                        trackText = File.ReadAllLines($@"track{i}.txt");
                        Console.WriteLine($"{trackText[0]} (Track number {i})");
                        Console.WriteLine();
                    }
                }
               
                int input2 = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Note: the time will be shown like this: '0:00:00'");
                Console.WriteLine("So if the time is, say, '1:25:33',");
                Console.WriteLine("the run's time is 1 hour and 25 minutes and 33 seconds.");
                Console.WriteLine();
                for (int i = 0; i <= tracksCount; i++)
                {
                    if (input2 == i)
                    {
                        trackText = File.ReadAllLines($@"track{i}.txt");
                        for (int y = 0; y <= trackText.Count() - 1; y++)
                        {
                            Console.WriteLine(trackText[y]);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Press any key to return to the main meun");
                        Console.ReadLine();
                        Console.Clear();
                        Main(args);
                    }
                }
            }
            else
            {
                Console.Clear();
                Main(args);
            }

        }
    }
}
