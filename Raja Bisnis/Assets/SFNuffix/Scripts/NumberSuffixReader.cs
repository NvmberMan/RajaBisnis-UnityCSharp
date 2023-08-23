using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NumberSuffixReader : MonoBehaviour
{
    public TextAsset file;
    public NumbersSuffixHandler numbersSuffix;

    void Start()
    {
        string[] text = File.ReadAllLines(AssetDatabase.GetAssetPath(file));
        NumberSuffix[] s = new NumberSuffix[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            var spl = text[i].Split(" ");
            var ns = new NumberSuffix();
            ns.Suffix = spl[0];
            ns.SuffixAbbreviation = spl[1];
            ns.PowerOfTen = int.Parse(spl[2]);
            s[i]=ns;
        }
        numbersSuffix.numberSuffixes = s;

    }

    void Update()
    {

    }
}
