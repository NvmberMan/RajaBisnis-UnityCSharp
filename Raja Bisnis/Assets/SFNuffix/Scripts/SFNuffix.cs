using System;
using UnityEngine;

public static class SFNuffix
{
    private static NumbersSuffixHandler suffixHandler;
    static SFNuffix()
    {
        suffixHandler = Resources.Load<NumbersSuffixHandler>("Numbers");
    }

    public static string GetFullValue(double value, int n = 0)
    {
        for (int i = 0; i < suffixHandler.numberSuffixes.Length; i++)
        {
            double pow = Math.Pow(10, suffixHandler.numberSuffixes[i].PowerOfTen);
            if (value < pow)
            {
                if (i - 1 < 0)
                    break;
                double powResult = Math.Pow(10, suffixHandler.numberSuffixes[i - 1].PowerOfTen);
                double v = value / powResult;
                return string.Format("{0:F" + n + "} ", v) + suffixHandler.numberSuffixes[i - 1].Suffix;
            }
        }
        return Math.Floor(value).ToString();
    }

    public static string GetShortValue(double value, int n = 0)
    {
        for (int i = 0; i < suffixHandler.numberSuffixes.Length; i++)
        {
            double pow = Math.Pow(10, suffixHandler.numberSuffixes[i].PowerOfTen);
            if (value < pow)
            {
                if (i - 1 < 0)
                    break;
                double powResult = Math.Pow(10, suffixHandler.numberSuffixes[i - 1].PowerOfTen);
                double v = value / powResult;
                return string.Format("{0:F" + n + "}", v) + suffixHandler.numberSuffixes[i - 1].SuffixAbbreviation;
            }
        }
        return Math.Floor(value).ToString();
    }
}
