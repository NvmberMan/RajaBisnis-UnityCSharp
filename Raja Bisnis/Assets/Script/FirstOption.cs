using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class FirstOption : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject pick;
    [SerializeField] GameObject board;

    [Header("UI Choice")]
    public Button food;
    public Button drink;
    public Button fashion;

    [Header("animation")]
    Animator animation;


    int id;

    public void YesTutorialChoice()
    {
        ShowOption();
        id = 1;
        print("Follow The Tuttorial");
    }
    public void NoTutorialChoice()
    {
        ShowOption();
        id = 0;
        print("Pick The Option");
    }

    public void Choice(Button type)
    {
        if(type == food)
        {
            print("I pick food");
            pick.gameObject.SetActive(false);
        }
        else if(type == drink)
        {
            print("I pick drink");
            pick.gameObject.SetActive(false);
        }
        else if(type == fashion)
        {
            print("I pick fashion");
            pick.gameObject.SetActive(false);
        }
    }

    private void ShowOption()
    {
        pick.gameObject.SetActive(true);
        board.gameObject.SetActive(false);
    }
}
