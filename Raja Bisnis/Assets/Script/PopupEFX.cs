using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupEFX : EFX
{
    [HideInInspector] public double value;
    [HideInInspector] public AudioClip clip;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private AudioSource audioSource;


    void Start()
    {
        moneyText.text = SFNuffix.GetShortValue(value, 1);

        if(clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
