using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstOption : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject pick;
    [SerializeField] GameObject board;

    [Header("UI Choice")]
    public Button food;
    public Button drink;
    public Button fashion;


    public void YesTutorialChoice()
    {
        ShowOption();
        print("Follow The Tuttorial");
    }
    public void NoTutorialChoice()
    {
        ShowOption();
        print("Pick The Option");
    }

    public void Choice(Button type)
    {
        if(type == food)
        {
            print("I pick food");
        }
        else if(type == drink)
        {
            print("I pick drink");
        }
        else if(type == fashion)
        {
            print("I pick fashion");
        }
    }

    private void ShowOption()
    {
        pick.gameObject.SetActive(true);
        board.gameObject.SetActive(false);
    }
}
