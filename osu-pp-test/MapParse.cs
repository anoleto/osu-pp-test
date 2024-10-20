using OsuMemoryDataProvider.OsuMemoryModels.Direct;
using System;
using System.Diagnostics;
using System.IO;

namespace osu_pp_test
{
    public class MapParse
    {
        private string SongsPath;

        public MapParse(Process osuProcess)
        {
            var osuPath = Path.GetDirectoryName(osuProcess.MainModule.FileName);

            foreach (var line in File.ReadAllText(Path.Combine(osuPath, $"osu!.{Environment.UserName}.cfg")).Split('\n'))
            {
                if (line.StartsWith("BeatmapDirectory"))
                {
                    var songsFolder = line.Split('=')[1].Trim();
                    SongsPath = Path.IsPathRooted(songsFolder) ? songsFolder : Path.Combine(osuPath, songsFolder);
                    break;
                }
            }
        }

        internal string GetFilename(CurrentBeatmap currentBeatmap)
        {
            if (string.IsNullOrEmpty(currentBeatmap.FolderName) || string.IsNullOrEmpty(currentBeatmap.OsuFileName))
                throw new FileNotFoundException("Current beatmap information is missing.");

            if (!File.Exists(Path.Combine(SongsPath, currentBeatmap.FolderName, currentBeatmap.OsuFileName)))
                throw new FileNotFoundException("Beatmap file not found.");

            return Path.Combine(SongsPath, currentBeatmap.FolderName, currentBeatmap.OsuFileName);
        }
    }
}
