using System;
using System.Runtime.InteropServices;

namespace osu_pp_test.Performance
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CalculateResult
    {
        public double pp;
        public double stars;
    }
}
