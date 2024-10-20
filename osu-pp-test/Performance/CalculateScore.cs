using OsuMemoryDataProvider.OsuMemoryModels.Direct;

namespace osu_pp_test.Performance
{
    internal class CalculateScore
    {
        private int mods;
        private Calculator calc;
        private string beatmaps;

        public CalculateScore(string bmap, int mod)
        {
            beatmaps = bmap;
            mods = mod;
            calc = new Calculator(bmap);
        }

        internal double getPerformance(Player score)
        {
            calc.getBmap(beatmaps);
            calc.SetMods(score.Mods.Value);
            return calc.CalculatePerformance(score);
        }
    }
}
