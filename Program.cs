using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;

namespace TinyChat
{
    class Program
    {
        static string chatFile = "chat.txt", nickName = string.Empty;
        static bool writeMode = false;
        static int lastPrintedLine = 0;
        static string[] chatLines = new string[] { };

        static void Main(string[] args)
        {
            Console.Write("Nick Name: @");
            nickName = Console.ReadLine();

            ReadChat();
            if (chatLines.Length > 20) lastPrintedLine = chatLines.Length - 20; // print only the last 20 lines at the beginning

            new Thread(new ThreadStart(UpdateChat)).Start();

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.LastWrite,
                Path = Environment.CurrentDirectory, 
                EnableRaisingEvents = true
            };
            watcher.Changed += (s,e) => ReadChat();

            while (true)
            {
                var read = Console.ReadKey(true);
                if (read.Key != ConsoleKey.Enter)
                {
                    writeMode = true;
                    Console.Write($"@{nickName}: ");
                    var readLine = Console.ReadLine();
                    if(readLine.Length > 0)
                    {
                        ClearCurrentConsoleLine();
                        AddMessage(readLine);
                    }
                    writeMode = false;
                }
            }
        }

        static void AddMessage(string messageLine)
        {
            var retryAttempts = 0;
            while (retryAttempts < 3)
            {
                try
                {
                    File.AppendAllText(chatFile, $"@{nickName}: {messageLine}{Environment.NewLine}");
                    break;
                }
                catch (IOException) // write clash. retry
                {
                    retryAttempts++;
                }
            }
        }

        static void ReadChat()
        {
            if(File.Exists(chatFile)) chatLines = ReadLines(chatFile).ToArray();
        }

        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        static void UpdateChat()
        {
            while (true)
            {
                Thread.Sleep(1000); // refresh every second

                if (writeMode) continue;

                for (; lastPrintedLine<chatLines.Length; lastPrintedLine++)
                    Console.WriteLine(chatLines[lastPrintedLine]);
            }
        }

        static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop-1);
        }
    }
}
