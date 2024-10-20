﻿using System;
using System.Linq;

namespace osu_pp_test.Mods
{
    [Flags]
    public enum Mods
    {
        None = 0,
        NoFail = 1 << 0,
        Easy = 1 << 1,
        TouchScreen = 1 << 2,  // old: 'NOVIDEO'
        Hidden = 1 << 3,
        HardRock = 1 << 4,
        SuddenDeath = 1 << 5,
        DoubleTime = 1 << 6,
        Relax = 1 << 7,
        HalfTime = 1 << 8,
        Nightcore = 1 << 9,
        Flashlight = 1 << 10,
        AutoPlay = 1 << 11,
        SpunOut = 1 << 12,
        AutoPilot = 1 << 13,
        Perfect = 1 << 14,
        KEY4 = 1 << 15,
        KEY5 = 1 << 16,
        KEY6 = 1 << 17,
        KEY7 = 1 << 18,
        KEY8 = 1 << 19,
        FadeIn = 1 << 20,
        Random = 1 << 21,
        Cinema = 1 << 22,
        Target = 1 << 23,
        KEY9 = 1 << 24,
        KeyCoop = 1 << 25,
        KEY1 = 1 << 26,
        KEY3 = 1 << 27,
        KEY2 = 1 << 28,
        ScoreV2 = 1 << 29,
        Mirror = 1 << 30
    }

    internal static class OsuMods
    {
        internal static string getMods(int modValue)
        {
            Mods mods = (Mods)modValue;
            return string.Join(", ", Enum.GetValues(typeof(Mods))
                .OfType<Mods>()
                .Where(mod => mods.HasFlag(mod) && mod != Mods.None)
                .Select(mod => mod.ToString()));
        }
    }
}
