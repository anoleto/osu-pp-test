using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;
using osu_pp_test.Mods;
using osu_pp_test.Performance;

namespace osu_pp_test
{
    internal class Program
    {
        private static MapParse mapParser;
        private static StructuredOsuMemoryReader osuReader;
        internal static double pp, lastPP;

        internal static int TotalHits;

        [STAThread]
        static void Main()
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "rosu.dll")))
                throw new Exception("rosu.dll not found");

            while (Process.GetProcessesByName("osu!").Length == 0)
            {
                Console.WriteLine("osu! is not running. Waiting for osu! to start...");
                Thread.Sleep(9000);
            }

            var osuProcess = Process.GetProcessesByName("osu!").First();
            mapParser = new MapParse(osuProcess);
            Console.WriteLine("osu! is running!");
            osuReader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint("osu!");

            while (true)
            {
                var data = new GeneralData();
                var beatmap = new CurrentBeatmap();
                var score = new Player();

                if (!osuReader.TryRead(data))
                {
                    Console.Clear();
                    Thread.Sleep(3000);
                    if (Process.GetProcessesByName("osu!").Length == 0) break;
                    continue;
                }

                osuReader.TryRead(beatmap);
                osuReader.TryRead(score);
                mapParser.GetFilename(beatmap);

                string mapName = beatmap.OsuFileName.Replace(".osu", "");

                if (data.OsuStatus == OsuMemoryStatus.Playing)
                {
                    getTotalHits(score);
                    var ppCalc = new CalculateScore(mapParser.GetFilename(beatmap), data.Mods);
                    pp = System.Math.Round(ppCalc?.getPerformance(score) ?? 0, 2);
                    if (pp != lastPP)
                    {
                        lastPP = pp;
                        string mods = data.Mods > 0
                                ? OsuMods.getMods(data.Mods)
                                : "No Mod";
                        UpdateConsole($"{mapName}\nCurrent pp: {pp}\nMods: {mods}\n" +
                            $"[{score.Hit300}/{score.Hit100}/{score.Hit50}/{score.HitMiss}]");
                    }
                }
                else if (data.OsuStatus != OsuMemoryStatus.ResultsScreen){ Console.Clear(); }
            }
        }

        private static void UpdateConsole(string text)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(text);
        }

        private static void getTotalHits(Player play) =>
            TotalHits = play.Hit50 + play.Hit100 + play.Hit300 + play.HitMiss;
    }
}
