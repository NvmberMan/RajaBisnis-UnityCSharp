using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    public Text text;
    public double value = 0;
    public int n = 2;
    public bool shortValue = false;
    private void OnValidate()
    {
        if(shortValue)
            text.text = SFNuffix.GetShortValue(value, n);
        else
            text.text = SFNuffix.GetFullValue(value, n);
    }
}
