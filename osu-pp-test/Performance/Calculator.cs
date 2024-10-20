using OsuMemoryDataProvider.OsuMemoryModels.Direct;
using System.Runtime.InteropServices;

namespace osu_pp_test.Performance
{
    public sealed class Calculator
    {
        [DllImport("rosu", CallingConvention = CallingConvention.Cdecl, EntryPoint = "calculate_score")]
        public static extern CalculateResult calculate_score(
            string beatmapPath, uint mode, uint mods, uint maxCombos, double accuracy, uint missCount, Optionu32 passedObjects);

        public uint maxCombo;
        private string mapPath;

        private int Mods;

        public Calculator(string MapPath) => mapPath = MapPath;
        public void SetMods(int mods) => Mods = mods;
        public void getBmap(string path) => mapPath = path;

        public double CalculatePerformance(Player score)
        {
            return CalculateScore(score, (float)score.Accuracy * 100f);
        }

        public double CalculateScore(Player score, float accuracy)
        {
            CalculateResult result = calculate_score(
                mapPath,
                (uint)score.Mode,
                (uint)score.Mods.Value,
                score.MaxCombo,
                accuracy,
                score.HitMiss,
                Optionu32.FromNullable((uint?)Program.TotalHits)
            );

            return result.pp;
        }
    }
}
