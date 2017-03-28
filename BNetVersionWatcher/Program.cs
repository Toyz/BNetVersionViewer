using System;
using System.IO;
using System.Net;
using System.Threading;

namespace BNetVersionWatcher
{
    class Program
    {
        static void Main(string[] gameCodes)
        {
            Console.Clear();
            TextWriter oldOut = Console.Out;

            FileStream filestream = new FileStream("out.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetError(streamwriter);

            while (true)
            {
                foreach (string game in gameCodes)
                {
                    var url = $"http://us.patch.battle.net:1119/{game}/versions?nocache={DateTime.Now.Millisecond}";
                    try
                    {
                        using (var wc = new WebClient())
                        {
                            var data = wc.DownloadString(url).TrimStart().TrimEnd();

                            Console.SetOut(streamwriter);
                            Console.WriteLine($"Game: {game}{Environment.NewLine}URL: {url}{Environment.NewLine}Contents: {Environment.NewLine}{data}{Environment.NewLine}------");
                            Console.SetOut(oldOut);
                            Console.WriteLine($"Game: {game}{Environment.NewLine}URL: {url}{Environment.NewLine}Contents: {Environment.NewLine}{data}{Environment.NewLine}------");
                        }
                    }catch(Exception ex)
                    {
                        Console.SetOut(streamwriter);
                        Console.WriteLine($"Game: {game} {Environment.NewLine}URL: {url} {Environment.NewLine}Error: {ex.Message}{Environment.NewLine}------");
                        Console.SetOut(oldOut);
                        Console.WriteLine($"Game: {game} {Environment.NewLine}URL: {url} {Environment.NewLine}Error: {ex.Message}{Environment.NewLine}------");
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
        }
    }
}
